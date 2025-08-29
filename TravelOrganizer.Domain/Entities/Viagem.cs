using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelOrganizer.Domain.Entities
{
    public class Viagem
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public List<Viajante> Viajantes { get; set; } = new();
        public List<Roteiro> Roteiros { get; set; } = new();
        
    }
}
