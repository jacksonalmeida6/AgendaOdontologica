using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly UserManager<HomeLogin> _funcionarioUsuarios;
        private readonly SignInManager<HomeLogin> _funcionarioLogin;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;
        private readonly RoleManager<NivelAcesso> _roleManager;

        public FuncionariosController(UserManager<HomeLogin> funcionarioUsuarios, SignInManager<HomeLogin> funcionarioLogin,
            AgendaOdontologicaDbContext agendaOdontologica, RoleManager<NivelAcesso> roleManager)
        {
            _funcionarioUsuarios = funcionarioUsuarios;
            _funcionarioLogin = funcionarioLogin;
            _agendaOdontologica = agendaOdontologica;
            _roleManager = roleManager;
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
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFuncionario,Funcao,DataNaci,DataAdmissao,Login,Senha,CPF,Endereco,CEP,PIS")] Funcionario funcionario)
        {
            try
            {
                //validação de data de nascimento

                ViewBag.dataNasc = funcionario.DataNaci;

                if (ViewBag.dataNasc >= DateTime.Now)
                {
                    return RedirectToAction(nameof(Error), new { mensagem = " Data de nascimento Invalida" });
                }
                HomeLogin home = new HomeLogin
                {
                    Login = funcionario.Login,
                    Senha = funcionario.Senha,
                    UserName = funcionario.Login,
                    Nome = funcionario.NomeFuncionario
                };

                //cria um usuario no Identity
                var UsuarioCriado = await _funcionarioUsuarios.CreateAsync(home, home.Senha);
                //loga esse usuario no sistema
                
                //adiciona dentista
                _ = _agendaOdontologica.Funcionarios.Add(funcionario);
                await _agendaOdontologica.SaveChangesAsync();


            }

            catch (Exception e)
            {
                var ex = e.Message;

            }
            return RedirectToAction(nameof(Index));
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

         //POST: Funcionarios/Edit/5
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

         //POST: Funcionarios/Delete/5
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