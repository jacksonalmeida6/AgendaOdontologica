using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgendaOdontologica.Data;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgendaOdontologica.Controllers
{
    public class AgendamentoesController : Controller
    {
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;

        public AgendamentoesController(AgendaOdontologicaDbContext agendaOdontologica)
        {
            _agendaOdontologica = agendaOdontologica;
        }

        public async Task<IActionResult> Index()
        {
            var sistemaBennerContext = _agendaOdontologica.Agendamentoes.Include(a => a.Dentista).Include(a => a.Paciente).Include(a => a.Secretaria);
            foreach (var item in sistemaBennerContext)
            {
                item.Dentista = _agendaOdontologica.Dentistas.Find(item.DentistaId);
                item.Dentista.Especializacao = _agendaOdontologica.Especializacaos.Find(item.Dentista.EspecializacaoId);
                item.Paciente = _agendaOdontologica.Pacientes.Find(item.PacienteId);
            }
            return View(await sistemaBennerContext.ToListAsync());
        }

        // GET: Agendamentoes/Details/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _agendaOdontologica.Agendamentoes
                .Include(a => a.Dentista)
                .Include(a => a.Paciente)
                .Include(a => a.Secretaria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamentoes/Create
        //[Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewBag.PacienteNome = _agendaOdontologica.Pacientes.ToList();
            ViewBag.DentistaNome = _agendaOdontologica.Dentistas.ToList();
            ViewBag.SecretariaNome = _agendaOdontologica.Secretarias.ToList();
            ViewData["DentistaId"] = new SelectList(_agendaOdontologica.Dentistas, "Id", "Id");
            ViewData["PacienteId"] = new SelectList(_agendaOdontologica.Pacientes, "Id", "Id");
            ViewData["SecretariaId"] = new SelectList(_agendaOdontologica.Secretarias, "Id", "Id");
            var sistemaBennerContext = _agendaOdontologica.Agendamentoes.Include(a => a.Dentista).Include(a => a.Paciente).Include(a => a.Secretaria);
            foreach (var item in sistemaBennerContext)
            {
                item.Dentista = _agendaOdontologica.Dentistas.Find(item.DentistaId);
                item.Dentista.Especializacao = _agendaOdontologica.Especializacaos.Find(item.Dentista.EspecializacaoId);
                item.Paciente = _agendaOdontologica.Pacientes.Find(item.PacienteId);
            }
            return View();
        }

        // POST: Agendamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,HoraAtendimento,TempoAtemdimento,DentistaId,PacienteId,SecretariaId")] Agendamento agendamento)
        {
            try
            {
                if (agendamento.HoraAtendimento < DateTime.Now)
                {
                    return RedirectToAction(nameof(Error), new { mensagem = " Data de Agendamento não é valido !!!" });
                }
                if (_agendaOdontologica.Agendamentoes.All(x => x.Equals(agendamento)))
                {
                    return RedirectToAction(nameof(Error), new { mensagem = " Este Agendamento já Existe !!!" });
                }
                if (_agendaOdontologica.Agendamentoes.All(a => a.DentistaId.Equals(agendamento.DentistaId) &&
               a.HoraAtendimento.Equals(agendamento.HoraAtendimento) && a.PacienteId != agendamento.PacienteId))
                {
                    return RedirectToAction(nameof(Error), new { mensagem = " Dentista vai atender outro paciente esse Horario !!!" });
                }


                if (ModelState.IsValid)
                {
                    _agendaOdontologica.Add(agendamento);
                    await _agendaOdontologica.SaveChangesAsync();

                }
                ViewData["DentistaId"] = new SelectList(_agendaOdontologica.Dentistas, "Id", "Id", agendamento.DentistaId);
                ViewData["PacienteId"] = new SelectList(_agendaOdontologica.Pacientes, "Id", "Id", agendamento.PacienteId);
                ViewData["SecretariaId"] = new SelectList(_agendaOdontologica.Secretarias, "Id", "Id", agendamento.SecretariaId);
                return View(agendamento);

            }
            catch (System.Exception e)
            {

                var ex = e.Message;
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Agendamentoes/Edit/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _agendaOdontologica.Agendamentoes.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            ViewBag.PacienteNome = _agendaOdontologica.Pacientes.ToList();
            ViewBag.DentistaNome = _agendaOdontologica.Dentistas.ToList();
            ViewBag.SecretariaNome = _agendaOdontologica.Secretarias.ToList();
            ViewData["Secretaria"] = new SelectList(_agendaOdontologica.Secretarias, "Id", "Id", agendamento.Secretaria);
            ViewData["Dentista"] = new SelectList(_agendaOdontologica.Dentistas, "Id", "Id", agendamento.Dentista);

            ViewData["DentistaId"] = new SelectList(_agendaOdontologica.Dentistas, "Id", "Id", agendamento.DentistaId);
            ViewData["PacienteId"] = new SelectList(_agendaOdontologica.Pacientes, "Id", "Id", agendamento.PacienteId);
            ViewData["SecretariaId"] = new SelectList(_agendaOdontologica.Secretarias, "Id", "Id", agendamento.SecretariaId);
            
            return View(agendamento);
        }

        // POST: Agendamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoraAtendimento,TempoAtemdimento,DentistaId,PacienteId,SecretariaId")] Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _agendaOdontologica.Update(agendamento);
                    await _agendaOdontologica.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DentistaId"] = new SelectList(_agendaOdontologica.Dentistas, "Id", "Id", agendamento.DentistaId);
            ViewData["PacienteId"] = new SelectList(_agendaOdontologica.Pacientes, "Id", "Id", agendamento.PacienteId);
            ViewData["SecretariaId"] = new SelectList(_agendaOdontologica.Secretarias, "Id", "Id", agendamento.SecretariaId);
            return View(agendamento);
        }

        // GET: Agendamentoes/Delete/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _agendaOdontologica.Agendamentoes
                .Include(a => a.Dentista)
                .Include(a => a.Paciente)
                .Include(a => a.Secretaria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendamento = await _agendaOdontologica.Agendamentoes.FindAsync(id);
            _agendaOdontologica.Agendamentoes.Remove(agendamento);
            await _agendaOdontologica.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
            return _agendaOdontologica.Agendamentoes.Any(e => e.Id == id);
        }

        public IActionResult Error(string mensagem)
        {
            var viewModel = new ErrorViewModel
            {
                Mensagem = mensagem,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}