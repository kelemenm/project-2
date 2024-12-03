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
using Microsoft.IdentityModel.Tokens;

namespace project_2.Controllers
{
    public class MintaController : Controller
    {
        private readonly LaborDbContext _context;
        private readonly IConfiguration _configuration;

        public MintaController(LaborDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, string? mvhKod, string? vizBazis, string? telepules, string? mvOk, string? modulKod, string? humviexport)
        {


            var mvHelyek = await _context.MvHely.ToListAsync();
            ViewBag.MvhKodList = mvHelyek.Select(mvHely => new SelectListItem
            {
                Value = mvHely.Id.ToString(),
                Text = $" {mvHely.MvhKod} - {mvHely.NevSajat}"
            }).ToList();
            ViewBag.VizBazisList = await _context.MvHely.Select(h => h.VizBazis).Distinct().ToListAsync();
            ViewBag.TelepulesList = await _context.MvHely.Select(h => h.Telepules).Distinct().ToListAsync();
            ViewBag.MvOkList = await _context.MvOka.Select(o => o.MvOk).Distinct().ToListAsync();
            ViewBag.ModulKodList = await _context.HumviModul.Select(m => m.ModulKod).Distinct().ToListAsync();

            ViewBag.maxSelectableItems = _configuration["Settings:maxSelectableItems"];

            var laborDbContext = _context.Minta
                .Include(c => c.cAkkrMintavetel)
                .Include(c => c.cHUMVIfelelos)
                .Include(c => c.cHUMVImodul)
                .Include(c => c.cMintavevo)
                .Include(c => c.cMvHely)
                .Include(c => c.cMvOka)
                .Include(c => c.cMvTipus)
                .Include(c => c.cVizsgaloLabor)
                .AsQueryable();

            // Szűrési feltételek
            
            if (startDate.HasValue)
            {
                laborDbContext = laborDbContext.Where(m => m.MvDatum >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                laborDbContext = laborDbContext.Where(m => m.MvDatum <= endDate.Value);
            }
            if (!string.IsNullOrEmpty(mvhKod))
            {
                laborDbContext = laborDbContext.Where(m => m.cMvHely.Id.ToString() == mvhKod);
            }
            if (!string.IsNullOrEmpty(vizBazis))
            {
                laborDbContext = laborDbContext.Where(m => m.cMvHely.VizBazis.Contains(vizBazis));
            }
            if (!string.IsNullOrEmpty(telepules))
            {
                laborDbContext = laborDbContext.Where(m => m.cMvHely.Telepules.Contains(telepules));
            }
            if (!string.IsNullOrEmpty(mvOk))
            {
                laborDbContext = laborDbContext.Where(m => m.cMvOka.MvOk.Contains(mvOk));
            }
            if (!string.IsNullOrEmpty(modulKod))
            {
                laborDbContext = laborDbContext.Where(m => m.cHUMVImodul.ModulKod.Contains(modulKod));
            }
            if (!string.IsNullOrEmpty(humviexport))
            {
                bool humviExportBool = humviexport.ToLower() == "true";
                laborDbContext = laborDbContext.Where(m => m.HUMVIexport == humviExportBool);
            }

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
                .Include(c => c.Eredmenyek)
                    .ThenInclude(e => e.Parameter)
                .Include(c => c.Eredmenyek)
                    .ThenInclude(e => e.Mertekegyseg)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMinta == null)
            {
                return NotFound();
            }

            return View(cMinta);
        }


        // GET: Minta/Create
        public async Task<IActionResult> Create()
        {
            var modulok = await _context.HumviModul.ToListAsync();
            ViewBag.ModulKod = modulok.Select(modul => new SelectListItem
            {
                Value = modul.Id.ToString(),
                Text = $" {modul.ModulKod} - {modul.Leiras}"
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
                Text = $" {mvTipus.MvTipusNev} - {mvTipus.Leiras}"
            }).ToList();
            var laborok = await _context.VizsgaloLabor.ToListAsync();
            ViewBag.Laborok = laborok.Select(labor => new SelectListItem
            {
                Value = labor.Id.ToString(),
                Text = $"{labor.Nev} - {labor.LabAkkrSzam} (Érvényesség: {labor.ErvKezdete:yyyy-MM-dd} - {labor.ErvVege:yyyy-MM-dd})"
            }).ToList();
            var mvOkok = await _context.MvOka.ToListAsync();
            ViewBag.MvOka = mvOkok.Select(mvOk => new SelectListItem
            {
                Value = mvOk.Id.ToString(),
                Text = $" {mvOk.MvOk} - {mvOk.Leiras}"
            }).ToList();
            var mvHelyek = await _context.MvHely.ToListAsync();
            ViewBag.MvhKod = mvHelyek.Select(mvHely => new SelectListItem
            {
                Value = mvHely.Id.ToString(),
                Text = $" {mvHely.MvhKod} - {mvHely.NevSajat}"
            }).ToList();
            var akkMintavetelStatuszok = await _context.AkkrMintavetel.ToListAsync();
            ViewBag.AkkrMintavetel = akkMintavetelStatuszok.Select(akkMintavetelStatusz => new SelectListItem
            {
                Value = akkMintavetelStatusz.Id.ToString(),
                Text = $" {akkMintavetelStatusz.AkkrMintavetelStatusz} - {akkMintavetelStatusz.Leiras}"
            }).ToList();
            var mintavevok = await _context.Mintavevo.ToListAsync();
            ViewBag.Mintavevok = mintavevok.Select(mintavevo => new SelectListItem
            {
                Value = mintavevo.Id.ToString(),
                Text = $"{mintavevo.Nev} - {mintavevo.MvAkkrSzam} (Érvényesség: {mintavevo.ErvKezdete:yyyy-MM-dd} - {mintavevo.ErvVege:yyyy-MM-dd})"
            }).ToList();

            return View(new cMinta());
        }

