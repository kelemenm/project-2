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
    public class MvOkaController : Controller
    {
        private readonly LaborDbContext _context;

        public MvOkaController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: MvOka
        public async Task<IActionResult> Index()
        {
            return View(await _context.MvOka.ToListAsync());
        }

        // GET: MvOka/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvOka = await _context.MvOka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMvOka == null)
            {
                return NotFound();
            }

            return View(cMvOka);
        }

        // GET: MvOka/Create
        public IActionResult Create()
        {
            return View(new cMvOka());
        }

        // POST: MvOka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MvOk,Leiras,Created,LastModified")] cMvOka cMvOka)
        {
            cMvOka.Created = DateTime.UtcNow;
            cMvOka.LastModified = DateTime.UtcNow;
            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            // if (ModelState.IsValid)
            {
                _context.Add(cMvOka);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cMvOka);
        }

        // GET: MvOka/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvOka = await _context.MvOka.FindAsync(id);
            if (cMvOka == null)
            {
                return NotFound();
            }
            return View(cMvOka);
        }

        // POST: MvOka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MvOk,Leiras,Created,LastModified")] cMvOka cMvOka)
        {
            cMvOka.LastModified = DateTime.UtcNow;

            if (id != cMvOka.Id)
            {
                return NotFound();
            }

            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cMvOka);
                    await _context.SaveChangesAsync();
                }
               catch (DbUpdateConcurrencyException)
                {
                    if (!cMvOkaExists(cMvOka.Id))
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
            return View(cMvOka);

        }

        // GET: MvOka/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvOka = await _context.MvOka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMvOka == null)
            {
                return NotFound();
            }

            return View(cMvOka);
        }

        // POST: MvOka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cMvOka = await _context.MvOka.FindAsync(id);
            if (cMvOka != null)
            {
                _context.MvOka.Remove(cMvOka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cMvOkaExists(long id)
        {
            return _context.AkkrMintavetel.Any(e => e.Id == id);
        }
    }
}
