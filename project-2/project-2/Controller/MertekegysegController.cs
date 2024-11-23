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
    public class MertekegysegController : Controller
    {
        private readonly LaborDbContext _context;

        public MertekegysegController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: Mertekegyseg
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mertekegyseg.ToListAsync());
        }

        // GET: Mertekegyseg/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMertekegyseg = await _context.Mertekegyseg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMertekegyseg == null)
            {
                return NotFound();
            }

            return View(cMertekegyseg);
        }

        // GET: Mertekegyseg/Create
        public IActionResult Create()
        {
            return View(new cMertekegyseg());
        }

        // POST: Mertekegyseg/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Megyseg,HumviLeiras,SajatLeiras")] MertekegysegDto mertekegysegDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newMertekegyseg = new cMertekegyseg
                {
                    Megyseg = mertekegysegDto.Megyseg,
                    HumviLeiras = mertekegysegDto.HumviLeiras,
                    SajatLeiras = mertekegysegDto.SajatLeiras,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newMertekegyseg);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mertekegysegDto);
        }

        // GET: Mertekegyseg/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMertekegyseg = await _context.Mertekegyseg.FindAsync(id);
            if (cMertekegyseg == null)
            {
                return NotFound();
            }
            return View(cMertekegyseg);
        }

        // POST: Mertekegyseg/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Megyseg,HumviLeiras,SajatLeiras")] MertekegysegDto mertekegysegDto)
        {
            if (mertekegysegDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mertekegyseg = await _context.Mertekegyseg.FirstOrDefaultAsync(x => x.Id == mertekegysegDto.Id);
                    if (mertekegyseg != null)
                    {
                        mertekegyseg.Megyseg = mertekegysegDto.Megyseg;
                        mertekegyseg.HumviLeiras = mertekegysegDto.HumviLeiras;
                        mertekegyseg.SajatLeiras = mertekegysegDto.SajatLeiras;
                        mertekegyseg.LastModified = DateTime.UtcNow;
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
            return View(mertekegysegDto);

        }

        // GET: Mertekegyseg/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMertekegyseg = await _context.Mertekegyseg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMertekegyseg == null)
            {
                return NotFound();
            }

            return View(cMertekegyseg);
        }

        // POST: Mertekegyseg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cMertekegyseg = await _context.Mertekegyseg.FindAsync(id);
            if (cMertekegyseg != null)
            {
                _context.Mertekegyseg.Remove(cMertekegyseg);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cMertekegysegExists(long id)
        {
            return _context.Mertekegyseg.Any(e => e.Id == id);
        }
    }
}
