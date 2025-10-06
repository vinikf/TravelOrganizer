using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelOrganizer.Api.Controllers;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Application.Interfaces
{
    public interface IViagemApplication
    {
        Task CriarViagem(NovaViagemDTO dto);
        Task<List<Viagem>> ListarViagens();
    }
}
