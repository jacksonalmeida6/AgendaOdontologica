using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Models
{
    public class Agendamento 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Data e Hora Atendimento")]
        public DateTime HoraAtendimento { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayName("Tempo Atemdimento")]
        public DateTime TempoAtemdimento { get; set; }

        [DisplayName("Nome Dentista")]
        public Secretaria Dentista { get; set; }
        [DisplayName("Nome Paciente")]
        public Paciente Paciente { get; set; }
        [DisplayName("Nome Secretaria")]
        public Secretaria Secretaria { get; set; }
        [DisplayName("Nome Dentista")]
        public int DentistaId { get; set; }
        [DisplayName("Nome Paciente")]
        public int PacienteId { get; set; }
        [DisplayName("Nome Secretaria")]
        public int SecretariaId { get; set; }


    }
}
