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
    public class MvHelyController : Controller
    {
        private readonly LaborDbContext _context;

        public MvHelyController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: MvHely
        public async Task<IActionResult> Index()
        {
            return View(await _context.MvHely.ToListAsync());
        }

        // GET: MvHely/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvHely = await _context.MvHely
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMvHely == null)
            {
                return NotFound();
            }

            return View(cMvHely);
        }

        // GET: MvHely/Create
        public IActionResult Create()
        {
            return View(new cMvHely());
        }

        // POST: MvHely/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MvhKod,NevSajat,NevTeljes,VizBazis,NevRovid,Telepules,Tipus,HumviRegiNev,GPS_N_Y,GPS_E_X")] MvHelyDto mvHelyDto)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newMvHely = new cMvHely
                {
                    MvhKod = mvHelyDto.MvhKod,
                    NevSajat = mvHelyDto.NevSajat,
                    NevTeljes = mvHelyDto.NevTeljes,
                    VizBazis = mvHelyDto.VizBazis,
                    NevRovid = mvHelyDto.NevRovid,
                    Telepules = mvHelyDto.Telepules,
                    Tipus = mvHelyDto.Tipus,
                    HumviRegiNev = mvHelyDto.HumviRegiNev,
                    GPS_N_Y = mvHelyDto.GPS_N_Y,
                    GPS_E_X = mvHelyDto.GPS_E_X,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newMvHely);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mvHelyDto);
        }

        // GET: MvHely/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvHely = await _context.MvHely.FindAsync(id);
            if (cMvHely == null)
            {
                return NotFound();
            }
            return View(cMvHely);
        }

        // POST: MvHely/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MvhKod,NevSajat,NevTeljes,VizBazis,NevRovid,Telepules,Tipus,HumviRegiNev,GPS_N_Y,GPS_E_X")] MvHelyDto mvHelyDto)
        {
            if (mvHelyDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mvHely = await _context.MvHely.FirstOrDefaultAsync(x => x.Id == mvHelyDto.Id);
                    if (mvHely != null)
                    {
                        mvHely.MvhKod = mvHelyDto.MvhKod;
                        mvHely.NevSajat = mvHelyDto.NevSajat;
                        mvHely.NevTeljes = mvHelyDto.NevTeljes;
                        mvHely.VizBazis = mvHelyDto.VizBazis;
                        mvHely.NevRovid = mvHelyDto.NevRovid;
                        mvHely.Telepules = mvHelyDto.Telepules;
                        mvHely.Tipus = mvHelyDto.Tipus;
                        mvHely.HumviRegiNev = mvHelyDto.HumviRegiNev;
                        mvHely.GPS_N_Y = mvHelyDto.GPS_N_Y;
                        mvHely.GPS_E_X = mvHelyDto.GPS_E_X;
                        mvHely.LastModified = DateTime.UtcNow;
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
            return View(mvHelyDto);

        }

        // GET: MvHely/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMvHely = await _context.MvHely
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMvHely == null)
            {
                return NotFound();
            }

            return View(cMvHely);
        }

        // POST: MvHely/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cMvHely = await _context.MvHely.FindAsync(id);
            if (cMvHely != null)
            {
                _context.MvHely.Remove(cMvHely);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cMvHelyExists(long id)
        {
            return _context.MvHely.Any(e => e.Id == id);
        }
    }
}
