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
    public class HUMVIfelelosController : Controller
    {
        private readonly LaborDbContext _context;

        public HUMVIfelelosController(LaborDbContext context)
        {
            _context = context;
        }

        public IActionResult LoadIndexPartial()
        {
            var data = _context.HumviFelelos.ToList();
            return PartialView("Index", data);
        }
        public IActionResult LoadDetailsPartial(int id)
        {
            var model = _context.HumviFelelos.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Details", model);
        }

        public IActionResult LoadEditPartial(int id)
        {
            var model = _context.HumviFelelos.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Edit", model);
        }

        public IActionResult LoadCreatePartial()
        {
            var model = new cHUMVIfelelos();
            return PartialView("Create", model);
        }

        public IActionResult LoadDeletePartial(int id)
        {
            var model = _context.HumviFelelos.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Delete", model);
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
        public async Task<IActionResult> Create([Bind("Felelos,Nev,Cim")] HUMVIfelelosDto hUMVIfelelosDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newHUMVIfelelos = new cHUMVIfelelos
                {
                    Felelos = hUMVIfelelosDto.Felelos,
                    Nev = hUMVIfelelosDto.Nev,
                    Cim = hUMVIfelelosDto.Cim,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newHUMVIfelelos);
                await _context.SaveChangesAsync();
                return Json(new
                {
                    success = true,
                    tableName = "HumviFelelos"  // Adattábla neve, amit kezelünk
                });
            }
            return View(hUMVIfelelosDto);
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,Felelos,Nev,Cim")] HUMVIfelelosDto hUMVIfelelosDto)
        {
            if (hUMVIfelelosDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var hUMVIfelelos = await _context.HumviFelelos.FirstOrDefaultAsync(x => x.Id == hUMVIfelelosDto.Id);
                    if (hUMVIfelelos != null)
                    {
                        hUMVIfelelos.Felelos = hUMVIfelelosDto.Felelos;
                        hUMVIfelelos.Nev = hUMVIfelelosDto.Nev;
                        hUMVIfelelos.Cim = hUMVIfelelosDto.Cim;
                        hUMVIfelelos.LastModified = DateTime.UtcNow;
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
                return Json(new
                {
                    success = true,
                    tableName = "HumviFelelos"  // Adattábla neve, amit kezelünk
                });

            }
            return View(hUMVIfelelosDto);

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
            return Json(new
            {
                success = true,
                tableName = "HumviFelelos"  // Adattábla neve, amit kezelünk
            });
        }

        private bool cHUMVIfelelosExists(long id)
        {
            return _context.HumviFelelos.Any(e => e.Id == id);
        }
    }
}
