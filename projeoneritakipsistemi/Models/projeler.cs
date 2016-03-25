using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class projeler
    {

        public int proje_id { get; set; }

        public string proje_adi { get; set; }

        public Nullable<int> proje_begeni_sayisi { get; set; }

        public DateTime proje_teslim_tarihi { get; set; }

        public string proje_durumu { get; set; }

        public string proje_aciklamasi { get; set; }

        public string proje_turu { get; set; }

        public Nullable<int> proje_kisi_siniri { get; set; }

        public string projeolusturanid { get; set; }

        public Nullable<int> ogrenci_id { get; set; }

        public Nullable<int> akademisyen_id { get; set; }

        public Nullable<int> bolum_id { get; set; }

        public Nullable<int> diger_kullanicilar_id { get; set; }

        public DateTime proje_yayin_tarihi { get; set; }

        public virtual ogrenci ogrenci { get; set; }

        public string proje_yapimiyla_ilgili_oneri { get; set; }

        public virtual akademisyen akademisyen { get; set; }

        public virtual bolum bolum { get; set; }





    }
}