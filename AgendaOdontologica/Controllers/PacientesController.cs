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
    public class PacientesController : Controller
    {
        private readonly UserManager<Paciente> _pacienteUsuarios;
        private readonly SignInManager<Paciente> _pacienteLogin;
        private readonly AgendaOdontologicaDbContext _agendaOdontologica;

        public PacientesController(UserManager<Paciente> pacienteUsuarios, SignInManager<Paciente> pacienteLogin, AgendaOdontologicaDbContext agendaOdontologica)
        {
            _pacienteUsuarios = pacienteUsuarios;
            _pacienteLogin = pacienteLogin;
            _agendaOdontologica = agendaOdontologica;
        }



        // GET: Pacientes
        //[Authorize(Roles = "Administrador")]
        //[Authorize(Roles = "Simples")]
        public async Task<IActionResult> Index()
        {
            return View(await _agendaOdontologica.Pacientes.ToListAsync());
        }

        // GET: Pacientes/Details/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _agendaOdontologica.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        //[Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,NomePaciente,RG,CPF,Endereço,CEP,ValorTratamento,FormaDePagamento")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _agendaOdontologica.Add(paciente);
                await _agendaOdontologica.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _agendaOdontologica.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomePaciente,RG,CPF,Endereço,CEP,ValorTratamento,FormaDePagamento")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _agendaOdontologica.Update(paciente);
                    await _agendaOdontologica.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _agendaOdontologica.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _agendaOdontologica.Pacientes.FindAsync(id);
            _agendaOdontologica.Pacientes.Remove(paciente);
            await _agendaOdontologica.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _agendaOdontologica.Pacientes.Any(e => e.Id == id);
        }
    }
}