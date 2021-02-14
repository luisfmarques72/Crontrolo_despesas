﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControloDespesas.Models;

namespace ControloDespesas.Controllers
{
    public class TipoDespesasController : Controller
    {
        private readonly Contexto _context;

        public TipoDespesasController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDespesas.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string txtProcurar)
        {
            if (!string.IsNullOrEmpty(txtProcurar))
                return View(await _context.TipoDespesas.Where(td=>td.Nome.ToUpper().Contains(txtProcurar.ToUpper())).ToListAsync());

                return View(await _context.TipoDespesas.ToListAsync());
        }

            // GET: TipoDespesas/Create
            public IActionResult Create()
        {
            return View();
        }

        public async Task<JsonResult> TipoDespesaExiste(string Nome)
        {
            if (await _context.TipoDespesas.AnyAsync(td => td.Nome.ToUpper() == Nome.ToUpper()))
                return Json("Esse tipo de despesa já existe");
            return Json(true);
        }

        // POST: TipoDespesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoDespesaID,Nome")] TipoDespesas tipoDespesas)
        {
            if (ModelState.IsValid)
            {

                TempData["confirmacao"] = tipoDespesas.Nome + " foi cadastrado com sucesso.";
                
                _context.Add(tipoDespesas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDespesas);
        }

        // GET: TipoDespesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDespesas = await _context.TipoDespesas.FindAsync(id);
            if (tipoDespesas == null)
            {
                return NotFound();
            }
            return View(tipoDespesas);
        }

        // POST: TipoDespesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoDespesaID,Nome")] TipoDespesas tipoDespesas)
        {
            if (id != tipoDespesas.TipoDespesaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    TempData["confirmacao"] = tipoDespesas.Nome + " foi cadastrado com sucesso.";
                    _context.Update(tipoDespesas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDespesasExists(tipoDespesas.TipoDespesaID))
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
            return View(tipoDespesas);
        }

        

        // POST: TipoDespesas/Delete/5
            
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var tipoDespesas = await _context.TipoDespesas.FindAsync(id);
            TempData["confirmacao"] = tipoDespesas.Nome + " foi excluido com sucesso.";
            _context.TipoDespesas.Remove(tipoDespesas);
            await _context.SaveChangesAsync();
            return Json(tipoDespesas.Nome + "excluido com sucesso");
        }

        private bool TipoDespesasExists(int id)
        {
            return _context.TipoDespesas.Any(e => e.TipoDespesaID == id);
        }
    }
}
