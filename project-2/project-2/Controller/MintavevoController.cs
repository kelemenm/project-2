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
        public async Task<IActionResult> Create([Bind("MintavevoAzonosito,MvAkkrSzam,Nev,Cim,ErvKezdete,ErvVege")] MintavevoDto mintavevoDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newMintavevo = new cMintavevo
                {
                    MintavevoAzonosito = mintavevoDto.MintavevoAzonosito,
                    MvAkkrSzam = mintavevoDto.MvAkkrSzam,
                    Nev = mintavevoDto.Nev,
                    Cim = mintavevoDto.Cim,
                    ErvKezdete = mintavevoDto.ErvKezdete,
                    ErvVege = mintavevoDto.ErvVege,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newMintavevo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mintavevoDto);
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,MintavevoAzonosito,MvAkkrSzam,Nev,Cim,ErvKezdete,ErvVege")] MintavevoDto mintavevoDto)
        {
            if (mintavevoDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var mintavevo = await _context.Mintavevo.FirstOrDefaultAsync(x => x.Id == mintavevoDto.Id);
                    if (mintavevo != null)
                    {
                        mintavevo.MintavevoAzonosito = mintavevoDto.MintavevoAzonosito;
                        mintavevo.MvAkkrSzam = mintavevoDto.MvAkkrSzam;
                        mintavevo.Nev = mintavevoDto.Nev;
                        mintavevo.Cim = mintavevoDto.Cim;
                        mintavevo.ErvKezdete = mintavevoDto.ErvKezdete;
                        mintavevo.ErvVege = mintavevoDto.ErvVege;
                        mintavevo.LastModified = DateTime.UtcNow;
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
            return View(mintavevoDto);

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
