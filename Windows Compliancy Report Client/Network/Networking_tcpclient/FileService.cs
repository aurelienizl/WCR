namespace Windows_Compliancy_Report_Client;

public class FileService : IFileService
{
    public FileHeaders GetFileInfo(string path)
    {
        try
        {
            var fileHeaders = new FileHeaders();
            var fileInfo = new FileInfo(path);

            fileHeaders.FileName = fileInfo.Name;
            fileHeaders.Lenth = fileInfo.Length;
            fileHeaders.Extension = fileInfo.Extension;

            return fileHeaders;
        }

        catch
        {
            return new FileHeaders();
        }
    }

    public string GetResoucePath()
    {
        var path = @"D:\";
        if (Directory.Exists(path) is false) Directory.CreateDirectory(path);
        return path;
    }
}