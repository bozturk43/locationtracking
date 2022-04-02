using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Arac
    {
        public int Id { get; set; }
        public string aracno { get; set; }
        public string marka { get; set; }
        public string model { get; set; }
        public string plaka { get; set; }
        public string aractipi { get; set; }
        public string durum { get; set; }
        public bool aktifmi { get; set; }

    }
}