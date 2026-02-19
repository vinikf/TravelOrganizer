using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Application.Interfaces
{
    public interface IUserContext
    {
        LoggedUserDTO User { get; }
    }
}
