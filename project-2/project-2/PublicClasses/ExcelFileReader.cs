using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using OfficeOpenXml;
using System.Data.SqlClient;
using Domain;
using Persistence;
using System.Linq.Expressions;
using System.ComponentModel;


public class ExcelFileReader
{
    private readonly LaborDbContext _context;

    // Konstruktor, amely megkapja a LaborDbContext példányt
    public ExcelFileReader(LaborDbContext context)
    {
        _context = context;
    }

    public FileInfo ExcelFileUploader()
    {
        string filePath = @".\Data\UploadedFiles\DB_minta_szamolas.xlsm";
        //string filePath = @".\Data\UploadedFiles\DB_minta_hosszu.xlsm";

        FileInfo file = new FileInfo(filePath);

        return file;
    }

    public long? FindIdByValue(string tableName, string columnName, string searchValue)
    {

        var entities = _context.Model.GetEntityTypes();
        foreach (var entity in entities)
        {
            Console.WriteLine(entity.Name);
        }


        // 1. Megkeresi az entitás típust a megadott táblanév alapján
        var entityType = _context.Model.FindEntityType($"Domain.{tableName}");
        if (entityType == null)
        {
            throw new InvalidOperationException($"Entity type for table {tableName} not found.");
        }

        // 2. Dinamikusan lekéri az adott DbSet-et
        var dbSetMethod = typeof(DbContext)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "Set"
                                 && m.IsGenericMethod
                                 && m.GetParameters().Length == 0);

        if (dbSetMethod == null)
        {
            throw new InvalidOperationException("Could not find the generic 'Set' method on DbContext.");
        }

        var genericDbSetMethod = dbSetMethod.MakeGenericMethod(entityType.ClrType);
        var dbSet = genericDbSetMethod.Invoke(_context, null) as IQueryable<object>;
        if (dbSet == null)
        {
            throw new InvalidOperationException($"Failed to retrieve the DbSet for {tableName}.");
        }

        // 3. Megkeresi a megfelelő oszlopot (Property-t) az entitásban
        var property = entityType.FindProperty(columnName);
        if (property == null)
        {
            throw new InvalidOperationException($"Column {columnName} not found in table {tableName}.");
        }

        // 4. Keresés a megadott érték alapján
        var parameter = Expression.Parameter(entityType.ClrType, "e");
        var propertyAccess = Expression.Property(parameter, property.Name);
        var searchValueConstant = Expression.Constant(searchValue);
        var equalsExpression = Expression.Equal(propertyAccess, searchValueConstant);

        var lambda = Expression.Lambda(equalsExpression, parameter);
        var whereMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
            .MakeGenericMethod(entityType.ClrType);

        var filteredQuery = whereMethod.Invoke(null, new object[] { dbSet, lambda });

        // 5. Lekérdezi az `Id` oszlopot az eredményből
        var idProperty = entityType.FindProperty("Id");
        if (idProperty == null)
        {
            throw new InvalidOperationException($"Id column not found in table {tableName}.");
        }

