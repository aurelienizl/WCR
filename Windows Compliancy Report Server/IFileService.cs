namespace Windows_Compliancy_Report_Server
{
    public interface IFileService
    {
        public FileHeaders GetFileInfo(string path);

        public string GetResoucePath();
    }
}