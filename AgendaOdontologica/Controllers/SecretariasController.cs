using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SecretariasController : Controller
    {


        private readonly UserManager<HomeLogin> _secretariaUsuarios;// deixar como homeLogin
        private readonly SignInManager<HomeLogin> _secretariaLogin;//deicxar como homeLogin
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;
        private readonly RoleManager<NivelAcesso> _roleManager;// add esse ao contrutor

        public SecretariasController(UserManager<HomeLogin> secretariaUsuarios, SignInManager<HomeLogin> secretariaLogin,
            AgendaOdontologicaDbContext agendaOdontologica, RoleManager<NivelAcesso> roleManager)
        {
            _secretariaUsuarios = secretariaUsuarios;
            _secretariaLogin = secretariaLogin;
            _agendaOdontologica = agendaOdontologica;
            _roleManager = roleManager;
        }







        // GET: Secretarias
       
        [Authorize(Roles = "Administrador,Simples")]
       

        public async Task<IActionResult> Index()
        {
            return View(await _agendaOdontologica.Secretarias.ToListAsync());
        }

        // GET: Secretarias/Details/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaria = await _agendaOdontologica.Secretarias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secretaria == null)
            {
                return NotFound();
            }

            return View(secretaria);
        }

        // GET: Secretarias/Create
        //[Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Secretarias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataNaci,DataAdmissao,Login,Senha,CPF,Endereco,CEP,PIS")] Secretaria secretaria)
        {
            try
            {
                //validação de data de nascimento

                ViewBag.dataNasc = secretaria.DataNaci;

                if (ViewBag.dataNasc >= DateTime.Now)
                {
                    return RedirectToAction(nameof(Error), new { mensagem = " Data de nascimento Invalida" });
                }
                HomeLogin home = new HomeLogin
                {
                    Login = secretaria.Login,
                    Senha = secretaria.Senha,
                    UserName = secretaria.Login,
                    Nome = secretaria.Nome
                };

                //cria um usuario no Identity
                var UsuarioCriado = await _secretariaUsuarios.CreateAsync(home, home.Senha);
                
                //adiciona dentista
                _ = _agendaOdontologica.Secretarias.Add(secretaria);
                await _agendaOdontologica.SaveChangesAsync();


            }

            catch (Exception e)
            {
                var ex = e.Message;

            }
            return RedirectToAction(nameof(Index));


           
        }

        // GET: Secretarias/Edit/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaria = await _agendaOdontologica.Secretarias.FindAsync(id);
            if (secretaria == null)
            {
                return NotFound();
            }
            return View(secretaria);
        }

        // POST: Secretarias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNaci,DataAdmissao,Login,Senha,CPF,Endereco,CEP,PIS")] Secretaria secretaria)
        {
            if (id != secretaria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _agendaOdontologica.Update(secretaria);
                    await _agendaOdontologica.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecretariaExists(secretaria.Id))
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
            return View(secretaria);
        }

        // GET: Secretarias/Delete/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretaria = await _agendaOdontologica.Secretarias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secretaria == null)
            {
                return NotFound();
            }

            return View(secretaria);
        }

        // POST: Secretarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secretaria = await _agendaOdontologica.Secretarias.FindAsync(id);
            _agendaOdontologica.Secretarias.Remove(secretaria);
            await _agendaOdontologica.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecretariaExists(int id)
        {
            return _agendaOdontologica.Secretarias.Any(e => e.Id == id);
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