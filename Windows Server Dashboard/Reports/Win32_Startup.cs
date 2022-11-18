using Microsoft.Win32;

namespace Windows_Server_Dashboard.Reports;

internal class Win32_Startup
{
    public Win32_Startup(string? name)
    {
        Name = name;
    }

    public string? Name { get; set; }


}