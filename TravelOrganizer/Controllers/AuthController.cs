using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly EmailAddressAttribute _emailAddressAttribute = new();
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly LinkGenerator linkGenerator;
        private readonly IUserStore<Usuario> _userStore;
        private readonly IOptionsMonitor<BearerTokenOptions> _bearerTokenOptions;
        private readonly IEmailService _emailService;

        public AuthController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, LinkGenerator linkGenerator, IUserStore<Usuario> userStore, IOptionsMonitor<BearerTokenOptions> bearerTokenOptions, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.linkGenerator = linkGenerator;
            _userStore = userStore;
            _bearerTokenOptions = bearerTokenOptions;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<Results<Ok, ValidationProblem>> Register(CadastrarUsuarioDTO dto)
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($"Register requires a user store with email support.");
            }

            var emailStore = (IUserEmailStore<Usuario>)_userStore;
            var email = dto.Email;

            if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
            {
                return CreateValidationProblem(IdentityResult.Failed(_userManager.ErrorDescriber.InvalidEmail(email)));
            }

            var user = new Usuario();
            user.Nome = dto.Nome;
            user.SobreNome = dto.Sobrenome;
            user.DataNascimento = dto.DataNascimento;
            await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            await SendConfirmationEmailAsync(user, email);
            return TypedResults.Ok();
        }

        [HttpPost("Login")]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] LoginRequest login)
        {
            var useCookieScheme = false;
            var isPersistent = false;
            _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent, lockoutOnFailure: true);

            if (result.RequiresTwoFactor)
            {
                if (!string.IsNullOrEmpty(login.TwoFactorCode))
                {
                    result = await _signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
                }
                else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
                {
                    result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
                }
            }

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            return TypedResults.Empty;
        }

        [HttpPost("Refresh")]
        public async Task<Results<Ok<AccessTokenResponse>, UnauthorizedHttpResult, SignInHttpResult, ChallengeHttpResult>> Refresh
            ([FromBody] RefreshRequest refreshRequest)
        {
            var refreshTokenProtector = _bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
            var refreshTicket = refreshTokenProtector.Unprotect(refreshRequest.RefreshToken);

            // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
            if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc || DateTime.UtcNow >= expiresUtc ||
                await _signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not Usuario user)

            {
                return TypedResults.Challenge();
            }

            var newPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
            return TypedResults.SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
        }
        [HttpPost("Logout")]
        public async Task<Results<Ok, UnauthorizedHttpResult>> Logout()
        {
            await _signInManager.SignOutAsync();
            return TypedResults.Ok();
        }

        [HttpPost("ForgotPassword")]
        public async Task<Results<Ok, ValidationProblem>> ForgotPassword([FromBody] ForgotPasswordRequest resetRequest, [FromServices] IServiceProvider sp)
        {
            var user = await _userManager.FindByEmailAsync(resetRequest.Email);

            if (user is not null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                await _emailService.SendEmailAsync(user.Nome, resetRequest.Email, "Assunto", HtmlEncoder.Default.Encode(code));
            }
            // Don't reveal that the user does not exist or is not confirmed, so don't return a 200 if we would have
            // returned a 400 for an invalid code given a valid user email.
            return TypedResults.Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<Results<Ok, ValidationProblem>> ResetPassword
            ([FromBody] ResetPasswordRequest resetRequest, [FromServices] IServiceProvider sp)
        {
            var user = await _userManager.FindByEmailAsync(resetRequest.Email);

            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed, so don't return a 200 if we would have
                // returned a 400 for an invalid code given a valid user email.
                return CreateValidationProblem(IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken()));
            }

            IdentityResult result;
            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetRequest.ResetCode));
                result = await _userManager.ResetPasswordAsync(user, code, resetRequest.NewPassword);
            }
            catch (FormatException)
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        }

        [HttpGet("ConfirmEmail", Name = "ConfirmEmail")]
        async Task<Results<ContentHttpResult, UnauthorizedHttpResult>> ConfirmEmail([FromQuery] string userId, [FromQuery] string code, [FromQuery] string? changedEmail, [FromServices] IServiceProvider sp)
        {
            if (await _userManager.FindByIdAsync(userId) is not { } user)
            {
                // We could respond with a 404 instead of a 401 like Identity UI, but that feels like unnecessary information.
                return TypedResults.Unauthorized();
            }

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException)
            {
                return TypedResults.Unauthorized();
            }

            IdentityResult result;

            if (string.IsNullOrEmpty(changedEmail))
            {
                result = await _userManager.ConfirmEmailAsync(user, code);
            }
            else
            {
                // As with Identity UI, email and user name are one and the same. So when we update the email,
                // we need to update the user name.
                result = await _userManager.ChangeEmailAsync(user, changedEmail, code);

                if (result.Succeeded)
                {
                    result = await _userManager.SetUserNameAsync(user, changedEmail);
                }
            }

            if (!result.Succeeded)
            {
                return TypedResults.Unauthorized();
            }

            return TypedResults.Text("Thank you for confirming your email.");
        }

        private static ValidationProblem CreateValidationProblem(IdentityResult result)
        {
            Debug.Assert(!result.Succeeded);
            var errorDictionary = new Dictionary<string, string[]>(1);

            foreach (var error in result.Errors)
            {
                string[] newDescriptions;

                if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                {
                    newDescriptions = new string[descriptions.Length + 1];
                    Array.Copy(descriptions, newDescriptions, descriptions.Length);
                    newDescriptions[descriptions.Length] = error.Description;
                }
                else
                {
                    newDescriptions = [error.Description];
                }

                errorDictionary[error.Code] = newDescriptions;
            }

            return TypedResults.ValidationProblem(errorDictionary);
        }

        private async Task SendConfirmationEmailAsync(Usuario user, string email, bool isChange = false)
        {
            var code = isChange
                ? await _userManager.GenerateChangeEmailTokenAsync(user, email)
                : await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var userId = await _userManager.GetUserIdAsync(user);
            var routeValues = new RouteValueDictionary()
            {
                ["userId"] = userId,
                ["code"] = code,
            };

            if (isChange)
            {
                // This is validated by the /confirmEmail endpoint on change.
                routeValues.Add("changedEmail", email);
            }

            var confirmEmailUrl = linkGenerator.GetUriByName(HttpContext, "email", routeValues)
                ?? throw new NotSupportedException($"Could not find confirmation email Uri.");

            await _emailService.SendEmailAsync(user.Nome, email, "assunto", HtmlEncoder.Default.Encode(confirmEmailUrl));
        }
    }
}

