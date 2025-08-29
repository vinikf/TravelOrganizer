using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelOrganizer.Domain.Entities
{
    public class Roteiro
    {
        public int Id { get; set; }
        public DateTime Dia { get; set; }
        [StringLength(8000)]
        public string Descricao { get; set; }

        public Roteiro(DateTime dia, string descricao)
        {
            Dia = dia;
            Descricao = descricao;
        }
    }
}
