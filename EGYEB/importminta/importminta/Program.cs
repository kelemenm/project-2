using System;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace ImportMinta
{
    class Program
    {
        public class MintaClass
        {
            public string labormintakod;
            public string modulkod;           
            public string felelos;
            public string mvtipus;
            public DateTime mvdatum;
            public string labor;
            public string labakkrszam;
            public DateTime mintaatvetel;
            public DateTime vizsgalatkezdete;
            public DateTime vizsgalatvege;
            public string mvoka;
            public string mvokaegyeb;
            public string mvhkod;
            public string mvhely;
            public string akkrmintavetel;
            public string mintavevo;
            public string mvakkrszam;
            public bool HUMVIexport;
        }

        public class EredmenyClass
        {
            public long mintaID;
            public string parkod;
            public string megyseg;
            public string ertek;
            public double? alsomh = null;
            public double? maxrange = null;
            public double? ertekhozzarendelt = null;
        }

        
        static void Main(string[] args)
        {
/*            //error log file
            string pathErrorLog = @"C:\MEGOSZTAS\Project-2_saját\error_log.txt";
            File.WriteAllText(pathErrorLog, "Importálás logfile...");
            File.AppendAllText(pathErrorLog, DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss"));
*/

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\MEGOSZTAS\Project-2_saját\DB_minta_szamolas.xlsm");
            //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\MEGOSZTAS\Project-2_saját\DB_minta_hosszu.xlsm");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets["HUMVI"];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //fejlécsort végig olvasva importálando mezők oszlopsorszámainak meghatározása
            //olvasás vége: "vege" mezőnél
            //excel is not zero based!!
            int fejlecsor = 2;
            int col = 1;
            int colLabormintakod = -1, colModulkod = -1, colFelelos = -1, colMvtipus = -1, colMvdatum = -1, colLabor = -1, colLabakkrszam = -1;
            int colMintaatvetel = -1, colVizsgalatkezdete = -1, colVizsgalatvege = -1, colMvoka = -1, colMvokaegyeb = -1, colMvhkod = -1;
            int colMvhely = -1, colAkkrmintavetel = -1, colMintavevo = -1, colMvakkrszam = -1, colFirstParam = -1, colLastParam = -1;
            do
            {
                //Console.WriteLine(xlRange.Cells[fejlecsor, col].Value2.ToString());
                switch (xlRange.Cells[fejlecsor, col].Value2.ToString())
                {
                    case "labormintakod":
                        colLabormintakod = col;
                        break;
                    case "modulkod":
                        colModulkod = col;
                        break;
                    case "felelos":
                        colFelelos = col;
                        break;
                    case "mvtipus":
                        colMvtipus = col;
                        break;
                    case "mvdatum":
                        colMvdatum = col;
                        break;
                    case "labor":
                        colLabor = col;
                        break;
                    case "labakkrszam":
                        colLabakkrszam = col;
                        break;
                    case "mintaatvetel":
                        colMintaatvetel = col;
                        break;
                    case "vizsgalatkezdete":
                        colVizsgalatkezdete = col;
                        break;
                    case "vizsgalatvege":
                        colVizsgalatvege = col;
                        break;
                    case "mvoka":
                        colMvoka = col;
                        break;
                    case "mvokaegyeb":
                        colMvokaegyeb = col;
                        break;
                    case "mvhkod":
                        colMvhkod = col;
                        break;
                    case "megnevezes":
                        colMvhely = col;
                        break;
                    case "akkrmintavetel":
                        colAkkrmintavetel = col;
                        break;
                    case "mintavevo":
                        colMintavevo = col;
                        break;
                    case "mvakkrszam":
                        colMvakkrszam = col;
                        break;
                    case "COLIFORM":
                        colFirstParam = col;
                        break;
                }
                col = col + 1;
            } while (xlRange.Cells[fejlecsor, col].Value2.ToString() != "vege");
            colLastParam = col - 1;

            //első oszlopot olvassa végig fejlécsort követő sortól az importálando sorok sorszámainak meghatározásához
            //olvasás vége: "vege" mezőnél
            int row = fejlecsor + 1;
            int rowFirstMinta = -1, rowLastMinta = -1;
            do
            {
                //Console.WriteLine(xlRange.Cells[row, 1].Value2.ToString() + "\t");           
                if (xlRange.Cells[row, 1].Value2.ToString().Trim() == "labormintakod")
                { 
                    rowFirstMinta = row+1;
                }
                row = row + 1;
            } while (xlRange.Cells[row, 1].Value2.ToString() != "vege");
            rowLastMinta = row-1;

            Console.WriteLine("első paraméter oszlop:" + colFirstParam);
            Console.WriteLine("utolsó paraméter oszlop:" + colLastParam);
            Console.WriteLine("első minta sor:" + rowFirstMinta);
            Console.WriteLine("utolsó minta sor:" + rowLastMinta);
            //                               rowLastMinta
            for (int i = rowFirstMinta; i <= rowLastMinta; i++)
            {
                long mintaId = 0;
                
                var minta = new MintaClass();
                minta.labormintakod = xlRange.Cells[i, colLabormintakod].Value2.ToString().Trim();
                minta.modulkod = xlRange.Cells[i, colModulkod].Value2.ToString().Trim();
                minta.felelos = xlRange.Cells[i, colFelelos].Value2.ToString().Trim();
                minta.mvtipus = xlRange.Cells[i, colMvtipus].Value2.ToString().Trim();

                try { minta.mvdatum = xlRange.Cells[i, colMvdatum].Value; }
                catch { minta.mintaatvetel = DateTime.Parse("01/01/1900 00:00:01"); }

                minta.labor = xlRange.Cells[i, colLabor].Value2.ToString().Trim();
                minta.labakkrszam = xlRange.Cells[i,colLabakkrszam ].Value2.ToString().Trim();
                
                try { minta.mintaatvetel = xlRange.Cells[i, colMintaatvetel].Value; } 
                catch { minta.mintaatvetel = DateTime.Parse("01/01/1900 00:00:01"); }

                try { minta.vizsgalatkezdete = xlRange.Cells[i, colVizsgalatkezdete].Value; }
                catch { minta.vizsgalatkezdete = DateTime.Parse("01/01/1900 00:00:01"); }

                try { minta.vizsgalatvege = xlRange.Cells[i, colVizsgalatvege].Value; }
                catch { minta.vizsgalatvege = DateTime.Parse("01/01/1900 00:00:01"); }

                minta.mvoka = xlRange.Cells[i, colMvoka].Value2.ToString().Trim();
                minta.mvokaegyeb = xlRange.Cells[i, colMvokaegyeb].Value2.ToString().Trim();
                minta.mvhkod = xlRange.Cells[i, colMvhkod].Value2.ToString().Trim();
                minta.mvhely = xlRange.Cells[i, colMvhely].Value2.ToString().Trim();
                minta.akkrmintavetel = xlRange.Cells[i, colAkkrmintavetel].Value2.ToString().Trim();
                minta.mintavevo = xlRange.Cells[i, colMintavevo].Value2.ToString().Trim();
                minta.mvakkrszam = xlRange.Cells[i, colMvakkrszam].Value2.ToString().Trim();
                minta.HUMVIexport=false;

/*                //adatok kiírása konzolra
                Console.Write(minta.labormintakod + "\t");
                Console.Write(minta.modulkod + "\t");
                Console.Write(minta.felelos + "\t");
                Console.Write(minta.mvtipus + "\t");
                Console.Write(minta.mvdatum + "\t");
                Console.Write(minta.labor + "\t");
                Console.Write(minta.labakkrszam + "\t");
                Console.Write(minta.mintaatvetel + "\t");
                Console.Write(minta.vizsgalatkezdete + "\t");
                Console.Write(minta.vizsgalatvege + "\t");
                Console.Write(minta.mvoka + "\t");
                Console.Write(minta.mvokaegyeb + "\t");
                Console.Write(minta.mvhkod + "\t");
                Console.Write(minta.mvhely + "\t");
                Console.Write(minta.akkrmintavetel + "\t");
                Console.Write(minta.mintavevo + "\t");
                Console.Write(minta.mvakkrszam + "\t");
                Console.Write(minta.HUMVIexport.ToString() + "\t");
                Console.WriteLine();*/

                // Kapcsolati string az MSSQL adatbázishoz
                string connectionString = "Server=12400corei5\\SQLEXPRESS;Database=labor_teszt_VS;User Id=tesztadmin;Password=teszt;";

                // SQL beszúró utasítás OUTPUT-tal
                string insertQuery = @"
                INSERT INTO minta (labormintakod, modulkod, felelos, mvtipus, mvdatum, labor, labakkrszam, mintaatvetel, vizsgalatkezdete, vizsgalatvege, mvok, mvokaegyeb, mvhkod, mvhely, akkrmintavetel, mintavevo, mvakkrszam, HUMVIexport, Created, LastModified)
                OUTPUT INSERTED.Id
                VALUES (@labormintakod, @modulkod, @felelos, @mvtipus, @mvdatum, @labor, @labakkrszam, @mintaatvetel, @vizsgalatkezdete, @vizsgalatvege, @mvoka, @mvokaegyeb, @mvhkod, @mvhely, @akkrmintavetel, @mintavevo, @mvakkrszam, @HUMVIexport, @Created, @LastModified)"
                ;
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
                            command.Parameters.AddWithValue("@labormintakod", minta.labormintakod);
                            command.Parameters.AddWithValue("@modulkod", minta.modulkod);
                            command.Parameters.AddWithValue("@felelos", minta.felelos);
                            command.Parameters.AddWithValue("@mvtipus", minta.mvtipus);
                            command.Parameters.AddWithValue("@mvdatum", minta.mvdatum);
                            command.Parameters.AddWithValue("@labor", minta.labor);
                            command.Parameters.AddWithValue("@labakkrszam", minta.labakkrszam);
                            command.Parameters.AddWithValue("@mintaatvetel", minta.mintaatvetel);
                            command.Parameters.AddWithValue("@vizsgalatkezdete", minta.vizsgalatkezdete);
                            command.Parameters.AddWithValue("@vizsgalatvege", minta.vizsgalatvege);
                            command.Parameters.AddWithValue("@mvoka", minta.mvoka);
                            command.Parameters.AddWithValue("@mvokaegyeb", minta.mvokaegyeb);
                            command.Parameters.AddWithValue("@mvhkod", minta.mvhkod);
                            command.Parameters.AddWithValue("@mvhely", minta.mvhely);
                            command.Parameters.AddWithValue("@akkrmintavetel", minta.akkrmintavetel);
                            command.Parameters.AddWithValue("@mintavevo", minta.mintavevo);
                            command.Parameters.AddWithValue("@mvakkrszam", minta.mvakkrszam);
                            command.Parameters.AddWithValue("@HUMVIexport", minta.HUMVIexport);
                            command.Parameters.AddWithValue("@Created", DateTime.Now);
                            command.Parameters.AddWithValue("@LastModified", DateTime.Now);


                            // Az OUTPUT INSERTED.Id segítségével visszakapjuk a beszúrt ID-t
                            long newMinta = (long)command.ExecuteScalar();
                            mintaId = newMinta;
                            Console.WriteLine("====================================================================================");
                            Console.WriteLine("Új minta sikeresen beszúrva...Sor: "+ i);
                            Console.WriteLine($"A beszúrt sor ID-ja: {newMinta}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Sikertelen minta rögzítés... Sor: " + i);
                        Console.WriteLine("Hiba történt: " + ex.Message);
/*                        File.AppendAllText(pathErrorLog, "Sikertelen minta rögzítés... Sor: " + i);
                        File.AppendAllText(pathErrorLog, ex.Message);*/
                    }
                }

                /*                //mintához tartozó eredmények kiírása
                                for (int j = colFirstParam; j <= colLastParam; j++) 
                                {
                                    if (xlRange.Cells[i, j].Value2.ToString().Trim() != "ures")
                                    {
                                        Console.WriteLine(xlRange.Cells[i, j].Value2.ToString() + "\t");           
                                    }
                                }*/

                //mintához tartozó eredmények kiírása
                for (int j = colFirstParam; j <= colLastParam; j++) 
                {
                    if (xlRange.Cells[i, j].Value2.ToString().Trim() != "ures")
                    {
                        var eredmeny = new EredmenyClass();
                        eredmeny.mintaID = mintaId;
                        eredmeny.parkod = xlRange.Cells[fejlecsor, j].Value2.ToString().Trim();
                        eredmeny.megyseg = xlRange.Cells[fejlecsor+1, j].Value2.ToString().Trim();
                        eredmeny.ertek = xlRange.Cells[i, j].Value2.ToString().Trim();
                        if (eredmeny.ertek.Substring(0, 1) == "<")
                        {
                            eredmeny.alsomh = Convert.ToDouble(eredmeny.ertek.Substring(1));
                        }
                        if (eredmeny.ertek.Substring(0, 1) == ">")
                        {
                            eredmeny.maxrange = Convert.ToDouble(eredmeny.ertek.Substring(1));
                        }
                        if (eredmeny.alsomh.HasValue)
                        {
                            eredmeny.ertekhozzarendelt = eredmeny.alsomh / 2;
                        } 
                        else if (eredmeny.maxrange.HasValue)
                        {
                            eredmeny.ertekhozzarendelt = eredmeny.maxrange;
                        }
                        else
                        {
                            eredmeny.alsomh = null;
                            eredmeny.maxrange = null;
                            
                            try { eredmeny.ertekhozzarendelt = Convert.ToDouble(eredmeny.ertek); }
                            catch { eredmeny.ertekhozzarendelt = null; }
                        }

                        /*Console.Write(eredmeny.parkod + "\t");
                        Console.Write(eredmeny.megyseg + "\t");
                        Console.Write(eredmeny.ertek + "\t");
                        Console.Write(eredmeny.alsomh + "\t");
                        Console.Write(eredmeny.maxrange + "\t");
                        Console.Write(eredmeny.ertekhozzarendelt + "\t");
                        Console.WriteLine();*/
     
                        // SQL beszúró utasítás OUTPUT-nélkül
                        string insertQueryEredmeny = @"
                        INSERT INTO eredmeny (mintaID, parkod, megyseg, ertek, alsomh, maxrange, ertekhozzarendelt, Created, LastModified)
                        VALUES (@mintaID, @parkod, @megyseg, @ertek, @alsomh, @maxrange, @ertekhozzarendelt, @Created, @LastModified)";

                        Console.WriteLine(eredmeny.ertek + "\t");
                        // Adatbázis művelet
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                // Kapcsolat megnyitása
                                connection.Open();

                                using (SqlCommand command = new SqlCommand(insertQueryEredmeny, connection))
                                {
                                    // Paraméterek beállítása
                                    command.Parameters.AddWithValue("@mintaID", eredmeny.mintaID);
                                    command.Parameters.AddWithValue("@parkod", eredmeny.parkod);
                                    command.Parameters.AddWithValue("@megyseg", eredmeny.megyseg);
                                    command.Parameters.AddWithValue("@ertek", eredmeny.ertek);
                                    command.Parameters.AddWithValue("@alsomh", eredmeny.alsomh ?? Convert.DBNull);
                                    command.Parameters.AddWithValue("@maxrange", eredmeny.maxrange ?? Convert.DBNull);
                                    command.Parameters.AddWithValue("@ertekhozzarendelt", eredmeny.ertekhozzarendelt ?? Convert.DBNull);
                                    command.Parameters.AddWithValue("@Created", DateTime.Now);
                                    command.Parameters.AddWithValue("@LastModified", DateTime.Now);

                                    int rowsAffected = command.ExecuteNonQuery();

                                    Console.WriteLine($"Sikeresen beszúrt {rowsAffected} sort.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Hiba történt: " + ex.Message);
/*                                File.AppendAllText(pathErrorLog, "Hibás eredménybeszúrás: " + eredmeny.ertek);
                                File.AppendAllText(pathErrorLog, ex.Message);*/
                            }
                        }



                    }

                }


            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }
    }
}

