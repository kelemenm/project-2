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
    public class AkkrMintavetelController : Controller
    {
        private readonly LaborDbContext _context;

        public AkkrMintavetelController(LaborDbContext context)
        {
            _context = context;
        }

        public IActionResult LoadIndexPartial()
        {
            var data = _context.AkkrMintavetel.ToList(); // Ha van adatforrás
            return PartialView("Index", data);
        }


        // GET: AkkrMintavetel
        public async Task<IActionResult> Index()
        {
            return View(await _context.AkkrMintavetel.ToListAsync());
        }

        // GET: AkkrMintavetel/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cAkkrMintavetel = await _context.AkkrMintavetel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cAkkrMintavetel == null)
            {
                return NotFound();
            }

            return View(cAkkrMintavetel);
        }

        // GET: AkkrMintavetel/Create
        public IActionResult Create()
        {
            return View(new cAkkrMintavetel());
        }

        // POST: AkkrMintavetel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AkkrMintavetelStatusz,Leiras,Created,LastModified")] cAkkrMintavetel cAkkrMintavetel)
        {
            cAkkrMintavetel.Created = DateTime.UtcNow;
            cAkkrMintavetel.LastModified = DateTime.UtcNow;
            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            // if (ModelState.IsValid)
            {
                _context.Add(cAkkrMintavetel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cAkkrMintavetel);
        }

        // GET: AkkrMintavetel/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cAkkrMintavetel = await _context.AkkrMintavetel.FindAsync(id);
            if (cAkkrMintavetel == null)
            {
                return NotFound();
            }
            return View(cAkkrMintavetel);
        }

        // POST: AkkrMintavetel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,AkkrMintavetelStatusz,Leiras,Created,LastModified")] cAkkrMintavetel cAkkrMintavetel)
        {
            cAkkrMintavetel.LastModified = DateTime.UtcNow;

            if (id != cAkkrMintavetel.Id)
            {
                return NotFound();
            }

            //A VALIDÁLÁS NEM MEGY, DE KIKOMMENTELVE A SORT TUDJA ÍRNI ADATBÁZIS
            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cAkkrMintavetel);
                    await _context.SaveChangesAsync();
                }
               catch (DbUpdateConcurrencyException)
                {
                    if (!cAkkrMintavetelExists(cAkkrMintavetel.Id))
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
            return View(cAkkrMintavetel);

        }

        // GET: AkkrMintavetel/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cAkkrMintavetel = await _context.AkkrMintavetel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cAkkrMintavetel == null)
            {
                return NotFound();
            }

            return View(cAkkrMintavetel);
        }

        // POST: AkkrMintavetel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cAkkrMintavetel = await _context.AkkrMintavetel.FindAsync(id);
            if (cAkkrMintavetel != null)
            {
                _context.AkkrMintavetel.Remove(cAkkrMintavetel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cAkkrMintavetelExists(long id)
        {
            return _context.AkkrMintavetel.Any(e => e.Id == id);
        }
    }
}
