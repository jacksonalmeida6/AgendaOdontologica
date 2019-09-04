using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Identity;
using AgendaOdontologica.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgendaOdontologica.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<HomeLogin> _homeUsuarios;
        private readonly SignInManager<HomeLogin> _homeLoginIdentity;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;
        private readonly RoleManager<NivelAcesso> _roleManager;


        public HomeController(UserManager<HomeLogin> homeUsuarios, SignInManager<HomeLogin> homeLogin,
            AgendaOdontologicaDbContext agendaOdontologica,RoleManager<NivelAcesso> roleManager)
        {
            _homeUsuarios = homeUsuarios;
            _homeLoginIdentity = homeLogin;
            _agendaOdontologica = agendaOdontologica;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Login(HomeLogin homeLogin)
        {
            var login = new HomeLogin
            {
                UserName = homeLogin.Login,
                Nome = homeLogin.Nome,
                Senha = homeLogin.Senha,
                
                
            };
            await _homeLoginIdentity.SignInAsync(login, false);

            return RedirectToAction("Idex", "Agendamentoes");
        }
        
        [HttpGet]
        public  IActionResult NovoNivelacesso()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoNivelacesso(NivelAcesso nivelAcesso)
        {
            if (ModelState.IsValid)
            {

                bool roleExiste = await _roleManager.RoleExistsAsync(nivelAcesso.Name);
                if (!roleExiste)
                {
                    await _roleManager.CreateAsync(nivelAcesso);
                    return RedirectToAction(nameof(Index));
                }

            }
            return View();
        }
       
        public async Task<IActionResult> AssociarUsuariosAsRoles()
        {
            // fazer um seletct de Dentista adicionando permissão
            ViewData["FuncionarioId"] = new SelectList(await _agendaOdontologica.Dentistas.ToListAsync(), "Id", "UserName");
            ViewData["SecretariaId"] = new SelectList(await _agendaOdontologica.Dentistas.ToListAsync(), "Id", "UserName");

            ViewData["UsuarioId"] = new SelectList(await _agendaOdontologica.HomeLogins.ToListAsync(), "Id", "Nome");
            ViewData["NivelAcessoId"] = new SelectList(await _agendaOdontologica.NivelAcessos.ToListAsync(), "Name", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssociarUsuariosAsRoles(UsuariosRoles usuariosRoles)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _homeUsuarios.FindByIdAsync( usuariosRoles.HomeLoginId);
                await _homeUsuarios.AddToRoleAsync(usuario, usuariosRoles.NivelAcessoId) ;

                return RedirectToAction(nameof(Index));

            }
            return View(usuariosRoles);
        }


        public async Task<IActionResult> Logout()
        {
            await _homeLoginIdentity.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
