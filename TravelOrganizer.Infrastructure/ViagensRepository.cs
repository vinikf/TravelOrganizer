using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Infrastructure
{
    public class ViagensRepository
    {
        protected ApplicationDbContext db;
        protected UsuarioLogadoDTO UsuarioLogado { get; set; }
        public ViagensRepository(ApplicationDbContext db, UsuarioLogadoDTO usuarioLogado)
        {
            this.db = db;
            UsuarioLogado = usuarioLogado;
        }

        public async Task<List<Viagem>> ObterTodas()
        {
             return await db.Viagens
                .Where(v => v.UsuarioId == UsuarioLogado.Id)
                .ToListAsync();

        }

        public async Task Salvar(Viagem viagem)
        {
            await db.Viagens.AddAsync(viagem);
            await db.SaveChangesAsync();
        }
    }
}
