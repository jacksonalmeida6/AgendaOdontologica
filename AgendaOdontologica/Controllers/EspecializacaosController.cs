using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaOdontologica.Data;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaOdontologica.Controllers
{
   
    public class EspecializacaosController : Controller
    {
        //private readonly UserManager<Especializacao> _especializacaoUsuarios;
        //private readonly SignInManager<Especializacao> _especializacaoLogin;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;

        public EspecializacaosController(/*UserManager<Especializacao> especializacaoUsuarios, SignInManager<Especializacao> especializacaoLogin, */AgendaOdontologicaDbContext agendaOdontologica)
        {
            //_especializacaoUsuarios = especializacaoUsuarios;
            //_especializacaoLogin = especializacaoLogin;
            _agendaOdontologica = agendaOdontologica;
        }



        // GET: Especializacaos

        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _agendaOdontologica.Especializacaos.ToListAsync());
        }

        // GET: Especializacaos/Details/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especializacao = await _agendaOdontologica.Especializacaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especializacao == null)
            {
                return NotFound();
            }

            return View(especializacao);
        }

        // GET: Especializacaos/Create
        //[Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Especializacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Especialidade")] Especializacao especializacao)
        {
            if (ModelState.IsValid)
            {
                _agendaOdontologica.Add(especializacao);
                await _agendaOdontologica.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especializacao);
        }

        // GET: Especializacaos/Edit/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especializacao = await _agendaOdontologica.Especializacaos.FindAsync(id);
            if (especializacao == null)
            {
                return NotFound();
            }
            return View(especializacao);
        }

        // POST: Especializacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Especialidade")] Especializacao especializacao)
        {
            if (id != especializacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _agendaOdontologica.Update(especializacao);
                    await _agendaOdontologica.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecializacaoExists(especializacao.Id))
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
            return View(especializacao);
        }

        // GET: Especializacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especializacao = await _agendaOdontologica.Especializacaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especializacao == null)
            {
                return NotFound();
            }

            return View(especializacao);
        }

        // POST: Especializacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especializacao = await _agendaOdontologica.Especializacaos.FindAsync(id);
            _agendaOdontologica.Especializacaos.Remove(especializacao);
            await _agendaOdontologica.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecializacaoExists(int id)
        {
            return _agendaOdontologica.Especializacaos.Any(e => e.Id == id);
        }
    }
}