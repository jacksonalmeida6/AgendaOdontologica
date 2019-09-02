using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class Funcionario 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome Funcionario")]
        public string NomeFuncionario { get; set; }


        [Required]
        public string Funcao { get; set; }


        [Required]
        [DisplayName("Data Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNaci { get; set; }


        [Required]
        [DisplayName("Data Admissão")]
        [DataType(DataType.Date)]
        public DateTime DataAdmissao { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]// senha
        public string Senha { get; set; }

        [Required]
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public int CEP { get; set; }
        public string PIS { get; set; }

    }
}
