using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class Dentista
    {
        public int Id { get; set; }
        public int EspecializacaoId { get; set; }

        [Required]
        [DisplayName("Nome Dentista")]
        public string Nome { get; set; }

        public Especializacao Especializacao { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data nascimento")]
        [Required]
        public DateTime DataNaci { get; set; }

        [Required]
        [DisplayName("Data Admissao")]  // anotação de nome para ficar separado no CRUD
        [DataType(DataType.Date)]  //tirando a hora e deixando somente a data
        public DateTime DataAdmissao { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]// senha
        public string Senha { get; set; }

        //[MaxLength(11)]
        //[MinLength(11)]
        [Required]
        public string CPF { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public int CEP { get; set; }

        [Required]
        public string PIS { get; set; }

    }
}
