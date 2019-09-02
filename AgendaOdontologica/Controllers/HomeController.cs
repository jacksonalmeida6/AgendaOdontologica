using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Identity;
using AgendaOdontologica.Data;

namespace AgendaOdontologica.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<HomeLogin> _homeUsuarios;
        private readonly SignInManager<HomeLogin> _homeLogin;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;

        public HomeController(UserManager<HomeLogin> homeUsuarios, SignInManager<HomeLogin> homeLogin, AgendaOdontologicaDbContext agendaOdontologica)
        {
            _homeUsuarios = homeUsuarios;
            _homeLogin = homeLogin;
            _agendaOdontologica = agendaOdontologica;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
