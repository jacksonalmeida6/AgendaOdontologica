using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaOdontologica.Data;
using AgendaOdontologica.Models;
using Microsoft.AspNetCore.Identity;

namespace AgendaOdontologica.Controllers
{
    public class NivelAcessoesController : Controller
    {
        private readonly UserManager<HomeLogin> _dentistaUsuarios;
        private readonly SignInManager<HomeLogin> _dentistaLogin;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;
        private readonly RoleManager<NivelAcesso> _roleManager;

        public NivelAcessoesController(UserManager<HomeLogin> dentistaUsuarios, SignInManager<HomeLogin> dentistaLogin,
            AgendaOdontologicaDbContext agendaOdontologica, RoleManager<NivelAcesso> roleManager)
        {
            _dentistaUsuarios = dentistaUsuarios;
            _dentistaLogin = dentistaLogin;
            _agendaOdontologica = agendaOdontologica;
            _roleManager = roleManager;
        }



        // GET: NivelAcessoes
        public async Task<IActionResult> Index()
        {
            return View(await _agendaOdontologica.NivelAcessos.ToListAsync());
        }

        // GET: NivelAcessoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivelAcesso = await _agendaOdontologica.NivelAcessos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nivelAcesso == null)
            {
                return NotFound();
            }

            return View(nivelAcesso);
        }

        // GET: NivelAcessoes/Create
        public IActionResult Create()
        {
            ViewData["NivelAcessoId"] = new SelectList(_agendaOdontologica.NivelAcessos, "Id", "Name");
            ViewData["HomeLoginId"] = new SelectList(_agendaOdontologica.HomeLogins, "Id", "Id");
            return View();
        }

        // POST: NivelAcessoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] NivelAcesso nivelAcesso)
        {
            if (ModelState.IsValid)
            {
                _agendaOdontologica.Add(nivelAcesso);
                await _agendaOdontologica.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nivelAcesso);
        }

        // GET: NivelAcessoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivelAcesso = await _agendaOdontologica.NivelAcessos.FindAsync(id);
            if (nivelAcesso == null)
            {
                return NotFound();
            }
            return View(nivelAcesso);
        }

        // POST: NivelAcessoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] NivelAcesso nivelAcesso)
        {
            if (id != nivelAcesso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _agendaOdontologica.Update(nivelAcesso);
                    await _agendaOdontologica.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NivelAcessoExists(nivelAcesso.Id))
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
            return View(nivelAcesso);
        }

        // GET: NivelAcessoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivelAcesso = await _agendaOdontologica.NivelAcessos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nivelAcesso == null)
            {
                return NotFound();
            }

            return View(nivelAcesso);
        }

        // POST: NivelAcessoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nivelAcesso = await _agendaOdontologica.NivelAcessos.FindAsync(id);
            _agendaOdontologica.NivelAcessos.Remove(nivelAcesso);
            await _agendaOdontologica.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NivelAcessoExists(string id)
        {
            return _agendaOdontologica.NivelAcessos.Any(e => e.Id == id);
        }
    }
}