        var selectMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == "Select" && m.GetParameters().Length == 2)
            .MakeGenericMethod(entityType.ClrType, typeof(long));

        var idSelector = Expression.Lambda(Expression.Property(parameter, idProperty.Name), parameter);
        var idQuery = selectMethod.Invoke(null, new object[] { filteredQuery, idSelector });

        // 6. Az első találatot visszaadja
        var firstOrDefaultMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == "FirstOrDefault" && m.GetParameters().Length == 1)
            .MakeGenericMethod(typeof(long));

        var result = firstOrDefaultMethod.Invoke(null, new object[] { idQuery });

        return result as long?;
    }

    public long? FindIdByTwoValues(string tableName, string columnName1, string searchValue1, string columnName2, string searchValue2)
    {
        var entities = _context.Model.GetEntityTypes();

        // 1. Megkeresi az entitás típust a megadott táblanév alapján
        var entityType = _context.Model.FindEntityType($"Domain.{tableName}");
        if (entityType == null)
        {
            throw new InvalidOperationException($"Entity type for table {tableName} not found.");
        }

        // 2. Dinamikusan lekéri az adott DbSet-et
        var dbSetMethod = typeof(DbContext)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "Set"
                                 && m.IsGenericMethod
                                 && m.GetParameters().Length == 0);

        if (dbSetMethod == null)
        {
            throw new InvalidOperationException("Could not find the generic 'Set' method on DbContext.");
        }

        var genericDbSetMethod = dbSetMethod.MakeGenericMethod(entityType.ClrType);
        var dbSet = genericDbSetMethod.Invoke(_context, null) as IQueryable<object>;
        if (dbSet == null)
        {
            throw new InvalidOperationException($"Failed to retrieve the DbSet for {tableName}.");
        }

        // 3. Megkeresi a megfelelő oszlopokat (Property-ket) az entitásban
        var property1 = entityType.FindProperty(columnName1);
        if (property1 == null)
        {
            throw new InvalidOperationException($"Column {columnName1} not found in table {tableName}.");
        }

        var property2 = entityType.FindProperty(columnName2);
        if (property2 == null)
        {
            throw new InvalidOperationException($"Column {columnName2} not found in table {tableName}.");
        }

        // 4. Keresés a megadott értékek alapján
        var parameter = Expression.Parameter(entityType.ClrType, "e");
        var propertyAccess1 = Expression.Property(parameter, property1.Name);
        var propertyAccess2 = Expression.Property(parameter, property2.Name);
        var searchValueConstant1 = Expression.Constant(searchValue1);
        var searchValueConstant2 = Expression.Constant(searchValue2);
        var equalsExpression1 = Expression.Equal(propertyAccess1, searchValueConstant1);
        var equalsExpression2 = Expression.Equal(propertyAccess2, searchValueConstant2);
        var combinedExpression = Expression.AndAlso(equalsExpression1, equalsExpression2);

        var lambda = Expression.Lambda(combinedExpression, parameter);
        var whereMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
            .MakeGenericMethod(entityType.ClrType);

        var filteredQuery = whereMethod.Invoke(null, new object[] { dbSet, lambda });

        // 5. Lekérdezi az `Id` oszlopot az eredményből
        var idProperty = entityType.FindProperty("Id");
        if (idProperty == null)
        {
            throw new InvalidOperationException($"Id column not found in table {tableName}.");
        }

        var selectMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == "Select" && m.GetParameters().Length == 2)
            .MakeGenericMethod(entityType.ClrType, typeof(long));

        var idSelector = Expression.Lambda(Expression.Property(parameter, idProperty.Name), parameter);
        var idQuery = selectMethod.Invoke(null, new object[] { filteredQuery, idSelector });

        // 6. Az első találatot visszaadja
        var firstOrDefaultMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == "FirstOrDefault" && m.GetParameters().Length == 1)
            .MakeGenericMethod(typeof(long));

        var result = firstOrDefaultMethod.Invoke(null, new object[] { idQuery });

        return result as long?;
    }


    public Dictionary<string, int> HeaderCols(FileInfo fileInfo, string sheetName, int headerRow)
    {
        Dictionary<string, int> fieldToColumnMapping = new Dictionary<string, int>()
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


        List<string> rowData = new List<string>();
        List<string> colData = new List<string>();

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


    public List<List<string>> ReadExcelSheet(FileInfo fileInfo, string sheetName, Dictionary<string, int> HeaderCols)
    {
        // A változó, ami a beolvasott adatokat tárolja
        List<List<string>> sheetData = new List<List<string>>();

        // Ellenőrizzük, hogy a fájl létezik-e
        if (!fileInfo.Exists)
        {
            //Console.WriteLine("A fájl nem található.");
            return sheetData;  // Üres lista visszaadása, ha a fájl nem található
        }

        // A fájl beolvasása Excel formátumban
        using (var package = new ExcelPackage(fileInfo))
        {
            // Megkeressük a kívánt munkalapot
            var worksheet = package.Workbook.Worksheets[sheetName];

            if (worksheet == null)
            {
                //Console.WriteLine($"A '{sheetName}' nevű munkalap nem található.");
                return sheetData;  // Üres lista visszaadása, ha a munkalap nem található
            }

            // Munkalap adatainak beolvasása
            int rowCount = HeaderCols["rowLastMinta"];
            int colCount = HeaderCols["colLastParam"];

            // Minden sort beolvassuk
            for (int row = HeaderCols["headerRow"]; row <= rowCount; row++)
            {
                List<string> rowData = new List<string>();

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

    public void ProcessLines(List<List<string>> sheetData, Dictionary<string, int> HeaderCols)
    {

        List<string> rowData = new List<string>();

        // Kapcsolati string az MSSQL adatbázishoz
        string connectionString = "Server=12400corei5\\SQLEXPRESS;Database=labor_teszt_VS;User Id=tesztadmin;Password=teszt;";

        // SQL beszúró utasítás OUTPUT-tal
        string insertQuery = @"
                INSERT INTO minta (labormintakod, modulkod, felelos, mvtipus, mvdatum, labor, mintaatvetel, vizsgalatkezdete, vizsgalatvege, mvok, mvokaegyeb, mvhkod, mvhely, akkrmintavetel, mintavevo, HUMVIexport, Created, LastModified)
                OUTPUT INSERTED.Id
                VALUES (@labormintakod, @modulkod, @felelos, @mvtipus, @mvdatum, @labor, @mintaatvetel, @vizsgalatkezdete, @vizsgalatvege, @mvoka, @mvokaegyeb, @mvhkod, @mvhely, @akkrmintavetel, @mintavevo, @HUMVIexport, @Created, @LastModified)";

        // SQL beszúró utasítás eredményekre
        string insertQueryEredmeny = @"
                        INSERT INTO eredmeny (mintaID, parkod, megyseg, ertek, alsomh, maxrange, ertekhozzarendelt, Created, LastModified)
                        OUTPUT INSERTED.Id
                        VALUES (@mintaID, @parkod, @megyseg, @ertek, @alsomh, @maxrange, @ertekhozzarendelt, @Created, @LastModified)";

        for (int i = HeaderCols["rowFirstMinta"] - 2; i < HeaderCols["rowLastMinta"] - 1; i++)
        {
            rowData = sheetData[i];
            cMinta minta = new cMinta();
            minta.LaborMintaKod = rowData[HeaderCols["labormintakod"] - 1].Trim();
minta.ModulKod = FindIdByValue("cHUMVImodul", "ModulKod", rowData[HeaderCols["modulkod"] - 1].Trim());
minta.Felelos = FindIdByValue("cHUMVIfelelos", "Felelos", rowData[HeaderCols["felelos"] - 1].Trim());
            minta.MvTipus = rowData[HeaderCols["mvtipus"] - 1].Trim();

            try { minta.MvDatum = DateTime.Parse(rowData[HeaderCols["mvdatum"] - 1]); }
            catch { minta.MvDatum = DateTime.Parse("01/01/1900 00:00:01"); }

            //minta.Labor = rowData[HeaderCols["labor"] - 1].Trim();
            //minta.LabAkkrSzam = rowData[HeaderCols["labakkrszam"] - 1].Trim();
minta.Labor = FindIdByTwoValues("cVizsgaloLabor",
    "Labor", rowData[HeaderCols["labor"] - 1].Trim(),
    "LabAkkrSzam", rowData[HeaderCols["labakkrszam"] - 1].Trim());



            try { minta.MintaAtvetel = DateTime.Parse(rowData[HeaderCols["mintaatvetel"] - 1]); }
            catch { minta.MintaAtvetel = DateTime.Parse("01/01/1900 00:00:01"); }

            try { minta.VizsgalatKezdete = DateTime.Parse(rowData[HeaderCols["vizsgalatkezdete"] - 1]); }
            catch { minta.VizsgalatKezdete = DateTime.Parse("01/01/1900 00:00:01"); }

            try { minta.VizsgalatVege = DateTime.Parse(rowData[HeaderCols["vizsgalatvege"] - 1]); }
            catch { minta.VizsgalatVege = DateTime.Parse("01/01/1900 00:00:01"); }

            minta.MvOk = rowData[HeaderCols["mvoka"] - 1].Trim();
            minta.MvOkaEgyeb = rowData[HeaderCols["mvokaegyeb"] - 1].Trim();
            minta.MvhKod = rowData[HeaderCols["mvhkod"] - 1].Trim();
            minta.MvHely = rowData[HeaderCols["megnevezes"] - 1].Trim();
            
minta.AkkrMintavetel = FindIdByValue("cAkkrMintavetel", "AkkrMintavetelStatusz", rowData[HeaderCols["akkrmintavetel"] - 1].Trim());

minta.Mintavevo = FindIdByTwoValues("cMintavevo", 
    "MintavevoAzonosito", rowData[HeaderCols["mintavevo"] - 1].Trim(),
    "MvAkkrSzam", rowData[HeaderCols["mvakkrszam"] - 1].Trim());


            minta.HUMVIexport = false;

            long newMintaId = WriteMinta(minta, connectionString, insertQuery);

            for (int j = HeaderCols["colFirstParam"] - 1; j <= HeaderCols["colLastParam"] - 1; j++)
            {
                if (rowData[j].Trim() != "ures")
                {
                    cEredmeny eredmeny = new cEredmeny();

                    eredmeny.MintaId = newMintaId;
                    eredmeny.ParKod = sheetData[0][j].Trim();
                    eredmeny.Megyseg = sheetData[1][j].Trim();
                    eredmeny.Ertek = rowData[j].Trim();

                    if (eredmeny.Ertek.Substring(0, 1) == "<")
                    {
                        eredmeny.AlsoMh = Convert.ToSingle(eredmeny.Ertek.Substring(1));
                        eredmeny.MaxRange = null;
                        eredmeny.ErtekHozzarendelt = eredmeny.AlsoMh / 2;
                    }
                    else if (eredmeny.Ertek.Substring(0, 1) == ">")
                    {
                        eredmeny.AlsoMh = null;
                        eredmeny.MaxRange = Convert.ToSingle(eredmeny.Ertek.Substring(1));
                        eredmeny.ErtekHozzarendelt = eredmeny.MaxRange;
                    }
                    else
                    {
                        eredmeny.AlsoMh = null;
                        eredmeny.MaxRange = null;
                        try { eredmeny.ErtekHozzarendelt = Convert.ToSingle(eredmeny.Ertek); }
                        catch { eredmeny.ErtekHozzarendelt = null; }
                    }
                    long newEredmenyId = WriteEredmeny(eredmeny, connectionString, insertQueryEredmeny);

                }
            }

        }

    }

    public long WriteMinta(cMinta minta, string connectionString, string insertQuery)
    {
        // Adatbázis művelet
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                // Kapcsolat megnyitása
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Paraméterek beállítása
                    command.Parameters.AddWithValue("@labormintakod", minta.LaborMintaKod);
                    command.Parameters.AddWithValue("@modulkod", minta.ModulKod);
                    command.Parameters.AddWithValue("@felelos", minta.Felelos);
                    command.Parameters.AddWithValue("@mvtipus", minta.MvTipus);
                    command.Parameters.AddWithValue("@mvdatum", minta.MvDatum);
                    command.Parameters.AddWithValue("@labor", minta.Labor);
                    command.Parameters.AddWithValue("@mintaatvetel", minta.MintaAtvetel);
                    command.Parameters.AddWithValue("@vizsgalatkezdete", minta.VizsgalatKezdete);
                    command.Parameters.AddWithValue("@vizsgalatvege", minta.VizsgalatVege);
                    command.Parameters.AddWithValue("@mvoka", minta.MvOk);
                    command.Parameters.AddWithValue("@mvokaegyeb", minta.MvOkaEgyeb);
                    command.Parameters.AddWithValue("@mvhkod", minta.MvhKod);
                    command.Parameters.AddWithValue("@mvhely", minta.MvHely);
                    command.Parameters.AddWithValue("@akkrmintavetel", minta.AkkrMintavetel);
                    command.Parameters.AddWithValue("@mintavevo", minta.Mintavevo);                  
                    command.Parameters.AddWithValue("@HUMVIexport", minta.HUMVIexport);
                    command.Parameters.AddWithValue("@Created", DateTime.Now);
                    command.Parameters.AddWithValue("@LastModified", DateTime.Now);

                    // Az OUTPUT INSERTED.Id segítségével visszakapjuk a beszúrt ID-t
                    long newMinta = (long)command.ExecuteScalar();
                    Console.WriteLine("====================================================================================");
                    Console.WriteLine("Új minta sikeresen beszúrva...");
                    Console.WriteLine($"A beszúrt sor ID-ja: {newMinta}");
                    return newMinta;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sikertelen minta rögzítés...");
                Console.WriteLine("Hiba történt: " + ex.Message);
                return -1;
            }
        }

    }

    public long WriteEredmeny(cEredmeny eredmeny, string connectionString, string insertQuery)
    {
        // Adatbázis művelet
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                // Kapcsolat megnyitása
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Paraméterek beállítása
                    command.Parameters.AddWithValue("@mintaID", eredmeny.MintaId);
                    command.Parameters.AddWithValue("@parkod", eredmeny.ParKod);
                    command.Parameters.AddWithValue("@megyseg", eredmeny.Megyseg);
                    command.Parameters.AddWithValue("@ertek", eredmeny.Ertek);
                    command.Parameters.AddWithValue("@alsomh", eredmeny.AlsoMh ?? Convert.DBNull);
                    command.Parameters.AddWithValue("@maxrange", eredmeny.MaxRange ?? Convert.DBNull);
                    command.Parameters.AddWithValue("@ertekhozzarendelt", eredmeny.ErtekHozzarendelt ?? Convert.DBNull);
                    command.Parameters.AddWithValue("@Created", DateTime.Now);
                    command.Parameters.AddWithValue("@LastModified", DateTime.Now);

                    // Az OUTPUT INSERTED.Id segítségével visszakapjuk a beszúrt ID-t
                    long newEredmeny = (long)command.ExecuteScalar();
                    Console.WriteLine("====================================================================================");
                    Console.WriteLine("Új eredmény sikeresen beszúrva...");
                    Console.WriteLine($"A beszúrt sor ID-ja: {newEredmeny}");
                    return newEredmeny;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sikertelen eredmény rögzítés...");
                Console.WriteLine("Hiba történt: " + ex.Message);
                return -1;
            }
        }

    }

}



