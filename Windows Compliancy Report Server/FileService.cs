
namespace Windows_Compliancy_Report_Server
{
    public class FileService : IFileService
    {
        public FileHeaders GetFileInfo(string path)
        {
            try
            {
                FileHeaders fileHeaders = new FileHeaders();
                FileInfo fileInfo = new FileInfo(path);

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
            string path = @"C:\reports\";

            if (Directory.Exists(path) is false)
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}