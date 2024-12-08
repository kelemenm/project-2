using System.Xml.Linq;
using System.Globalization;
using OfficeOpenXml;

namespace project_2.Controllers;

[Route("[controller]")]
public class MintaController : Controller
{
    private readonly LaborDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IExcelFileReader _excelFileReader;

    public MintaController(
        LaborDbContext context, 
        IConfiguration configuration, 
        IExcelFileReader excelFileReader)
    {
        _context = context;
        _configuration = configuration;
        _excelFileReader = excelFileReader;
    }

    [HttpGet]
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

        var selectedIds = TempData["SelectedIds"] as string;
        if (selectedIds != null)
        {
            ViewBag.SelectedIds = selectedIds.Split(',').Select(long.Parse).ToList();
        }

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

    [HttpGet("Details/{id}")]
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


    [HttpGet("Create")]
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

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Create")]
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

    [HttpGet("Edit/{id}")]
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

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("Id,LaborMintaKod,ModulKod,Felelos,MvTipus,MvDatum,Labor,MintaAtvetel,VizsgalatKezdete,VizsgalatVege,MvOk,MvOkaEgyeb,MvhKod,MvHely,AkkrMintavetel,Mintavevo,HUMVIexport")] MintaDto mintaDto)
    {
        if (mintaDto.Id == null)
        {
            return BadRequest("Id is missing!");
        }
        if (ModelState.IsValid)
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

            return RedirectToAction("Index");
        }
        return View(mintaDto);
    }

    [HttpGet("Delete/{id}")]
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

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var cMinta = await _context.Minta.FindAsync(id);
        if (cMinta != null)
        {
            _context.Minta.Remove(cMinta);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost("Export")]
    public IActionResult Export(List<long> selectedIds, string exportAction)
    {
        if (selectedIds == null || !selectedIds.Any())
        {
            TempData["Error"] = "Nincs kiválasztott minta az exportáláshoz.";
            return RedirectToAction("Index");
        }

        // Tároljuk el a kijelölt mintákat TempData-ba, hogy később vissza tudjuk olvasni.
        TempData["SelectedIds"] = string.Join(",", selectedIds);

        var kivalasztottMintak = _context.Minta
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

        switch (exportAction)
        {
            case "preview":
                var xmlContent = DownloadXml(kivalasztottMintak);
                if (xmlContent != null && xmlContent.Length > 0)
                {
                    // XML tartalom átadása a nézetnek
                    string xmlString = System.Text.Encoding.UTF8.GetString(xmlContent.ToArray());
                    return View("ExportResult", model: xmlString);  // Az XML stringet átadjuk a nézetnek
                }
                else
                {
                    TempData["Error"] = "A fájl generálása nem sikerült.";
                    return RedirectToAction("Index");
                }
            case "xml":
                string fileName = "HUMVI_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xml";
                var xmlStream = DownloadXml(kivalasztottMintak);
                if (xmlStream != null && xmlStream.Length > 0)
                {
                    foreach (var minta in kivalasztottMintak)
                    {
                        minta.HUMVIexport = true;
                    }
                    _context.SaveChanges();
                    Response.OnCompleted(() =>
                    {
                        TempData["Success"] = "Exportálás sikeres!";
                        return Task.CompletedTask;
                    });
                    TempData.Remove("SelectedIds");
                    return File(xmlStream, "application/xml", fileName);
                }
                else
                {
                    TempData["Error"] = "A fájl generálása nem sikerült.";
                    return RedirectToAction("Index");
                }

            case "excel":
                // Excel fájl exportálása
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Minta Export");

                    // Oszlopindex inicializálása
                    int columnIndex = 1;
                    int rowIndex = 1;

                    // 1. A `cMinta` oszlopok fejlécének beállítása
                    var mintaHeaders = new[] {  "Laborkód",
                                                    "Modul",
                                                    "HUMVI Felelős",
                                                    "MvTipus",
                                                    "Mintavétel dátuma",
                                                    "Labor",
                                                    "Mintátvétel dátuma",
                                                    "Vizsgálat kezdete",
                                                    "Vizsgálat vége",
                                                    "Mv. ok kód",
                                                    "Mintavétel oka",
                                                    "Mintavétel helye",
                                                    "Vízbázis",
                                                    "Település"
                        };

                    foreach (var header in mintaHeaders)
                    {
                        worksheet.Cells[rowIndex, columnIndex].Value = header;
                        columnIndex++;
                    }

                    // 2. A `cEredmeny` oszlopok fejlécének beállítása
                    var uniqueParameters = kivalasztottMintak
                        .SelectMany(m => m.Eredmenyek)
                        .Select(e => new { e.Parameter.ParKod, e.Mertekegyseg.Megyseg })
                        .Distinct()
                        .ToList();

                    foreach (var param in uniqueParameters)
                    {
                        worksheet.Cells[rowIndex, columnIndex].Value = param.ParKod; // Első szint: ParKod
                        worksheet.Cells[rowIndex + 1, columnIndex].Value = param.Megyseg; // Második szint: Megyseg
                        columnIndex++;
                    }

                    // 3. Az adatok kitöltése
                    rowIndex = 3; // Adatok kezdősora
                    foreach (var minta in kivalasztottMintak)
                    {
                        columnIndex = 1;

                        // `cMinta` mezők hozzáadása                                                      
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.LaborMintaKod;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cHUMVImodul?.ModulKod;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cHUMVIfelelos?.Nev;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cMvTipus?.MvTipusNev;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.MvDatum.ToString("yyyy-MM-dd");
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cVizsgaloLabor?.Labor;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.MintaAtvetel.ToString("yyyy-MM-dd");
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.VizsgalatKezdete.ToString("yyyy-MM-dd");
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.VizsgalatVege.ToString("yyyy-MM-dd");
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cMvOka?.MvOk;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.MvOkaEgyeb;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cMvHely?.NevSajat == "na" ? minta.MvHely : minta.cMvHely?.NevSajat;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cMvHely?.VizBazis;
                        worksheet.Cells[rowIndex, columnIndex++].Value = minta.cMvHely?.Telepules;

                        // `cEredmeny` mezők hozzáadása
                        foreach (var param in uniqueParameters)
                        {
                            var eredmeny = minta.Eredmenyek.FirstOrDefault(e => e.Parameter.ParKod == param.ParKod);
                            if (double.TryParse(eredmeny?.Ertek?.Replace(".", ","), out double numericValue))
                            {
                                worksheet.Cells[rowIndex, columnIndex++].Value = numericValue; // Számként tároljuk
                            }
                            else
                            {
                                worksheet.Cells[rowIndex, columnIndex++].Value = eredmeny?.Ertek; // Szövegként tároljuk
                            }
                        }

                        rowIndex++;
                    }

                    // 4. Formázás: automatikus méretezés
                    worksheet.Cells.AutoFitColumns();

                    // Fájl generálása és visszaadása
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    TempData.Remove("SelectedIds");

                    string excelFileName = $"MintaExport_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelFileName);
                }


            default:
                TempData["Error"] = "Ismeretlen exportálási típus.";
                return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }

    [HttpGet("Import")]
    public IActionResult Import()
    {
        return View();
    }

    [HttpPost("Import")]
    public IActionResult Import(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nincs fájl kiválasztva!");
        }

        var stream = file.OpenReadStream();
        
        Dictionary<string, int> headerColumns = _excelFileReader.HeaderCols(stream, ExcelFileReader.SheetName, ExcelFileReader.HeaderRow);
        List<List<string>> excelData = _excelFileReader.ReadExcelSheet(stream, ExcelFileReader.SheetName, headerColumns);
        _excelFileReader.ProcessLines(excelData, headerColumns);
        
        return Ok();
    }

    public MemoryStream DownloadXml(List<cMinta> kivalasztottMintak)
    {
        XElement xmlTree = new XElement("humvi_vizminta",
            kivalasztottMintak.Select(minta =>
                new XElement("minta",
                    new XElement("mintafej",
                        new XElement("modulkod", minta.cHUMVImodul.ModulKod),
                        new XElement("felelos", minta.cHUMVIfelelos.Felelos),
                        new XElement("mvtipus", minta.cMvTipus.MvTipusNev),
                        new XElement("mvdatum", minta.MvDatum.ToString("yyyy-MM-dd")),
                        new XElement("labor", minta.cVizsgaloLabor.Labor),
                        new XElement("labormintakod", minta.LaborMintaKod),
                        new XElement("mvoka", minta.cMvOka.MvOk),
                        new XElement("mvhkod", minta.cMvHely.MvhKod)
                    ),
                    minta.Eredmenyek.Select(eredmeny =>
                        new XElement("param",
                            new XElement("parkod", eredmeny.Parameter.ParKod),
                            new XElement("ertek", eredmeny.AlsoMh.HasValue ? "MHA" : eredmeny.Ertek.Replace(".", ",")),
                            new XElement("megyseg", eredmeny.Mertekegyseg.Megyseg),
                            eredmeny.AlsoMh.HasValue ? new XElement("alsomh", eredmeny.AlsoMh.Value.ToString("0.00", new CultureInfo("hu-HU"))) : null
                        )
                    )
                )
            )
        );

        var bytes = System.Text.Encoding.UTF8.GetBytes(xmlTree.ToString());
        var fileStream = new System.IO.MemoryStream(bytes);
        return fileStream;
    }

}
