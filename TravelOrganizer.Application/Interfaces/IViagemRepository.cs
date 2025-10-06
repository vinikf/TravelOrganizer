using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Application.Interfaces
{
    public interface IViagemRepository
    {
        Task Salvar(Viagem viagem);
        Task<List<Viagem>> ObterTodas(int usuarioId);
    }
}
