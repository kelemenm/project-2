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
    public class HUMVIModulController : Controller
    {
        private readonly LaborDbContext _context;

        public HUMVIModulController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: HUMVIModul
        public async Task<IActionResult> Index()
        {
            return View(await _context.HumviModul.ToListAsync());
        }

        // GET: HUMVIModul/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cHUMVImodul = await _context.HumviModul
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cHUMVImodul == null)
            {
                return NotFound();
            }

            return View(cHUMVImodul);
        }

        // GET: HUMVIModul/Create
        public IActionResult Create()
        {
            return View(new cHUMVImodul());
        }

        // POST: HUMVIModul/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModulKod,Leiras,Created,LastModified")] cHUMVImodul cHUMVImodul)
        {
            cHUMVImodul.Created = DateTime.UtcNow;
            cHUMVImodul.LastModified = DateTime.UtcNow;
            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            // if (ModelState.IsValid)
            {
                _context.Add(cHUMVImodul);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cHUMVImodul);
        }

        // GET: HUMVIModul/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cHUMVImodul = await _context.HumviModul.FindAsync(id);
            if (cHUMVImodul == null)
            {
                return NotFound();
            }
            return View(cHUMVImodul);
        }

        // POST: ccccccccccc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ModulKod,Leiras,Created,LastModified")] cHUMVImodul cHUMVImodul)
        {
            cHUMVImodul.LastModified = DateTime.UtcNow;

            if (id != cHUMVImodul.Id)
            {
                return NotFound();
            }

            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cHUMVImodul);
                    await _context.SaveChangesAsync();
                }
               catch (DbUpdateConcurrencyException)
                {
                    if (!cHumviModulExists(cHUMVImodul.Id))
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
            return View(cHUMVImodul);

        }

        // GET: HUMVIModul/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cHUMVImodul = await _context.HumviModul
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cHUMVImodul == null)
            {
                return NotFound();
            }

            return View(cHUMVImodul);
        }

        // POST: HUMVIModul/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cHUMVImodul = await _context.HumviModul.FindAsync(id);
            if (cHUMVImodul != null)
            {
                _context.HumviModul.Remove(cHUMVImodul);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cHumviModulExists(long id)
        {
            return _context.HumviModul.Any(e => e.Id == id);
        }
    }
}
