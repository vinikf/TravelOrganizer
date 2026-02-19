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
    public class User : IdentityUser<int>
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
