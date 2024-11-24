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
using project_2.Dtos;

namespace project_2.Controllers
{
    public class MvTipusController : Controller
    {
        private readonly LaborDbContext _context;

        public MvTipusController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: MvTipus
        public async Task<IActionResult> Index()
        {
            return View(await _context.MvTipus.ToListAsync());
        }

        // GET: MvTipus/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvTipus = await _context.MvTipus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMvTipus == null)
            {
                return NotFound();
            }

            return View(cMvTipus);
        }

        // GET: MvTipus/Create
        public IActionResult Create()
        {
            return View(new cMvTipus());
        }

        // POST: MvTipus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MvTipusNev,Leiras")] MvTipusDto mvTipusDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newMvTipus = new cMvTipus
                {
                    MvTipusNev = mvTipusDto.MvTipusNev,
                    Leiras = mvTipusDto.Leiras,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newMvTipus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mvTipusDto);
        }

        // GET: MvTipus/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvTipus = await _context.MvTipus.FindAsync(id);
            if (cMvTipus == null)
            {
                return NotFound();
            }
            return View(cMvTipus);
        }

        // POST: MvTipus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MvTipusNev,Leiras")] MvTipusDto mvTipusDto)
        {
            if (mvTipusDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mvTipus = await _context.MvTipus.FirstOrDefaultAsync(x => x.Id == mvTipusDto.Id);
                    if (mvTipus != null)
                    {
                        mvTipus.MvTipusNev = mvTipusDto.MvTipusNev;
                        mvTipus.Leiras = mvTipusDto.Leiras;
                        mvTipus.LastModified = DateTime.UtcNow;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(mvTipusDto);

        }

        // GET: MvTipus/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvTipus = await _context.MvTipus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMvTipus == null)
            {
                return NotFound();
            }

            return View(cMvTipus);
        }

        // POST: MvTipus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cMvTipus = await _context.MvTipus.FindAsync(id);
            if (cMvTipus != null)
            {
                _context.MvTipus.Remove(cMvTipus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cMvTipusExists(long id)
        {
            return _context.AkkrMintavetel.Any(e => e.Id == id);
        }
    }
}
