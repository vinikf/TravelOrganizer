using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Application;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Application.Services;
using TravelOrganizer.Domain.Entities;
using TravelOrganizer.Infrastructure;
using TravelOrganizer.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "TravelPolicy",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5173", "https://localhost:44322")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); 
        });
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "TravelOrganizer API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT", 
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token no formato: {seu_token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
    options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
})
.AddCookie(IdentityConstants.ApplicationScheme)
.AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<ITripApplication, TripApplication>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("TravelPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