        // POST: Minta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaborMintaKod,ModulKod,Felelos,MvTipus,MvDatum,Labor,MintaAtvetel,VizsgalatKezdete,VizsgalatVege,MvOk,MvOkaEgyeb,MvhKod,MvHely,AkkrMintavetel,Mintavevo,HUMVIexport")] MintaDto mintaDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newMinta = new cMinta
                {
                    LaborMintaKod = mintaDto.LaborMintaKod,
                    ModulKod = mintaDto.ModulKod,
                    Felelos = mintaDto.Felelos,
                    MvTipus = mintaDto.MvTipus,
                    MvDatum = mintaDto.MvDatum,
                    Labor = mintaDto.Labor,
                    MintaAtvetel = mintaDto.MintaAtvetel,
                    VizsgalatKezdete = mintaDto.VizsgalatKezdete,
                    VizsgalatVege = mintaDto.VizsgalatVege,
                    MvOk = mintaDto.MvOk,
                    MvOkaEgyeb = mintaDto.MvOkaEgyeb,
                    MvhKod = mintaDto.MvhKod,
                    MvHely = mintaDto.MvHely,
                    AkkrMintavetel = mintaDto.AkkrMintavetel,
                    Mintavevo = mintaDto.Mintavevo,
                    HUMVIexport = mintaDto.HUMVIexport,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newMinta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mintaDto);
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
                    Text = $" {modul.ModulKod} - {modul.Leiras}"
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
                Text = $" {mvTipus.MvTipusNev} - {mvTipus.Leiras}"
            }).ToList();
            var laborok = await _context.VizsgaloLabor.ToListAsync();
            ViewBag.Laborok = laborok.Select(labor => new SelectListItem
            {
                Value = labor.Id.ToString(),
                Text = $"{labor.Nev} - {labor.LabAkkrSzam} (Érvényesség: {labor.ErvKezdete:yyyy-MM-dd} - {labor.ErvVege:yyyy-MM-dd})"
            }).ToList();
            var mvOkok = await _context.MvOka.ToListAsync();
            ViewBag.MvOka = mvOkok.Select(mvOk => new SelectListItem
            {
                Value = mvOk.Id.ToString(),
                Text = $" {mvOk.MvOk} - {mvOk.Leiras}"
            }).ToList();
            var mvHelyek = await _context.MvHely.ToListAsync();
            ViewBag.MvhKod = mvHelyek.Select(mvHely => new SelectListItem
            {
                Value = mvHely.Id.ToString(),
                Text = $" {mvHely.MvhKod} - {mvHely.NevSajat}"
            }).ToList();
            var akkMintavetelStatuszok = await _context.AkkrMintavetel.ToListAsync();
            ViewBag.AkkrMintavetel = akkMintavetelStatuszok.Select(akkMintavetelStatusz => new SelectListItem
            {
                Value = akkMintavetelStatusz.Id.ToString(),
                Text = $" {akkMintavetelStatusz.AkkrMintavetelStatusz} - {akkMintavetelStatusz.Leiras}"
            }).ToList();
            var mintavevok = await _context.Mintavevo.ToListAsync();
            ViewBag.Mintavevok = mintavevok.Select(mintavevo => new SelectListItem
            {
                Value = mintavevo.Id.ToString(),
                Text = $"{mintavevo.Nev} - {mintavevo.MvAkkrSzam} (Érvényesség: {mintavevo.ErvKezdete:yyyy-MM-dd} - {mintavevo.ErvVege:yyyy-MM-dd})"
            }).ToList();

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
            if (ModelState.IsValid)
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

        //Minták exportálása
        [HttpPost]
        public IActionResult Export(List<long> selectedIds)
        {
            if (selectedIds == null || !selectedIds.Any())
            {
                TempData["Error"] = "Nincs kiválasztott minta az exportáláshoz.";
                return RedirectToAction("Index");
            }


            var kiválasztottMinták = _context.Minta
                .Include(c => c.cAkkrMintavetel)
                .Include(c => c.cHUMVIfelelos)
                .Include(c => c.cHUMVImodul)
                .Include(c => c.cMintavevo)
                .Include(c => c.cMvHely)
                .Include(c => c.cMvOka)
                .Include(c => c.cMvTipus)
                .Include(c => c.cVizsgaloLabor)
                .Include(c => c.Eredmenyek)
                    .ThenInclude(e => e.Parameter)
                .Include(c => c.Eredmenyek)
                    .ThenInclude(e => e.Mertekegyseg)
                .AsQueryable()
                .Where(m => selectedIds.Contains(m.Id))
                .ToList();

            return View("ExportResult", kiválasztottMinták);
        }
    }
}
