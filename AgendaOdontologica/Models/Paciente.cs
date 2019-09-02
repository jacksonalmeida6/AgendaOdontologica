using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class Paciente 
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome Paciente")]
        public string NomePaciente { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Endereço { get; set; }
        public int CEP { get; set; }
        [DisplayName("Valor Tratamento")]
        public double ValorTratamento { get; set; }
        [DisplayName("FormaDe Pagamento")]
        public string FormaDePagamento { get; set; }

    }
}
