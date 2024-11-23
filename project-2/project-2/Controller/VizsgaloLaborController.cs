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
    public class VizsgaloLaborController : Controller
    {
        private readonly LaborDbContext _context;

        public VizsgaloLaborController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: VizsgaloLabor
        public async Task<IActionResult> Index()
        {
            return View(await _context.VizsgaloLabor.ToListAsync());
        }

        // GET: VizsgaloLabor/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVizsgaloLabor = await _context.VizsgaloLabor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cVizsgaloLabor == null)
            {
                return NotFound();
            }

            return View(cVizsgaloLabor);
        }

        // GET: VizsgaloLabor/Create
        public IActionResult Create()
        {
            return View(new cVizsgaloLabor());
        }

        // POST: VizsgaloLabor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Labor,LabAkkrSzam,Nev,Cim,ErvKezdete,ErvVege,Created,LastModified")] cVizsgaloLabor cVizsgaloLabor)
        {
            cVizsgaloLabor.Created = DateTime.UtcNow;
            cVizsgaloLabor.LastModified = DateTime.UtcNow;
            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            // if (ModelState.IsValid)
            {
                _context.Add(cVizsgaloLabor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cVizsgaloLabor);
        }

        // GET: VizsgaloLabor/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVizsgaloLabor = await _context.VizsgaloLabor.FindAsync(id);
            if (cVizsgaloLabor == null)
            {
                return NotFound();
            }
            return View(cVizsgaloLabor);
        }

        // POST: VizsgaloLabor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Labor,LabAkkrSzam,Nev,Cim,ErvKezdete,ErvVege,Created,LastModified")] cVizsgaloLabor cVizsgaloLabor)
        {
            cVizsgaloLabor.LastModified = DateTime.UtcNow;

            if (id != cVizsgaloLabor.Id)
            {
                return NotFound();
            }

            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cVizsgaloLabor);
                    await _context.SaveChangesAsync();
                }
               catch (DbUpdateConcurrencyException)
                {
                    if (!cVizsgaloLaborExists(cVizsgaloLabor.Id))
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
            return View(cVizsgaloLabor);

        }

        // GET: VizsgaloLabor/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVizsgaloLabor = await _context.VizsgaloLabor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cVizsgaloLabor == null)
            {
                return NotFound();
            }

            return View(cVizsgaloLabor);
        }

        // POST: VizsgaloLabor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cVizsgaloLabor = await _context.VizsgaloLabor.FindAsync(id);
            if (cVizsgaloLabor != null)
            {
                _context.VizsgaloLabor.Remove(cVizsgaloLabor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cVizsgaloLaborExists(long id)
        {
            return _context.VizsgaloLabor.Any(e => e.Id == id);
        }
    }
}
