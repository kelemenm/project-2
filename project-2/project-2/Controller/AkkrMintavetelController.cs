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
    public class AkkrMintavetelController : Controller
    {
        private readonly LaborDbContext _context;

        public AkkrMintavetelController(LaborDbContext context)
        {
            _context = context;
        }

        public IActionResult LoadIndexPartial()
        {
            var data = _context.AkkrMintavetel.ToList();
            return PartialView("Index", data);
        }
        public IActionResult LoadDetailsPartial(int id)
        {
            var model = _context.AkkrMintavetel.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Details", model);
        }

        public IActionResult LoadEditPartial(int id)
        {
            var model = _context.AkkrMintavetel.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Edit", model);
        }

        public IActionResult LoadCreatePartial()
        {
            var model = new cAkkrMintavetel();
            return PartialView("Create", model);
        }

        public IActionResult LoadDeletePartial(int id)
        {
            var model = _context.AkkrMintavetel.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Delete", model);
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
        public async Task<IActionResult> Create([Bind("AkkrMintavetelStatusz,Leiras")] AkkrMintavetelDto akkrMintavetelDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newAkkrMintavetel = new cAkkrMintavetel
                {
                    AkkrMintavetelStatusz = akkrMintavetelDto.AkkrMintavetelStatusz,
                    Leiras = akkrMintavetelDto.Leiras,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newAkkrMintavetel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(akkrMintavetelDto);
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,AkkrMintavetelStatusz,Leiras")] AkkrMintavetelDto akkrMintavetelDto)
        {
            if (akkrMintavetelDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var akkrMintavetel = await _context.AkkrMintavetel.FirstOrDefaultAsync(x => x.Id == akkrMintavetelDto.Id);
                    if (akkrMintavetel != null)
                    {
                        akkrMintavetel.AkkrMintavetelStatusz = akkrMintavetelDto.AkkrMintavetelStatusz;
                        akkrMintavetel.Leiras = akkrMintavetelDto.Leiras;
                        akkrMintavetel.LastModified = DateTime.UtcNow;
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
            return PartialView("LoadEditPartial", akkrMintavetelDto);
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
            return RedirectToAction("Index");
        }

        private bool cAkkrMintavetelExists(long id)
        {
            return _context.AkkrMintavetel.Any(e => e.Id == id);
        }
    }
}
