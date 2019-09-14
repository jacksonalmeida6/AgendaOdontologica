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
    public class NivelAcessoesController : Controller
    {
        private readonly AgendaOdontologicaDbContext _context;

        public NivelAcessoesController(AgendaOdontologicaDbContext context)
        {
            _context = context;
        }

        // GET: NivelAcessoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.NivelAcessos.ToListAsync());
        }

        // GET: NivelAcessoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivelAcesso = await _context.NivelAcessos
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
            ViewData["NivelAcessoId"] = new SelectList(_context.NivelAcessos, "Id", "N");
            ViewData["HomeLoginId"] = new SelectList(_context.HomeLogins, "Id", "Id");
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
                _context.Add(nivelAcesso);
                await _context.SaveChangesAsync();
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

            var nivelAcesso = await _context.NivelAcessos.FindAsync(id);
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
                    _context.Update(nivelAcesso);
                    await _context.SaveChangesAsync();
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

            var nivelAcesso = await _context.NivelAcessos
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
            var nivelAcesso = await _context.NivelAcessos.FindAsync(id);
            _context.NivelAcessos.Remove(nivelAcesso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NivelAcessoExists(string id)
        {
            return _context.NivelAcessos.Any(e => e.Id == id);
        }
    }
}
