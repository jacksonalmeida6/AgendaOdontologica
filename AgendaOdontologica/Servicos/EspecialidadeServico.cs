using AgendaOdontologica.Data;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaOdontologica.Servicos
{
    public class EspecialidadeServico : Controller
    {
        private readonly AgendaOdontologicaDbContext _agendaOdontologicaDbContext;

        public EspecialidadeServico(AgendaOdontologicaDbContext agendaOdontologicaDbContext)
        {
            _agendaOdontologicaDbContext = agendaOdontologicaDbContext;
        }

        //retornar todas as especialidades
        public List<Especializacao> TodasEspecialidades()
        {
            return _agendaOdontologicaDbContext.Especializacaos.ToList();
        }

        public async Task<RedirectToActionResult> Inserir(Dentista dentista)
        {
            try
            {

                _agendaOdontologicaDbContext.Dentistas.Add(dentista);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
