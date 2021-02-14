using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControloDespesas.Models;

namespace ControloDespesas.Controllers
{
    public class SalariosController : Controller
    {
        private readonly Contexto _context;

        public SalariosController(Contexto context)
        {
            _context = context;
        }

        // GET: Salarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Salarios.Include(s => s.Meses);
            return View(await contexto.ToListAsync());
        }

        //Procurar por mÊs
        [HttpPost]
        public async Task<IActionResult> Index(string txtProcurar)
        {
            if (!string.IsNullOrEmpty(txtProcurar))
                return View(await _context.Salarios.Include(td => td.Meses).Where(m => m.Meses.Nome.ToUpper().Contains(txtProcurar.ToUpper())).ToListAsync());

            return View(await _context.Salarios.Include(s => s.Meses).ToListAsync());
        }


        // GET: Salarios/Create
        public IActionResult Create()
        {
            ViewData["Mesid"] = new SelectList(_context.Meses.Where(x=>x.MesId != x.Salarios.Mesid), "MesId", "Nome");
            return View();
        }

        // POST: Salarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalarioId,Mesid,valor")] Salarios salarios)
        {
            if (ModelState.IsValid)
            {

                TempData["Confirmacao"] = "Salário cadastrado com sucesso.";
                
                _context.Add(salarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mesid"] = new SelectList(_context.Meses, "MesId", "Nome", salarios.Mesid);
            return View(salarios);
        }

        // GET: Salarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarios = await _context.Salarios.FindAsync(id);
            if (salarios == null)
            {
                return NotFound();
            }
            ViewData["Mesid"] = new SelectList(_context.Meses.Where(x => x.MesId == x.Salarios.Mesid), "MesId", "Nome", salarios.Mesid);
            return View(salarios);
        }

        // POST: Salarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalarioId,Mesid,valor")] Salarios salarios)
        {
            if (id != salarios.SalarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = "Salário atualizado com sucesso.";
                try
                {
                    
                    _context.Update(salarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalariosExists(salarios.SalarioId))
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
            ViewData["Mesid"] = new SelectList(_context.Meses.Where(x => x.MesId == x.Salarios.Mesid), "MesId", "Nome", salarios.Mesid);
            return View(salarios);
        }

        

        // POST: Salarios/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            TempData["Confirmacao"] = "Salário atualizado apagado com sucesso.";
            var salarios = await _context.Salarios.FindAsync(id);
            _context.Salarios.Remove(salarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalariosExists(int id)
        {
            return _context.Salarios.Any(e => e.SalarioId == id);
        }
    }
}
