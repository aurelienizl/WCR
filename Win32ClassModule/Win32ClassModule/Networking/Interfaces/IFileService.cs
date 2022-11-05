namespace WindowsReportingClient
{
    public interface IFileService
    {
        public FileHeaders GetFileInfo(string path);

        public string GetResoucePath();
    }
}