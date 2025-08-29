using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Api.Controllers
{
    public class NovaViagemDTO
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int UsuarioId { get; set; }
        public List<Viajante> Viajantes { get; set; } = new();
        public List<Roteiro> Roteiros { get; set; } = new();
    }
}