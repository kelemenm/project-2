using OfficeOpenXml;
using Domain;
using System.Linq.Expressions;

namespace project_2;

public class ExcelFileReader
{
    private readonly LaborDbContext _context;

    public ExcelFileReader(LaborDbContext context)
    {
        _context = context;
    }

    public FileInfo ExcelFileUploader()
    {
        //string filePath = @".\Data\UploadedFiles\DB_minta_szamolas.xlsm";
        string filePath = @".\Data\UploadedFiles\DB_minta_hosszu.xlsm";

        return new FileInfo(filePath);
    }

    public Dictionary<string, int> HeaderCols(FileInfo fileInfo, string sheetName, int headerRow)
    {
        var fieldToColumnMapping = new Dictionary<string, int>()
        {
            {"headerRow", headerRow },
            {"labormintakod", 0 },
            {"modulkod", 0 },
            {"felelos", 0 },
            {"mvtipus", 0 },
            {"mvdatum", 0 },
            {"labor", 0 },
            {"labakkrszam", 0 },
            {"mintaatvetel", 0 },
            {"vizsgalatkezdete", 0 },
            {"vizsgalatvege", 0 },
            {"mvoka", 0 },
            {"mvokaegyeb", 0 },
            {"mvhkod", 0 },
            {"megnevezes", 0 },
            {"akkrmintavetel", 0 },
            {"mintavevo", 0 },
            {"mvakkrszam", 0 },
            {"colFirstParam", 0 },
            {"colLastParam", 0 },
            {"rowFirstMinta", 0 },
            {"rowLastMinta", 0 }
        };

        // Ellenőrizzük, hogy a fájl létezik-e
        if (!fileInfo.Exists)
        {
            Console.WriteLine("A fájl nem található.");
            return fieldToColumnMapping;  // Üres dictionary visszaadása, ha a fájl nem található
        }

        var rowData = new List<string>();

        // A fájl beolvasása Excel formátumban
        using (var package = new ExcelPackage(fileInfo))
        {
            // Megkeressük a kívánt munkalapot
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var worksheet = package.Workbook.Worksheets[sheetName];

            if (worksheet == null)
            {
                Console.WriteLine($"A '{sheetName}' nevű munkalap nem található.");
                return fieldToColumnMapping;  // Üres dictionary visszaadása, ha a munkalap nem található
            }

            //adatok beolvasása
            int col = 1;
            while (worksheet.Cells[headerRow, col].Text != "vege")
            {
                var cellValue = worksheet.Cells[headerRow, col].Text;
                rowData.Add(cellValue);
                col++;
            }

            // Az oszlopszámok frissítése a rowData alapján
            for (int i = 0; i < rowData.Count; i++)
            {
                string currentElement = rowData[i];

                // Ha a rowData eleme egyezik a dictionary valamelyik kulcsával
                if (fieldToColumnMapping.ContainsKey(currentElement))
                {
                    // Az oszlopszám frissítése, figyeljünk arra, hogy a sorszámozás 1-től kezdődjön!
                    fieldToColumnMapping[currentElement] = i + 1;  // i + 1, mivel a sorszám 1-től kell induljon
                }
                // Az első paraméter oszlop COLIFORM kell hogy legyen
                if (currentElement == "COLIFORM")
                {
                    fieldToColumnMapping["colFirstParam"] = i + 1;
                }
            }
            fieldToColumnMapping["colLastParam"] = rowData.Count;

            //az első oszlop beolvasása
            //adatok beolvasása
            int row = headerRow + 1;
            while (worksheet.Cells[row, 1].Text != "vege")
            {
                var cellValue = worksheet.Cells[row, 1].Text;
                if (cellValue.Trim() == "labormintakod")
                {
                    fieldToColumnMapping["rowFirstMinta"] = row + 1;
                }
                row++;
            }
            fieldToColumnMapping["rowLastMinta"] = row - 1;

        }
        return fieldToColumnMapping;
    }


    public List<List<string>> ReadExcelSheet(FileInfo fileInfo, string sheetName, Dictionary<string, int> headerCols)
    {
        // A változó, ami a beolvasott adatokat tárolja
        var sheetData = new List<List<string>>();

        // Ellenőrizzük, hogy a fájl létezik-e
        if (!fileInfo.Exists)
        {
            Console.WriteLine("A fájl nem található.");
            return sheetData;  // Üres lista visszaadása, ha a fájl nem található
        }

        // A fájl beolvasása Excel formátumban
        using (var package = new ExcelPackage(fileInfo))
        {
            if (package.Workbook.Worksheets[sheetName] is not ExcelWorksheet worksheet)
            {
                Console.WriteLine($"A '{sheetName}' nevű munkalap nem található.");
                return sheetData;  // Üres lista visszaadása, ha a munkalap nem található
            }

            // Munkalap adatainak beolvasása
            int rowCount = headerCols["rowLastMinta"];
            int colCount = headerCols["colLastParam"];

            // Minden sort beolvassuk
            for (int row = headerCols["headerRow"]; row <= rowCount; row++)
            {
                var rowData = new List<string>();

                // Minden oszlopot beolvassuk az adott sorban
                for (int col = 1; col <= colCount; col++)
                {
                    var cellValue = worksheet.Cells[row, col].Text;
                    rowData.Add(cellValue);
                }

                // Sor adatainak hozzáadása a sheetData listához
                sheetData.Add(rowData);
            }
        }

        return sheetData;
    }

