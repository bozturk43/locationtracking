using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Sevkiyat
    {
        public int Id { get; set; }
        public string sevkiyatno { get; set; }
        public string cikisnoktasi { get; set; }
        public string varisnoktasi { get; set; }
        
        //Arac table relation
        public Arac arac { get; set; }
        public int aracId { get; set; }
        //Arac table relation

        //Sofor table relation
        public Sofor sofor { get; set; }
        public int soforId { get; set; }
        //Sofor table relation
        public string urun { get; set; }
        public string durum { get; set; }
        public bool aktifmi { get; set; }

        public List<Koordinat> Koordinatlar { get; set; }

    }
}