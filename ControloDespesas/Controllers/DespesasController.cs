﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControloDespesas.Models;
using X.PagedList;
using ControloDespesas.ViewModels;

namespace ControloDespesas.Controllers
{
    public class DespesasController : Controller
    {
        private readonly Contexto _context;

        public DespesasController(Contexto context)
        {
            _context = context;
        }

        // GET: Despesas
        public async Task<IActionResult> Index(int? pagina)
        {
            const int itensPagina = 10;
            int numeroPagina = (pagina ?? 1);
            ViewData["Meses"] = new SelectList(_context.Meses.Where(x => x.MesId == x.Salarios.Mesid), "MesId", "Nome");
          


            var contexto = _context.Despesas.Include(d => d.Meses).Include(d => d.TipoDespesas).OrderBy(d=>d.MesId);
            return View(await contexto.ToPagedListAsync(numeroPagina,itensPagina));
        }

        

        // GET: Despesas/Create
        public IActionResult Create()
        {
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome");
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "TipoDespesaID", "Nome");

            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaId,MesId,TipoDespesaId,Valor")] Despesas despesas)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(despesas);
                await _context.SaveChangesAsync();
                TempData["Confirmacao"] = "Despesa criada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesas.MesId);
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "TipoDespesaID", "Nome", despesas.TipoDespesaId);
            return View(despesas);
        }

        // GET: Despesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesas = await _context.Despesas.FindAsync(id);
            if (despesas == null)
            {
                return NotFound();
            }
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesas.MesId);          
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "TipoDespesaID", "Nome", despesas.TipoDespesaId);
            
            return View(despesas);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DespesaId,MesId,TipoDespesaId,Valor")] Despesas despesas)
        {
            if (id != despesas.DespesaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                try
                {
                    
                    _context.Update(despesas);
                    await _context.SaveChangesAsync();
                    TempData["Confirmacao"] = "Despesa atualizada com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesasExists(despesas.DespesaId))
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
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesas.MesId);
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "TipoDespesaID", "Nome", despesas.TipoDespesaId);
            return View(despesas);
        }

        
        // POST: Despesas/Delete/5
        [HttpPost]
        
        public async Task<IActionResult> Delete(int id)
        {
           
            var despesas = await _context.Despesas.FindAsync(id);
            TempData["Confirmacao"] = despesas.Valor + "Despesa apagada com sucesso.";
            _context.Despesas.Remove(despesas);
            await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(Index));
        }

        private bool DespesasExists(int id)
        {
            return _context.Despesas.Any(e => e.DespesaId == id);
        }

        public JsonResult GastosTotaisdoMes(int mesId)
        {
            GastosTotaisViewModel gastos = new GastosTotaisViewModel();
            gastos.ValorTotalGasto = _context.Despesas.Where(d => d.Meses.MesId == mesId).Sum(d => d.Valor);
            gastos.Salario = _context.Salarios.Where(s => s.Meses.MesId == mesId).Select(s => s.valor).FirstOrDefault();
            return Json(gastos);

        }
    }
}