    public void ProcessLines(List<List<string>> sheetData, Dictionary<string, int> headerCols)
    {
        for (int i = headerCols["rowFirstMinta"] - 2; i < headerCols["rowLastMinta"] - 1; i++)
        {
            var rowData = sheetData[i];

            var minta = CreateMinta(rowData, headerCols);
            long newMintaId = _context.Minta.Add(minta).Entity.Id;

            for (int j = headerCols["colFirstParam"] - 1; j <= headerCols["colLastParam"] - 1; j++)
            {
                if (rowData[j].Trim() == "ures")
                {
                    continue;
                }

                var eredmeny = CreateEredmeny(sheetData, rowData, j, newMintaId);
                _context.Eredmeny.Add(eredmeny);
            }
        }

        _context.SaveChanges();
    }

    private cEredmeny CreateEredmeny(List<List<string>> sheetData, List<string> rowData, int rowIndex, long newMintaId)
    {
        var eredmeny = new cEredmeny
        {
            MintaId = newMintaId,
            ParKod = FindId<cParameter>(x => x.ParKod == sheetData[0][rowIndex].Trim()),
            Megyseg = FindId<cMertekegyseg>(x => x.Megyseg == sheetData[1][rowIndex].Trim()),
            Ertek = rowData[rowIndex].Trim(),
        };

        _ = eredmeny.Ertek[..1] switch
        {
            "<" => SetRelationRelatedProperties(
                alshMh: Convert.ToSingle(eredmeny.Ertek.Substring(1)),
                maxRange: null,
                ertek: eredmeny.AlsoMh / 2),
            ">" => SetRelationRelatedProperties(
                alshMh: null,
                maxRange: Convert.ToSingle(eredmeny.Ertek.Substring(1)),
                ertek: eredmeny.MaxRange),
            _ => SetRelationRelatedProperties(
                alshMh: null,
                maxRange: null,
                ertek: float.TryParse(eredmeny.Ertek, out float value) ? value : null),
        };

        return eredmeny;

        bool SetRelationRelatedProperties(float? alshMh, float? maxRange, float? ertek)
        {
            eredmeny.AlsoMh = alshMh;
            eredmeny.MaxRange = maxRange;
            eredmeny.ErtekHozzarendelt = ertek;

            return true;
        }
    }

    private cMinta CreateMinta(List<string> rowData, Dictionary<string, int> headerCols)
    {
        return new cMinta
        {
            LaborMintaKod = rowData[headerCols["labormintakod"] - 1].Trim(),
            MvOkaEgyeb = rowData[headerCols["mvokaegyeb"] - 1].Trim(),
            MvHely = rowData[headerCols["megnevezes"] - 1].Trim(),
            ModulKod = FindId<cHUMVImodul>(x => x.ModulKod == rowData[headerCols["modulkod"] - 1].Trim()),
            Felelos = FindId<cHUMVIfelelos>(x => x.Felelos == rowData[headerCols["felelos"] - 1].Trim()),
            MvTipus = FindId<cMvTipus>(x => x.MvTipusNev == rowData[headerCols["mvtipus"] - 1].Trim()),
            MvOk = FindId<cMvOka>(x => x.MvOk == rowData[headerCols["mvoka"] - 1].Trim()),
            MvhKod = FindId<cMvHely>(x => x.MvhKod == rowData[headerCols["mvhkod"] - 1].Trim()),
            MvDatum = GetDateTimeValueOrDefault(rowData[headerCols["mvdatum"] - 1]),
            MintaAtvetel = GetDateTimeValueOrDefault(rowData[headerCols["mintaatvetel"] - 1]),
            VizsgalatKezdete = GetDateTimeValueOrDefault(rowData[headerCols["vizsgalatkezdete"] - 1]),
            VizsgalatVege = GetDateTimeValueOrDefault(rowData[headerCols["vizsgalatvege"] - 1]),
            AkkrMintavetel = FindId<cAkkrMintavetel>(x => x.AkkrMintavetelStatusz == rowData[headerCols["akkrmintavetel"] - 1].Trim()),
            Labor = FindId<cVizsgaloLabor>(x => x.Labor == rowData[headerCols["labor"] - 1].Trim() && x.LabAkkrSzam == rowData[headerCols["labakkrszam"] - 1].Trim()),
            Mintavevo = FindId<cMintavevo>(x => x.MintavevoAzonosito == rowData[headerCols["mintavevo"] - 1].Trim() && x.MvAkkrSzam == rowData[headerCols["mvakkrszam"] - 1].Trim()),
            HUMVIexport = false
        };
    }

    public long? FindId<T>(Expression<Func<T, bool>> predicate) where T : cEntity
    {
        return _context.Set<T>().FirstOrDefault(predicate)?.Id;
    }

    private static DateTime GetDateTimeValueOrDefault(string value)
    {
        return DateTime.TryParse(value, out var result)
            ? result
            : DateTime.MinValue;
    }
}