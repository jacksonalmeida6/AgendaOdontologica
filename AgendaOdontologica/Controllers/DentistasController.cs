using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgendaOdontologica.Data;
using AgendaOdontologica.Models;
using AgendaOdontologica.Servicos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaOdontologica.Controllers
{
    public class DentistasController : Controller
    {
        private readonly UserManager<Dentista> _dentistaUsuarios;
        private readonly SignInManager<Dentista> _dentistaLogin;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;

        public DentistasController(UserManager<Dentista> dentistaUsuarios, SignInManager<Dentista> dentistaLogin, AgendaOdontologicaDbContext agendaOdontologica)
        {
            _dentistaUsuarios = dentistaUsuarios;
            _dentistaLogin = dentistaLogin;
            _agendaOdontologica = agendaOdontologica;
        }

        public async Task<IActionResult> Index()
        {
            var Agendamento = _agendaOdontologica.Dentistas.ToList();
            foreach (var item in Agendamento)
            {
                item.Especializacao = _agendaOdontologica.Especializacaos.Find(item.EspecializacaoId);
            }


            return View(await _agendaOdontologica.Dentistas.ToListAsync());
        }


        // GET: Dentistas/Details/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não existe" });
            }

            var dentista = await _agendaOdontologica.Dentistas.FirstOrDefaultAsync(m => m.Id == id);
            if (dentista == null)
            {
                return RedirectToAction(nameof(Error), new { mensagem = "Id não fornecido" });
            }

            return View(dentista);
        }

        // GET: Dentistas/Create
        //[Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            // metodo criar


            var especializacaos = _agendaOdontologica.Especializacaos.ToList();
            ViewBag.Especialidades = especializacaos;
            return View();

        }

        // POST: Dentistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(Dentista dentista)
        {
            try
            {
                ViewBag.dataNasc = dentista.DataNaci;
                if (ViewBag.dataNasc >= DateTime.Now)
                {
                    return RedirectToAction(nameof(Error), new { mensagem = " Data de nascimento Invalida" });
                }
                _ = _agendaOdontologica.Dentistas.Add(dentista);
                await _agendaOdontologica.SaveChangesAsync();



            }
            catch (Exception e)
            {
                var ex = e.Message;

            }
            return RedirectToAction(nameof(Index));







        }

        // GET: Dentistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var especializacaos = _agendaOdontologica.Especializacaos.ToList();
            ViewBag.Especialidades = especializacaos;

            if (id == null)
            {
                return NotFound();
            }

            var dentista = await _agendaOdontologica.Dentistas.FindAsync(id);
            if (dentista == null)
            {
                return NotFound();
            }
            return View(dentista);
        }

        // POST: Dentistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNaci,DataAdmissao,Login,Senha,CPF,Endereco,CEP,PIS")] Secretaria dentista)
        {
            if (id != dentista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _agendaOdontologica.Update(dentista);
                    await _agendaOdontologica.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DentistaExists(dentista.Id))
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
            return View(dentista);
        }

        // GET: Dentistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentista = await _agendaOdontologica.Dentistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dentista == null)
            {
                return NotFound();
            }

            return View(dentista);
        }

        // POST: Dentistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dentista = await _agendaOdontologica.Dentistas.FindAsync(id);
            _agendaOdontologica.Dentistas.Remove(dentista);
            await _agendaOdontologica.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DentistaExists(int id)
        {
            return _agendaOdontologica.Dentistas.Any(e => e.Id == id);
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
