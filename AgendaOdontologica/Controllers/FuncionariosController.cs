using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaOdontologica.Data;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaOdontologica.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly UserManager<Funcionario> _funcionarioUsuarios;
        private readonly SignInManager<Funcionario> _funcionarioLogin;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;

        public FuncionariosController(UserManager<Funcionario> funcionarioUsuarios, SignInManager<Funcionario> funcionarioLogin, AgendaOdontologicaDbContext agendaOdontologica)
        {
            _funcionarioUsuarios = funcionarioUsuarios;
            _funcionarioLogin = funcionarioLogin;
            _agendaOdontologica = agendaOdontologica;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _agendaOdontologica.Funcionarios.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _agendaOdontologica.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        //[Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFuncionario,Funcao,DataNaci,DataAdmissao,Login,Senha,CPF,Endereco,CEP,PIS")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _agendaOdontologica.Add(funcionario);
                await _agendaOdontologica.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _agendaOdontologica.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFuncionario,Funcao,DataNaci,DataAdmissao,Login,Senha,CPF,Endereco,CEP,PIS")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _agendaOdontologica.Update(funcionario);
                    await _agendaOdontologica.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
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
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _agendaOdontologica.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _agendaOdontologica.Funcionarios.FindAsync(id);
            _agendaOdontologica.Funcionarios.Remove(funcionario);
            await _agendaOdontologica.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _agendaOdontologica.Funcionarios.Any(e => e.Id == id);
        }
    }
}