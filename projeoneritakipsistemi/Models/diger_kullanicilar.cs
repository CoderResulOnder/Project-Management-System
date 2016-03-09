using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class diger_kullanicilar
    {
        public diger_kullanicilar()
        {
            this.projes = new HashSet<proje>();
        }
        [Key]
        public int diger_kullanicilar_id { get; set; }
        [Required]
        public string diger_kullanicilar_adi { get; set; }
        [Required]
        public string diger_kullanicilar_soyadi { get; set; }
        public string diger_kullanicilar_parola { get; set; }
        public string diger_kullanicilar_kullanici_adi { get; set; }
        public string diger_kullanicilar_mail { get; set; }
        [Required]
        public string diger_kullanicilar_unvan { get; set; }
        [Required]
        public string diger_kullanicilar_uzmanlik_alani { get; set; }
        public DateTime diger_kullanicilar_kayit_tarihi { get; set; }
        [Required]
        public string diger_kullanicilar_acıklama { get; set; }
        public virtual ICollection<proje> projes { get; set; }
    }
}