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
    public class MintavevoController : Controller
    {
        private readonly LaborDbContext _context;

        public MintavevoController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: Mintavevo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mintavevo.ToListAsync());
        }

        // GET: Mintavevo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMintavevo = await _context.Mintavevo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMintavevo == null)
            {
                return NotFound();
            }

            return View(cMintavevo);
        }

        // GET: Mintavevo/Create
        public IActionResult Create()
        {
            return View(new cMintavevo());
        }

        // POST: Mintavevo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MintavevoAzonosito,MvAkkrSzam,Nev,Cim,ErvKezdete,ErvVege,Created,LastModified")] cMintavevo cMintavevo)
        {
            cMintavevo.Created = DateTime.UtcNow;
            cMintavevo.LastModified = DateTime.UtcNow;
            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            // if (ModelState.IsValid)
            {
                _context.Add(cMintavevo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cMintavevo);
        }

        // GET: Mintavevo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMintavevo = await _context.Mintavevo.FindAsync(id);
            if (cMintavevo == null)
            {
                return NotFound();
            }
            return View(cMintavevo);
        }

        // POST: Mintavevo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MintavevoAzonosito,MvAkkrSzam,Nev,Cim,ErvKezdete,ErvVege,Created,LastModified")] cMintavevo cMintavevo)
        {
            cMintavevo.LastModified = DateTime.UtcNow;

            if (id != cMintavevo.Id)
            {
                return NotFound();
            }

            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cMintavevo);
                    await _context.SaveChangesAsync();
                }
               catch (DbUpdateConcurrencyException)
                {
                    if (!cMintavevoExists(cMintavevo.Id))
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
            return View(cMintavevo);

        }

        // GET: Mintavevo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMintavevo = await _context.Mintavevo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMintavevo == null)
            {
                return NotFound();
            }

            return View(cMintavevo);
        }

        // POST: Mintavevo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cMintavevo = await _context.Mintavevo.FindAsync(id);
            if (cMintavevo != null)
            {
                _context.Mintavevo.Remove(cMintavevo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cMintavevoExists(long id)
        {
            return _context.Mintavevo.Any(e => e.Id == id);
        }
    }
}
