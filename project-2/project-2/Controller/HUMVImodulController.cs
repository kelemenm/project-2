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
    public class HUMVIModulController : Controller
    {
        private readonly LaborDbContext _context;

        public HUMVIModulController(LaborDbContext context)
        {
            _context = context;
        }

        public IActionResult LoadIndexPartial()
        {
            var data = _context.HumviModul.ToList();
            return PartialView("Index", data);
        }
        public IActionResult LoadDetailsPartial(int id)
        {
            var model = _context.HumviModul.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Details", model);
        }

        public IActionResult LoadEditPartial(int id)
        {
            var model = _context.HumviModul.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Edit", model);
        }

        public IActionResult LoadCreatePartial()
        {
            var model = new cHUMVImodul();
            return PartialView("Create", model);
        }

        public IActionResult LoadDeletePartial(int id)
        {
            var model = _context.HumviModul.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Delete", model);
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
        public async Task<IActionResult> Create([Bind("ModulKod,Leiras")] HUMVImodulDto hUMVImodulDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newHUMVImodul = new cHUMVImodul
                {
                    ModulKod = hUMVImodulDto.ModulKod,
                    Leiras = hUMVImodulDto.Leiras,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newHUMVImodul);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hUMVImodulDto);
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

        // POST: HUMVIModul/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ModulKod,Leiras")] HUMVImodulDto hUMVImodulDto)
        {
            if (hUMVImodulDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var hUMVImodul = await _context.HumviModul.FirstOrDefaultAsync(x => x.Id == hUMVImodulDto.Id);
                    if (hUMVImodul != null)
                    {
                        hUMVImodul.ModulKod = hUMVImodulDto.ModulKod;
                        hUMVImodul.Leiras = hUMVImodulDto.Leiras;
                        hUMVImodul.LastModified = DateTime.UtcNow;
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
            return View(hUMVImodulDto);

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
            return RedirectToAction("Index");
        }

        private bool cHumviModulExists(long id)
        {
            return _context.HumviModul.Any(e => e.Id == id);
        }
    }
}
