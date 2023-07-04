using mesaj_portali.Models;
using mesaj_portali.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mesaj_portali.Controllers
{
    public class ServisController : ApiController
    {

        Database1Entities1 db = new Database1Entities1();
        SonucModel sonuc = new SonucModel();

        //-----------------------------------------------------------------
        #region üyeler
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeAdSoyad = x.uyeAdSoyad,
                uyeSifre = x.uyeSifre,
                uyeMail = x.uyeMail

            }).ToList();
            return liste;
        }
        //-----------------------------------------------------------------
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeAdSoyad = x.uyeAdSoyad,
                uyeSifre = x.uyeSifre,
                uyeMail = x.uyeMail
            }).SingleOrDefault();
            return kayit;
        }
        //-----------------------------------------------------------------
        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            Uye yeni = new Uye();
            yeni.uyeAdSoyad = model.uyeAdSoyad;
            yeni.uyeMail = model.uyeMail;
            yeni.uyeSifre = model.uyeSifre;

            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }
        //-----------------------------------------------------------------
        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            kayit.uyeAdSoyad = model.uyeAdSoyad;
            kayit.uyeMail = model.uyeMail;
            kayit.uyeSifre = model.uyeSifre;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";
            return sonuc;
        }
        //-----------------------------------------------------------------
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silndi";
            return sonuc;
        }
        #endregion

        //-----------------------------------------------------------------
        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {

            Uye gonderici = db.Uye.Where(s => s.uyeId == model.gonderenId).SingleOrDefault();
            Uye alici = db.Uye.Where(s => s.uyeId == model.aliciId).SingleOrDefault();

            if (gonderici == null || alici == null)
            { 
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj gönderilemedi";
                return sonuc;
            }

            Mesaj yeni = new Mesaj();
            yeni.gonderenId = model.gonderenId;
            yeni.aliciId = model.aliciId;
            yeni.mesajSohbetId = model.mesajSohbetId;
            yeni.mesajTarih = model.mesajTarih;
            yeni.mesajText = model.mesajText;

            db.Mesaj.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Eklendi";
            return sonuc;
        }
        //-----------------------------------------------------------------
        [HttpGet]
        [Route("api/mesajliste")]
        public List<MesajModel> MesajListe()
        {
            List<MesajModel> liste = db.Mesaj.Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajTarih = x.mesajTarih,
                mesajText = x.mesajText,
                mesajSohbetId = x.mesajSohbetId,
                gonderenId = x.gonderenId,
                aliciId = x.aliciId

            }).ToList();
            return liste;
        }
        //-----------------------------------------------------------------
        [HttpGet]
        [Route("api/mesajbyıd/{mesajId}")]
        public MesajModel MesajById(int mesajId)
        {
            MesajModel kayit = db.Mesaj.Where(s => s.mesajId == mesajId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajTarih = x.mesajTarih,
                mesajText = x.mesajText,
                mesajSohbetId = x.mesajSohbetId,
                gonderenId = x.gonderenId,
                aliciId = x.aliciId

            }).SingleOrDefault();
            return kayit;
        }
        //-----------------------------------------------------------------
        [HttpGet]
        [Route("api/mesajlistebysohbetid/{sohbetId}")]
        public List<MesajModel> MesajListeBySohbetId(int sohbetId)
        {
            List<MesajModel> liste = db.Mesaj.Where(s => s.mesajSohbetId == sohbetId).Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajTarih = x.mesajTarih,
                mesajText = x.mesajText,
                mesajSohbetId = x.mesajSohbetId,
                gonderenId = x.gonderenId,
                aliciId = x.aliciId

            }).ToList();
            return liste;
        }
        //-----------------------------------------------------------------
        [HttpDelete]
        [Route("api/mesajsil/{mesajId}")]
        public SonucModel MesajSil(int mesajId)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.mesajId == mesajId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunmadı!";
                return sonuc;
            }

            db.Mesaj.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;
        }
        //-----------------------------------------------------------------

        [HttpGet]
        [Route("api/sohbetliste")]
        public List<SohbetModel> SohbetListe()
        {
            List<SohbetModel> liste = db.Sohbet.Select(x => new SohbetModel()
            {
                sohbetId = x.sohbetId,
                sohbetAdi = x.sohbetAdi,
            }).ToList();
            return liste;
        }


        [HttpPost]
        [Route("api/sohbetekle")]
        public SonucModel SohbetEkle(SohbetModel model)
        {

           
            Sohbet yeni = new Sohbet();
            yeni.sohbetAdi = model.sohbetAdi;


            db.Sohbet.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sohbet Eklendi";
            return sonuc;
        }
        //-----------------------------------------------------------------

        [HttpDelete]
        [Route("api/sohbetsil/{sohbetId}")]
        public SonucModel SohbetSil(int sohbetId)
        {
            Sohbet kayit = db.Sohbet.Where(s => s.sohbetId == sohbetId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sohbet Bulunmadı!";
                return sonuc;
            }

            db.Sohbet.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;
        }
        //-----------------------------------------------------------------

        [HttpGet]
        [Route("api/uyesohbetliste")]
        public List<UyeSohbetModel> UyeSohbetListe()
        {
            List<UyeSohbetModel> liste = db.UyeSohbet.Select(x => new UyeSohbetModel()
            {
                Id = x.Id,
                sohbetById = x.sohbetById,
                uyeById = x.uyeById,

            }).ToList();
            return liste;
        }

        [HttpPost]
        [Route("api/uyesohbetekle")]
        public SonucModel UyeSohbetEkle(UyeSohbetModel model)
        {


            UyeSohbet yeni = new UyeSohbet();
            yeni.sohbetById = model.sohbetById;
            yeni.uyeById = model.uyeById;


            db.UyeSohbet.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "üye Sohbet Eklendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesohbetsil/{uyesohbetId}")]
        public SonucModel UyeSohbetSil(int Id)
        {
            UyeSohbet kayit = db.UyeSohbet.Where(s => s.Id == Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "üye Sohbet Bulunmadı!";
                return sonuc;
            }

            db.UyeSohbet.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;
        }







    }
}
