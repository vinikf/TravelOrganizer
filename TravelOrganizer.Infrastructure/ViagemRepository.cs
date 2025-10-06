using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Infrastructure
{
    public class ViagemRepository : IViagemRepository
    {
        private readonly ApplicationDbContext _db;

        public ViagemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Salvar(Viagem viagem)
        {
            _db.Viagens.Add(viagem);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Viagem>> ObterTodas(int usuarioId)
        {
            return await _db.Viagens
                            .Where(v => v.UsuarioId == usuarioId)
                            .ToListAsync();
        }
    }
}
