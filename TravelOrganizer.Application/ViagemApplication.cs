using TravelOrganizer.Api.Controllers;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Application
{
    public class ViagemApplication : IViagemApplication
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IUsuarioContext _usuarioContext;

        public ViagemApplication(IViagemRepository viagemRepository, IUsuarioContext usuarioContext)
        {
            _viagemRepository = viagemRepository;
            _usuarioContext = usuarioContext;
        }

        public async Task CriarViagem(NovaViagemDTO dto)
        {
            var viagem = new Viagem
            {
                Nome = dto.Nome,
                DataFim = dto.DataFim,
                DataInicio = dto.DataInicio,
                Roteiros = dto.Roteiros,
                UsuarioId = _usuarioContext.Usuario.Id,
                Viajantes = dto.Viajantes
            };

            await _viagemRepository.Salvar(viagem);
        }

        public async Task<List<Viagem>> ListarViagens()
        {
            var usuario = _usuarioContext.Usuario;
            return await _viagemRepository.ObterTodas(usuario.Id);
        }
    }
}
