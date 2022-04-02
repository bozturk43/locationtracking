using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Koordinat
    {
        public int Id { get; set; }
        public string latitude { get; set; }
        public string longtitude { get; set; }
        public Sevkiyat sevkiyat { get; set; }
        public int sevkiyatId { get; set; }
    }
}