SELECT COUNT(Id) AS Minta FROM Minta
SELECT COUNT(Id) AS Eredmeny FROM Eredmeny

SELECT * From Minta
SELECT * From Eredmeny

SELECT m.Id, m.LaborMintaKod, HumviModul.Leiras AS HUMVImodul, HumviFelelos.Nev AS HUMVIfelelos, MvTipus.Leiras AS MvTipus, 
       VizsgaloLabor.Nev AS Labor, VizsgaloLabor.LabAkkrSzam AS LabAkkrSzam, MvOka.Leiras As MvOka, MvHely.NevSajat As MvHely_kod_alapjan, m.MvHely,
	   AkkrMintavetel.Leiras AS AkkrMintavetel, Mintavevo.Nev AS Mintavevo, Mintavevo.MvAkkrSzam AS MvAkkrSzam FROM Minta AS m
INNER JOIN HumviModul ON m.ModulKod=HumviModul.ModulKod
INNER JOIN HumviFelelos ON m.Felelos=HumviFelelos.Felelos
INNER JOIN MvTipus ON m.MvTipus=MvTipus.MvTipusNev
INNER JOIN VizsgaloLabor ON m.Labor=VizsgaloLabor.Labor AND m.LabAkkrSzam = VizsgaloLabor.LabAkkrSzam
INNER JOIN MvOka ON m.MvOk = MvOka.MvOk
INNER JOIN MvHely ON m.MvhKod = MvHely.MvhKod
INNER JOIN AkkrMintavetel ON m.AkkrMintavetel = AkkrMintavetel.AkkrMintavetelStatusz
INNER JOIN Mintavevo ON m.Mintavevo=Mintavevo.MintavevoAzonosito AND m.MvAkkrSzam = Mintavevo.MvAkkrSzam

SELECT Eredmeny.Id, Eredmeny.MintaId, Eredmeny.ParKod, Parameter.SajatLeiras, Eredmeny.Megyseg, Mertekegyseg.SajatLeiras, Eredmeny.Ertek, Eredmeny.AlsoMh, Eredmeny.MaxRange, Eredmeny.ErtekHozzarendelt, Eredmeny.Created, Eredmeny.LastModified FROM Eredmeny
INNER JOIN Parameter ON Eredmeny.ParKod=Parameter.ParKod
INNER JOIN Mertekegyseg ON Eredmeny.Megyseg=Mertekegyseg.Megyseg;