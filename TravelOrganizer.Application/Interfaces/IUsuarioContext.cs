using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Application.Interfaces
{
    public interface IUsuarioContext
    {
        UsuarioLogadoDTO Usuario { get; }
    }
}
