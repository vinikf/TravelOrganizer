using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TravelOrganizer.Domain.DTOs;

namespace TravelOrganizer.Domain.Entities
{
    public class Usuario : IdentityUser<int>
    {
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(100)]
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
