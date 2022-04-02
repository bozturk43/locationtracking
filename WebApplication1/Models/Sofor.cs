using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Sofor
    {
        public int Id { get; set; }
        public string soforNo { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string fotograf { get; set; }
        public string kimlikno { get; set; }
        public string ehliyetno { get; set; }
        public string ehliyettipi { get; set; }
        public string telefonno  { get; set; }
        public string email { get; set; }
        public string sehir { get; set; }
        public string ulke { get; set; }
        public string durum { get; set; }
        public bool aktifmi { get; set; }

    }
}