using Microsoft.Win32;

namespace Windows_Server_Dashboard.Reports;

internal class Startup
{
    public Startup(string? name)
    {
        Name = name;
    }

    public string? Name { get; }

    
}