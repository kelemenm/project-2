namespace project_2.Controllers;

[Route("[controller]")]
public class EredmenyController : Controller
{
    private readonly LaborDbContext _context;

    public EredmenyController(LaborDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string? mvhKod, string? vizBazis, string? telepules, string? parameterId = "309", string? parameterSearch = "")
    {
        ViewBag.ParameterList = _context.Parameter
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.SajatLeiras
            }).ToList();
        var mvHelyek = await _context.MvHely.ToListAsync();
        ViewBag.MvhKodList = mvHelyek.Select(mvHely => new SelectListItem
        {
            Value = mvHely.Id.ToString(),
            Text = $" {mvHely.MvhKod} - {mvHely.NevSajat}"
        }).ToList();
        ViewBag.VizBazisList = await _context.MvHely.Select(h => h.VizBazis).Distinct().ToListAsync();
        ViewBag.TelepulesList = await _context.MvHely.Select(h => h.Telepules).Distinct().ToListAsync();


        var eredmenyek = _context.Eredmeny
            .Include(c => c.Parameter)
            .Include(c => c.Mertekegyseg)
            .Include(c => c.Minta)
                .ThenInclude(m => m.cMvHely)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameterId))
        {
            int paramId = int.Parse(parameterId);
            eredmenyek = eredmenyek.Where(e => e.Parameter.Id == paramId);
        }

        if (!string.IsNullOrWhiteSpace(mvhKod))
        {
            eredmenyek = eredmenyek.Where(e => e.Minta.cMvHely.Id.ToString() == mvhKod);
        }
        if (!string.IsNullOrWhiteSpace(vizBazis))
        {
            eredmenyek = eredmenyek.Where(e => e.Minta.cMvHely.VizBazis.Contains(vizBazis));
        }
        if (!string.IsNullOrWhiteSpace(telepules))
        {
            eredmenyek = eredmenyek.Where(e => e.Minta.cMvHely.Telepules.Contains(telepules));
        }
        if (fromDate.HasValue)
        {
            eredmenyek = eredmenyek.Where(e => e.Minta.MvDatum >= fromDate.Value);
        }
        if (toDate.HasValue)
        {
            eredmenyek = eredmenyek.Where(e => e.Minta.MvDatum <= toDate.Value);
        }

        return View(await eredmenyek.ToListAsync());
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cEredmeny = await _context.Eredmeny
            .Include(c => c.Mertekegyseg)
            .Include(c => c.Minta)
            .Include(c => c.Parameter)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cEredmeny == null)
        {
            return NotFound();
        }

        return View(cEredmeny);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        ViewData["Megyseg"] = new SelectList(_context.Mertekegyseg, "Id", "HumviLeiras");
        ViewData["MintaId"] = new SelectList(_context.Minta, "Id", "LaborMintaKod");
        ViewData["ParKod"] = new SelectList(_context.Parameter, "Id", "HumviLeiras");

        return View();
    }

    // POST: Eredmeny/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MintaId,ParKod,Megyseg,Ertek,AlsoMh,MaxRange,ErtekHozzarendelt,Id,Created,LastModified")] cEredmeny cEredmeny)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cEredmeny);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Megyseg"] = new SelectList(_context.Mertekegyseg, "Id", "HumviLeiras", cEredmeny.Megyseg);
        ViewData["MintaId"] = new SelectList(_context.Minta, "Id", "LaborMintaKod", cEredmeny.MintaId);
        ViewData["ParKod"] = new SelectList(_context.Parameter, "Id", "HumviLeiras", cEredmeny.ParKod);

        return View(cEredmeny);
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cEredmeny = await _context.Eredmeny.FindAsync(id);
        if (cEredmeny == null)
        {
            return NotFound();
        }
        ViewData["Megyseg"] = new SelectList(_context.Mertekegyseg, "Id", "HumviLeiras", cEredmeny.Megyseg);
        ViewData["MintaId"] = new SelectList(_context.Minta, "Id", "LaborMintaKod", cEredmeny.MintaId);
        ViewData["ParKod"] = new SelectList(_context.Parameter, "Id", "HumviLeiras", cEredmeny.ParKod);

        return View(cEredmeny);
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("MintaId,ParKod,Megyseg,Ertek,AlsoMh,MaxRange,ErtekHozzarendelt,Id,Created,LastModified")] cEredmeny cEredmeny)
    {
        if (id != cEredmeny.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cEredmeny);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cEredmenyExists(cEredmeny.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["Megyseg"] = new SelectList(_context.Mertekegyseg, "Id", "HumviLeiras", cEredmeny.Megyseg);
        ViewData["MintaId"] = new SelectList(_context.Minta, "Id", "LaborMintaKod", cEredmeny.MintaId);
        ViewData["ParKod"] = new SelectList(_context.Parameter, "Id", "HumviLeiras", cEredmeny.ParKod);

        return View(cEredmeny);
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cEredmeny = await _context.Eredmeny
            .Include(c => c.Mertekegyseg)
            .Include(c => c.Minta)
            .Include(c => c.Parameter)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cEredmeny == null)
        {
            return NotFound();
        }

        return View(cEredmeny);
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var cEredmeny = await _context.Eredmeny.FindAsync(id);
        if (cEredmeny != null)
        {
            _context.Eredmeny.Remove(cEredmeny);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool cEredmenyExists(long id)
    {
        return _context.Eredmeny.Any(e => e.Id == id);
    }
}
