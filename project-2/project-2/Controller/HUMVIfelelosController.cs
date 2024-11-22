using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using Microsoft.CodeAnalysis.Scripting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace project_2.Controllers
{
    public class HUMVIfelelosController : Controller
    {
        private readonly LaborDbContext _context;

        public HUMVIfelelosController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: HUMVIfelelos
        public async Task<IActionResult> Index()
        {
            return View(await _context.HumviFelelos.ToListAsync());
        }

        // GET: HUMVIfelelos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cHUMVIfelelos = await _context.HumviFelelos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cHUMVIfelelos == null)
            {
                return NotFound();
            }

            return View(cHUMVIfelelos);
        }

        // GET: HUMVIfelelos/Create
        public IActionResult Create()
        {
            return View(new cHUMVIfelelos());
        }

        // POST: HUMVIfelelos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Felelos,Nev,Cim,Created,LastModified")] cHUMVIfelelos cHUMVIfelelos)
        {
            cHUMVIfelelos.Created = DateTime.UtcNow;
            cHUMVIfelelos.LastModified = DateTime.UtcNow;
            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            // if (ModelState.IsValid)
            {
                _context.Add(cHUMVIfelelos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cHUMVIfelelos);
        }

        // GET: HUMVIfelelos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cHUMVIfelelos = await _context.HumviFelelos.FindAsync(id);
            if (cHUMVIfelelos == null)
            {
                return NotFound();
            }
            return View(cHUMVIfelelos);
        }

        // POST: HUMVIfelelos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Felelos,Nev,Cim,Created,LastModified")] cHUMVIfelelos cHUMVIfelelos)
        {
            cHUMVIfelelos.LastModified = DateTime.UtcNow;

            if (id != cHUMVIfelelos.Id)
            {
                return NotFound();
            }

            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cHUMVIfelelos);
                    await _context.SaveChangesAsync();
                }
               catch (DbUpdateConcurrencyException)
                {
                    if (!cHUMVIfelelosExists(cHUMVIfelelos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(cHUMVIfelelos);

        }

        // GET: HUMVIfelelos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cHUMVIfelelos = await _context.HumviFelelos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cHUMVIfelelos == null)
            {
                return NotFound();
            }

            return View(cHUMVIfelelos);
        }

        // POST: HUMVIfelelos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cHUMVIfelelos = await _context.HumviFelelos.FindAsync(id);
            if (cHUMVIfelelos != null)
            {
                _context.HumviFelelos.Remove(cHUMVIfelelos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cHUMVIfelelosExists(long id)
        {
            return _context.HumviFelelos.Any(e => e.Id == id);
        }
    }
}
