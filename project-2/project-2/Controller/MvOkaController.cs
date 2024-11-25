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
        public async Task<IActionResult> Create([Bind("MvOk,Leiras")] MvOkaDto mvOkaDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newMvOka = new cMvOka
                {
                    MvOk = mvOkaDto.MvOk,
                    Leiras = mvOkaDto.Leiras,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newMvOka);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mvOkaDto);

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
        public async Task<IActionResult> Edit(long id, [Bind("Id,MvOk,Leiras")] MvOkaDto mvOkaDto)
        {
            if (mvOkaDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mvOka = await _context.MvOka.FirstOrDefaultAsync(x => x.Id == mvOkaDto.Id);
                    if (mvOka != null)
                    {
                        mvOka.MvOk = mvOkaDto.MvOk;
                        mvOka.Leiras = mvOkaDto.Leiras;
                        mvOka.LastModified = DateTime.UtcNow;
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
            return View(mvOkaDto);
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
