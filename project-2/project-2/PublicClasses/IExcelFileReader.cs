namespace project_2;

public interface IExcelFileReader
{
    FileInfo ExcelFileUploader();

    Dictionary<string, int> HeaderCols(Stream file, string sheetName, int headerRow);

    Dictionary<string, int> HeaderCols(FileInfo fileInfo, string sheetName, int headerRow);

    void ProcessLines(List<List<string>> sheetData, Dictionary<string, int> headerCols);

    List<List<string>> ReadExcelSheet(Stream file, string sheetName, Dictionary<string, int> headerCols);

    List<List<string>> ReadExcelSheet(FileInfo fileInfo, string sheetName, Dictionary<string, int> headerCols);
}