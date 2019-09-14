using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class HomeLogin :  IdentityUser
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
