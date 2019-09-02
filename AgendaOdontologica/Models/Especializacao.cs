using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class Especializacao 
    {
        [Key]
        public int Id { get; set; }
        public string Especialidade { get; set; }

    }
}
