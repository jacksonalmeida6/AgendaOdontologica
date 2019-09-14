using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class Secretaria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nome Dentista")]
        public string Nome { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Data nascimento")]
        [Required]
        public DateTime DataNaci { get; set; }
        [Required]
        [DisplayName("Data Admissão")]  
        [DataType(DataType.Date)]  
        public DateTime DataAdmissao { get; set; }
        
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]// senha
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public int CEP { get; set; }
        public string PIS { get; set; }

    }
}
