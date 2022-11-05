using System;
using System.ComponentModel.DataAnnotations;

namespace WindowsReportingClient
{
    public class FileHeaders
    {
        [Required]
        public string FileName { get; set; } = default!;

        [Required]
        public long Lenth { get; set; }

        public string Extension { get; set; } = default!;

        [Required]
        public string SecretKey { get; set; } = default!;
    }
}