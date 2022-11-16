namespace Windows_Compliancy_Report_Client;

public interface IFileService
{
    public FileHeaders GetFileInfo(string path);

    public string GetResoucePath();
}