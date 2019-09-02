using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class Secretaria
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNaci { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public int CEP { get; set; }
        public string PIS { get; set; }

    }
}
