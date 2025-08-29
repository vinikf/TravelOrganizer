using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelOrganizer.Domain.Entities
{
    public class Viajante
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool NecessitaAcessibilidade { get; set; }
    }
}
