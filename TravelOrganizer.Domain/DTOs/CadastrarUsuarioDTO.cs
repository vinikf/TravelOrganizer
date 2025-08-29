﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelOrganizer.Domain.DTOs
{
    public class CadastrarUsuarioDTO
    {
        [Required]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
