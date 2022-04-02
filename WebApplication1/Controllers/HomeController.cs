using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            
            if (db.Database.Exists() != true)
            {
                db.Database.Create();
            }

            List<Sevkiyat> sevkiyatlar = db.Sevkiyats.Where(x => x.aktifmi == true && x.durum=="Devam Ediyor").ToList();
            
            return View(sevkiyatlar);
        }

        //------------------SOFOR İSLEMLERİ BASLANGIÇ
        public ActionResult Soforyonetimi()
        {
            List<Sofor> soforler = db.Sofors.Where(x => x.aktifmi == true).ToList();
            return View(soforler);
        }
        public ActionResult Soforekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Soforekle(HttpPostedFileBase resiminput,string adinput,string soyadinput,string telefoninput,string emailinput,string kimlikinput,string ehliyetinput,string ehliyettipinput, string sehirinput,string ulkeinput)
        {
            string resimyolu = "";
            string resimad = "";
            string soforno = "";

            try
            {
                if(resiminput!=null && resiminput.ContentLength > 0)
                {
                    resimad=Guid.NewGuid().ToString() +"-"+ Path.GetFileName(resiminput.FileName);
                    resimyolu = Path.Combine(Server.MapPath("~/Content/html/default/assets/images/users"), resimad);
                    resiminput.SaveAs(resimyolu);
                }
            }
            catch { }

            Sofor eklenensofor = new Sofor();
            eklenensofor.soforNo= Guid.NewGuid().ToString("N").Substring(0, 5);
            eklenensofor.fotograf = resimad;
            eklenensofor.adi = adinput;
            eklenensofor.soyadi = soyadinput;
            eklenensofor.telefonno = telefoninput;
            eklenensofor.kimlikno = kimlikinput;
            eklenensofor.ehliyetno = ehliyetinput;
            eklenensofor.ehliyettipi = ehliyettipinput;
            eklenensofor.email = emailinput;
            eklenensofor.sehir = sehirinput;
            eklenensofor.ulke = ulkeinput;
            eklenensofor.durum = "Bosta";
            eklenensofor.aktifmi = true;
            db.Sofors.Add(eklenensofor);
            db.SaveChanges();

            Response.Redirect("~/Home/Soforyonetimi");

            return View();
        }
        
        [Route("{Routewithno}")]//İslemleri id ile değil olusturdugum guid ile yapmak için string unique bir değerle router ekledim
        public ActionResult Soforduzenle(string itemNo)
        {
            Sofor sofor = db.Sofors.Where(x => x.soforNo == itemNo).FirstOrDefault();
            return View(sofor);
        }
        [Route("{Routewithno}")]
        [HttpPost]
        public ActionResult Soforduzenle(HttpPostedFileBase resiminput, string itemNo,  string adinput, string soyadinput, string telefoninput, string emailinput, string kimlikinput, string ehliyetinput, string ehliyettipinput, string sehirinput, string ulkeinput)
        {
            Sofor duzenlenensofor = db.Sofors.Where(x => x.soforNo == itemNo).FirstOrDefault();
            string resimyolu = "";
            string resimad = "";

            try
            {
                if (resiminput != null && resiminput.ContentLength > 0)
                {
                    resimad = Guid.NewGuid().ToString() + "-" + Path.GetFileName(resiminput.FileName);
                    resimyolu = Path.Combine(Server.MapPath("~/Content/html/default/assets/images/users"), resimad);
                    resiminput.SaveAs(resimyolu);
                }
                else
                {
                    resimad= duzenlenensofor.fotograf;
                }
            }
            catch { }
            duzenlenensofor.fotograf = resimad;
            duzenlenensofor.adi = adinput;
            duzenlenensofor.soyadi = soyadinput;
            duzenlenensofor.telefonno = telefoninput;
            duzenlenensofor.kimlikno = kimlikinput;
            duzenlenensofor.ehliyetno = ehliyetinput;
            duzenlenensofor.ehliyettipi = ehliyettipinput;
            duzenlenensofor.email = emailinput;
            duzenlenensofor.sehir = sehirinput;
            duzenlenensofor.ulke = ulkeinput;
            db.SaveChanges();

            Response.Redirect("~/Home/Soforyonetimi");
            return View(duzenlenensofor);
        }
        [Route("{Routewithno}")]
        public ActionResult Soforsil(string itemNo)
        {
            Sofor silinensofor = db.Sofors.Where(x => x.soforNo == itemNo).FirstOrDefault();
            silinensofor.aktifmi = false;
            db.SaveChanges();
            Response.Redirect("~/Home/Soforyonetimi");
            return View();
        }
        //----------------------SOFOR ISLEMLERİ BİTİS

        //----------------------ARAC ISLEMLERİ BASLANGIC
        public ActionResult Aracyonetimi()
        {
            List<Arac> araclar = db.Aracs.Where(x => x.aktifmi == true).ToList();
            return View(araclar);
        }
        public ActionResult Aracekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Aracekle(string markainput,string modelinput,string plakainput, string aractipinput,string duruminput)
        {
            Arac eklenenarac = new Arac();
            eklenenarac.aracno= Guid.NewGuid().ToString("N").Substring(0, 5);
            eklenenarac.marka = markainput;
            eklenenarac.model = modelinput;
            eklenenarac.plaka = plakainput;
            eklenenarac.aractipi = aractipinput;
            eklenenarac.durum = duruminput;
            eklenenarac.aktifmi = true;
            db.Aracs.Add(eklenenarac);
            db.SaveChanges();
            Response.Redirect("~/Home/Aracyonetimi");
            return View();
        }

        [Route("{Routewithno}")]
        public ActionResult Aracduzenle(string itemNo)
        {
            Arac arac = db.Aracs.Where(x => x.aracno == itemNo).FirstOrDefault();
            return View(arac);
        }

        [Route("{Routewithno}")]
        [HttpPost]
        public ActionResult Aracduzenle(string itemNo, string markainput, string modelinput, string plakainput, string aractipinput, string duruminput)
        {
            Arac duzenlenenarac = db.Aracs.Where(x => x.aracno == itemNo).FirstOrDefault();
            
            duzenlenenarac.marka = markainput;
            duzenlenenarac.model = modelinput;
            duzenlenenarac.plaka = plakainput;
            duzenlenenarac.aractipi = aractipinput;
            duzenlenenarac.durum = duruminput;
            db.SaveChanges();

            Response.Redirect("~/Home/Aracyonetimi");
            return View(duzenlenenarac);
        }

        public ActionResult Aracsil(string itemNo)
        {
            Arac silinenarac = db.Aracs.Where(x=>x.aracno==itemNo).FirstOrDefault();
            silinenarac.aktifmi = false;
            db.SaveChanges();
            Response.Redirect("~/Home/Aracyonetimi");
            return View();
        }

        //----------------------ARAC ISLEMLERİ BİTİS


        //----------------------SEVKİYAT ISLEMLERİ BASLANGIC
        public ActionResult Sevkiyatyonetimi()
        {
            List<Sevkiyat> sevkiyatlar = db.Sevkiyats.Where(x => x.aktifmi == true).ToList();
            return View(sevkiyatlar);
        }
        [Route("{Routewithno}")]
        [HttpGet]
        public ActionResult Sevkiyatduzenle(string itemNo)
        {
            Sevkiyat sevkiyat = db.Sevkiyats.Where(x => x.sevkiyatno == itemNo).FirstOrDefault();
            return View(sevkiyat);
        }
        [HttpPost]
        public ActionResult Sevkiyatiptal(string itemNo, string duruminput)
        {
            Sevkiyat iptalsevkiyat = db.Sevkiyats.Where(x => x.sevkiyatno == itemNo).FirstOrDefault();
            iptalsevkiyat.durum = duruminput;
            db.SaveChanges();
            Response.Redirect("~/Home/Sevkiyatyonetimi");
            return View();
        }
        public ActionResult Sevkiyatekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sevkiyatekle(string cikisnoktasiinput, string varisnoktasiinput, string aracinput,string soforinput,string uruninput,string duruminput)
        {
            Sevkiyat eklenensevkiyat = new Sevkiyat();
            eklenensevkiyat.cikisnoktasi = cikisnoktasiinput;
            eklenensevkiyat.varisnoktasi = varisnoktasiinput;
            eklenensevkiyat.aracId = Convert.ToInt32(aracinput);
            eklenensevkiyat.soforId = Convert.ToInt32(soforinput);
            eklenensevkiyat.urun = uruninput;
            eklenensevkiyat.durum = duruminput;
            eklenensevkiyat.aktifmi = true;
            db.Sevkiyats.Add(eklenensevkiyat);
            db.SaveChanges();
            Response.Redirect("~/Home/Sevkiyatyonetimi");
            return View();
        }

        //----------------------SEVKİYAT ISLEMLERİ BASLANGIC


        public ActionResult Koordinatekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Koordinatekle(string id,string lat,string lng)
        {
            Koordinat coord = new Koordinat();
            int cid = Convert.ToInt32(id);
            coord.sevkiyatId = cid;
            coord.latitude =  lat;
            coord.longtitude = lng;
            db.Koordinats.Add(coord);
            db.SaveChanges();
            return View();
        }
        [HttpPost]
        public JsonResult getCoords(string id)
        {
            int sevkid = Convert.ToInt32(id);
            List<Koordinat> koords = db.Koordinats.Where(x => x.sevkiyatId == sevkid).ToList();
            return Json(koords,JsonRequestBehavior.AllowGet);
        }

    }
}