using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kantar.Randstad.GPS.Entities
{
    public class FileResult
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public int NumSuccess { get; set; }
        public int NumReject { get; set; }
        public byte[] OriginalFile { get; set; }
        public byte[] ResultFile { get; set; }
    }
}