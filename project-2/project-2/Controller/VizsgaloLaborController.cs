using Domain;
using Microsoft.AspNetCore.Mvc;
using project_2.Dtos;

namespace project_2.Controllers
{
    public class VizsgaloLaborController : Controller
    {
        private readonly LaborDbContext _context;

        public VizsgaloLaborController(LaborDbContext context)
        {
            _context = context;
        }

        // GET: VizsgaloLabor
        public async Task<IActionResult> Index()
        {
            return View(await _context.VizsgaloLabor.ToListAsync());
        }

        // GET: VizsgaloLabor/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVizsgaloLabor = await _context.VizsgaloLabor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cVizsgaloLabor == null)
            {
                return NotFound();
            }

            return View(cVizsgaloLabor);
        }

        // GET: VizsgaloLabor/Create
        public IActionResult Create()
        {
            return View(new cVizsgaloLabor());
        }

        // POST: VizsgaloLabor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Labor,LabAkkrSzam,Nev,Cim,ErvKezdete,ErvVege")] VizsgaloLaborDto vizsgaloLaborDto)
        {
             if (ModelState.IsValid)
            {
                var now = DateTime.UtcNow;
                var newVizsgaloLabor = new cVizsgaloLabor
                {
                    Labor = vizsgaloLaborDto.Labor,
                    LabAkkrSzam = vizsgaloLaborDto.LabAkkrSzam,
                    Nev = vizsgaloLaborDto.Nev,
                    Cim = vizsgaloLaborDto.Cim,
                    ErvKezdete = vizsgaloLaborDto.ErvKezdete,
                    ErvVege = vizsgaloLaborDto.ErvVege,
                    Created = now,
                    LastModified = now
                };
                _context.Add(newVizsgaloLabor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vizsgaloLaborDto);
        }

        // GET: VizsgaloLabor/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVizsgaloLabor = await _context.VizsgaloLabor.FindAsync(id);
            if (cVizsgaloLabor == null)
            {
                return NotFound();
            }
            return View(cVizsgaloLabor);
        }

        // POST: VizsgaloLabor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Labor,LabAkkrSzam,Nev,Cim,ErvKezdete,ErvVege")] VizsgaloLaborDto vizsgaloLaborDto)
        {
            if (vizsgaloLaborDto.Id == null)
            {
                return BadRequest("Id is missing!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var vizsgaloLabor = await _context.VizsgaloLabor.FirstOrDefaultAsync(x => x.Id == vizsgaloLaborDto.Id);
                    if (vizsgaloLabor != null)
                    {
                        vizsgaloLabor.Labor = vizsgaloLaborDto.Labor;
                        vizsgaloLabor.LabAkkrSzam = vizsgaloLaborDto.LabAkkrSzam;
                        vizsgaloLabor.Nev = vizsgaloLaborDto.Nev;
                        vizsgaloLabor.Cim = vizsgaloLaborDto.Cim;
                        vizsgaloLabor.ErvKezdete = vizsgaloLaborDto.ErvKezdete;
                        vizsgaloLabor.ErvVege = vizsgaloLaborDto.ErvVege;
                        vizsgaloLabor.LastModified = DateTime.UtcNow;
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
            return View(vizsgaloLaborDto);

        }

        // GET: VizsgaloLabor/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVizsgaloLabor = await _context.VizsgaloLabor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cVizsgaloLabor == null)
            {
                return NotFound();
            }

            return View(cVizsgaloLabor);
        }

        // POST: VizsgaloLabor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cVizsgaloLabor = await _context.VizsgaloLabor.FindAsync(id);
            if (cVizsgaloLabor != null)
            {
                _context.VizsgaloLabor.Remove(cVizsgaloLabor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cVizsgaloLaborExists(long id)
        {
            return _context.VizsgaloLabor.Any(e => e.Id == id);
        }
    }
}
