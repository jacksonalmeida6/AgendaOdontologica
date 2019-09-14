using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaOdontologica.Data;
using AgendaOdontologica.Models;

namespace AgendaOdontologica.Controllers
{
    public class UsuariosRolesController : Controller
    {
        private readonly AgendaOdontologicaDbContext _context;

        public UsuariosRolesController(AgendaOdontologicaDbContext context)
        {
            _context = context;
        }

        // GET: UsuariosRoles
        public async Task<IActionResult> Index()
        {

            return View(await _context.UsuariosRoles.ToListAsync());
        }

        // GET: UsuariosRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosRoles = await _context.UsuariosRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuariosRoles == null)
            {
                return NotFound();
            }

            return View(usuariosRoles);
        }

        // GET: UsuariosRoles/Create
        public IActionResult Create()
        {
           

            return View();
        }

        // POST: UsuariosRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HomeLoginId,NivelAcessoId")] UsuariosRoles usuariosRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuariosRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuariosRoles);
        }

        // GET: UsuariosRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosRoles = await _context.UsuariosRoles.FindAsync(id);
            if (usuariosRoles == null)
            {
                return NotFound();
            }
            return View(usuariosRoles);
        }

        // POST: UsuariosRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HomeLoginId,NivelAcessoId")] UsuariosRoles usuariosRoles)
        {
            if (id != usuariosRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuariosRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosRolesExists(usuariosRoles.Id))
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
            return View(usuariosRoles);
        }

        // GET: UsuariosRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosRoles = await _context.UsuariosRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuariosRoles == null)
            {
                return NotFound();
            }

            return View(usuariosRoles);
        }

        // POST: UsuariosRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuariosRoles = await _context.UsuariosRoles.FindAsync(id);
            _context.UsuariosRoles.Remove(usuariosRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosRolesExists(int id)
        {
            return _context.UsuariosRoles.Any(e => e.Id == id);
        }
    }
}
