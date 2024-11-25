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
    public class MintaController : Controller
    {
        private readonly LaborDbContext _context;

        public MintaController(LaborDbContext context)
        {
            _context = context;
        }

        public IActionResult LoadIndexPartial()
        {
            var data = _context.Minta.ToList();
            return PartialView("Index", data);
        }
        public IActionResult LoadDetailsPartial(int id)
        {
            var model = _context.Minta.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Details", model);
        }

        public IActionResult LoadEditPartial(int id)
        {
            var model = _context.Minta.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Edit", model);
        }

        public IActionResult LoadCreatePartial()
        {
            var model = new cMinta();
            return PartialView("Create", model);
        }

        public IActionResult LoadDeletePartial(int id)
        {
            var model = _context.Minta.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return PartialView("Delete", model);
        }



        // GET: Minta
        public async Task<IActionResult> Index()
        {
            var laborDbContext = _context.Minta.Include(c => c.cAkkrMintavetel).Include(c => c.cHUMVIfelelos).Include(c => c.cHUMVImodul).Include(c => c.cMintavevo).Include(c => c.cMvHely).Include(c => c.cMvOka).Include(c => c.cMvTipus).Include(c => c.cVizsgaloLabor);
            return View(await laborDbContext.ToListAsync());
        }

        // GET: Minta/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMinta = await _context.Minta
                .Include(c => c.cAkkrMintavetel)
                .Include(c => c.cHUMVIfelelos)
                .Include(c => c.cHUMVImodul)
                .Include(c => c.cMintavevo)
                .Include(c => c.cMvHely)
                .Include(c => c.cMvOka)
                .Include(c => c.cMvTipus)
                .Include(c => c.cVizsgaloLabor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMinta == null)
            {
                return NotFound();
            }

            return View(cMinta);
        }

        // GET: Minta/Create
        public IActionResult Create()
        {
            ViewData["AkkrMintavetel"] = new SelectList(_context.AkkrMintavetel, "Id", "AkkrMintavetelStatusz");
            ViewData["Felelos"] = new SelectList(_context.HumviFelelos, "Id", "Cim");
            ViewData["ModulKod"] = new SelectList(_context.HumviModul, "Id", "Leiras");
            ViewData["Mintavevo"] = new SelectList(_context.Mintavevo, "Id", "Cim");
            ViewData["MvhKod"] = new SelectList(_context.MvHely, "Id", "MvhKod");
            ViewData["MvOk"] = new SelectList(_context.MvOka, "Id", "Leiras");
            ViewData["MvTipus"] = new SelectList(_context.MvTipus, "Id", "Leiras");
            ViewData["Labor"] = new SelectList(_context.VizsgaloLabor, "Id", "Cim");
            return View();
        }

        // POST: Minta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaborMintaKod,ModulKod,Felelos,MvTipus,MvDatum,Labor,MintaAtvetel,VizsgalatKezdete,VizsgalatVege,MvOk,MvOkaEgyeb,MvhKod,MvHely,AkkrMintavetel,Mintavevo,HUMVIexport,Id,Created,LastModified")] cMinta cMinta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cMinta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AkkrMintavetel"] = new SelectList(_context.AkkrMintavetel, "Id", "AkkrMintavetelStatusz", cMinta.AkkrMintavetel);
            ViewData["Felelos"] = new SelectList(_context.HumviFelelos, "Id", "Cim", cMinta.Felelos);
            ViewData["ModulKod"] = new SelectList(_context.HumviModul, "Id", "Leiras", cMinta.ModulKod);
            ViewData["Mintavevo"] = new SelectList(_context.Mintavevo, "Id", "Cim", cMinta.Mintavevo);
            ViewData["MvhKod"] = new SelectList(_context.MvHely, "Id", "MvhKod", cMinta.MvhKod);
            ViewData["MvOk"] = new SelectList(_context.MvOka, "Id", "Leiras", cMinta.MvOk);
            ViewData["MvTipus"] = new SelectList(_context.MvTipus, "Id", "Leiras", cMinta.MvTipus);
            ViewData["Labor"] = new SelectList(_context.VizsgaloLabor, "Id", "Cim", cMinta.Labor);
            return View(cMinta);
        }

        // GET: Minta/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMinta = await _context.Minta.FindAsync(id);
            if (cMinta == null)
            {
                return NotFound();
            }

            var modulok = await _context.HumviModul.ToListAsync();
            ViewBag.ModulKod = modulok.Select(modul => new SelectListItem
                {
                    Value = modul.Id.ToString(),
                    Text = modul.Leiras 
                }).ToList();
            var felelosok = await _context.HumviFelelos.ToListAsync();
            ViewBag.Felelos = felelosok.Select(felelos => new SelectListItem
            {
                Value = felelos.Id.ToString(),
                Text = felelos.Nev 
            }).ToList();
            var mvTipusok = await _context.MvTipus.ToListAsync();
            ViewBag.MvTipus = mvTipusok.Select(mvTipus => new SelectListItem
            {
                Value = mvTipus.Id.ToString(),
                Text = mvTipus.MvTipusNev
            }).ToList();
            var laborok = await _context.VizsgaloLabor.ToListAsync();
            ViewBag.Laborok = laborok.Select(labor => new SelectListItem
            {
                Value = labor.Id.ToString(),
                Text = $"{labor.Nev} - {labor.LabAkkrSzam} (Érvényesség: {labor.ErvKezdete:yyyy-MM-dd} - {labor.ErvVege:yyyy-MM-dd})"
            }).ToList();



            ViewData["AkkrMintavetel"] = new SelectList(_context.AkkrMintavetel, "Id", "AkkrMintavetelStatusz", cMinta.AkkrMintavetel);
            //ViewData["Felelos"] = new SelectList(_context.HumviFelelos, "Id", "Cim", cMinta.Felelos);
            //ViewData["ModulKod"] = new SelectList(_context.HumviModul, "Id", "Leiras", cMinta.ModulKod);
            ViewData["Mintavevo"] = new SelectList(_context.Mintavevo, "Id", "Cim", cMinta.Mintavevo);
            ViewData["MvhKod"] = new SelectList(_context.MvHely, "Id", "MvhKod", cMinta.MvhKod);
            ViewData["MvOk"] = new SelectList(_context.MvOka, "Id", "Leiras", cMinta.MvOk);
            ViewData["MvTipus"] = new SelectList(_context.MvTipus, "Id", "Leiras", cMinta.MvTipus);
            ViewData["Labor"] = new SelectList(_context.VizsgaloLabor, "Id", "Cim", cMinta.Labor);
            return View(cMinta);
        }

        // POST: Minta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,LaborMintaKod,ModulKod,Felelos,MvTipus,MvDatum,Labor,MintaAtvetel,VizsgalatKezdete,VizsgalatVege,MvOk,MvOkaEgyeb,MvhKod,MvHely,AkkrMintavetel,Mintavevo,HUMVIexport")] MintaDto mintaDto)
        {
            if (mintaDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }
           // if (ModelState.IsValid)
            {
                try
                {
                    var minta = await _context.Minta.FirstOrDefaultAsync(x => x.Id == mintaDto.Id);
                    if (minta != null)
                    {
                        minta.LaborMintaKod = mintaDto.LaborMintaKod;
                        minta.ModulKod = mintaDto.ModulKod;
                        minta.Felelos = mintaDto.Felelos;
                        minta.MvTipus = mintaDto.MvTipus;
                        minta.MvDatum = mintaDto.MvDatum;
                        minta.Labor = mintaDto.Labor;
                        minta.MintaAtvetel = mintaDto.MintaAtvetel;
                        minta.VizsgalatKezdete = mintaDto.VizsgalatKezdete;
                        minta.VizsgalatVege = mintaDto.VizsgalatVege;
                        minta.MvOk = mintaDto.MvOk;
                        minta.MvOkaEgyeb = mintaDto.MvOkaEgyeb;
                        minta.MvhKod = mintaDto.MvhKod;
                        minta.MvHely = mintaDto.MvHely;
                        minta.AkkrMintavetel = mintaDto.AkkrMintavetel;
                        minta.Mintavevo = mintaDto.Mintavevo;
                        minta.HUMVIexport = mintaDto.HUMVIexport;                      
                        minta.LastModified = DateTime.UtcNow;
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
/*            ViewData["AkkrMintavetel"] = new SelectList(_context.AkkrMintavetel, "Id", "AkkrMintavetelStatusz", cMinta.AkkrMintavetel);
            ViewData["Felelos"] = new SelectList(_context.HumviFelelos, "Id", "Cim", cMinta.Felelos);
            ViewData["ModulKod"] = new SelectList(_context.HumviModul, "Id", "Leiras", cMinta.ModulKod);
            ViewData["Mintavevo"] = new SelectList(_context.Mintavevo, "Id", "Cim", cMinta.Mintavevo);
            ViewData["MvhKod"] = new SelectList(_context.MvHely, "Id", "MvhKod", cMinta.MvhKod);
            ViewData["MvOk"] = new SelectList(_context.MvOka, "Id", "Leiras", cMinta.MvOk);
            ViewData["MvTipus"] = new SelectList(_context.MvTipus, "Id", "Leiras", cMinta.MvTipus);
            ViewData["Labor"] = new SelectList(_context.VizsgaloLabor, "Id", "Cim", cMinta.Labor);*/
            return View(mintaDto);
        }

        // GET: Minta/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMinta = await _context.Minta
                .Include(c => c.cAkkrMintavetel)
                .Include(c => c.cHUMVIfelelos)
                .Include(c => c.cHUMVImodul)
                .Include(c => c.cMintavevo)
                .Include(c => c.cMvHely)
                .Include(c => c.cMvOka)
                .Include(c => c.cMvTipus)
                .Include(c => c.cVizsgaloLabor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMinta == null)
            {
                return NotFound();
            }

            return View(cMinta);
        }

        // POST: Minta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cMinta = await _context.Minta.FindAsync(id);
            if (cMinta != null)
            {
                _context.Minta.Remove(cMinta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cMintaExists(long id)
        {
            return _context.Minta.Any(e => e.Id == id);
        }
    }
}
