using System.ComponentModel.DataAnnotations;

namespace Windows_Compliancy_Report_Client.Network.Networking_tcpclient;

public class FileHeaders
{
    [Required] public string FileName { get; set; } = default!;

    [Required] public long Lenth { get; set; }

    public string Extension { get; set; } = default!;

    [Required] public string SecretKey { get; set; } = default!;
}