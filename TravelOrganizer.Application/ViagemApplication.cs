using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelOrganizer.Api.Controllers;
using TravelOrganizer.Domain;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;
using TravelOrganizer.Infrastructure;

namespace TravelOrganizer.Application
{
    public class ViagemApplication
    {
        protected UsuarioLogadoDTO UsuarioLogado { get; set; }

        public ViagemApplication(UsuarioLogadoDTO usuarioLogado)
        {
            UsuarioLogado = usuarioLogado;
        }

        public async Task CriarViagem(NovaViagemDTO dto)
        {

            try
            {
                Viagem viagem = new()
                {
                    Nome = dto.Nome,
                    DataFim = dto.DataFim,
                    DataInicio = dto.DataInicio,
                    Roteiros = dto.Roteiros,
                    UsuarioId = UsuarioLogado.Id,
                    Viajantes = dto.Viajantes
                };

                using (var db = new ApplicationDbContext())
                {
                    var viagemRepository = new ViagensRepository(db, UsuarioLogado);
                    await viagemRepository.Salvar(viagem);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Viagem>> ListarViagens() 
        {
            using (var db = new ApplicationDbContext())
            {
                try
                {
                    return await new ViagensRepository(db, UsuarioLogado).ObterTodas();
                }
                catch (Exception)
                {
                    throw;
                }
            }
                
        } 
    }
}
