using System;
using System.Collections.Generic;
using System.Text;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgendaOdontologica.Data
{
    public class AgendaOdontologicaDbContext : IdentityDbContext
    { 
        public AgendaOdontologicaDbContext(DbContextOptions<AgendaOdontologicaDbContext> options)
            : base(options)
        {
        }

        public DbSet<NivelAcesso> NivelAcessos { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Agendamento> Agendamentoes { get; set; }
        public DbSet<Especializacao> Especializacaos { get; set; }
        public DbSet<HomeLogin> HomeLogins { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Secretaria> Secretarias { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }


    }
}
