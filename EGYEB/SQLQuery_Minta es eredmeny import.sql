SELECT COUNT(Id) AS Minta FROM Minta
SELECT COUNT(Id) AS Eredmeny FROM Eredmeny

SELECT * From Minta
SELECT * From Eredmeny

SELECT m.Id, m.LaborMintaKod, HumviModul.Leiras AS HUMVImodul, HumviFelelos.Nev AS HUMVIfelelos, MvTipus.Leiras AS MvTipus, 
       VizsgaloLabor.Nev AS Labor, VizsgaloLabor.LabAkkrSzam AS LabAkkrSzam, MvOka.Leiras As MvOka, MvHely.NevSajat As MvHely_kod_alapjan, m.MvHely,
	   AkkrMintavetel.Leiras AS AkkrMintavetel, Mintavevo.Nev AS Mintavevo, Mintavevo.MvAkkrSzam AS MvAkkrSzam FROM Minta AS m
INNER JOIN HumviModul ON m.ModulKod=HumviModul.Id
INNER JOIN HumviFelelos ON m.Felelos=HumviFelelos.Id
INNER JOIN MvTipus ON m.MvTipus=MvTipus.Id
INNER JOIN VizsgaloLabor ON m.Labor=VizsgaloLabor.Id
INNER JOIN MvOka ON m.MvOk = MvOka.Id
INNER JOIN MvHely ON m.MvhKod = MvHely.Id
INNER JOIN AkkrMintavetel ON m.AkkrMintavetel = AkkrMintavetel.Id
INNER JOIN Mintavevo ON m.Mintavevo=Mintavevo.Id

SELECT Eredmeny.Id, Eredmeny.MintaId, Eredmeny.ParKod, Parameter.SajatLeiras, Eredmeny.Megyseg, Mertekegyseg.SajatLeiras, Eredmeny.Ertek, Eredmeny.AlsoMh, Eredmeny.MaxRange, Eredmeny.ErtekHozzarendelt, Eredmeny.Created, Eredmeny.LastModified FROM Eredmeny
INNER JOIN Parameter ON Eredmeny.ParKod=Parameter.Id
INNER JOIN Mertekegyseg ON Eredmeny.Megyseg=Mertekegyseg.Id;