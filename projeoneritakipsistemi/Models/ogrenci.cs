using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class ogrenci
    {

        public ogrenci()
        {
            this.projes = new HashSet<proje>();
        }
        [Key]
        public int ogrenci_id { get; set; }
        [Required]
        public string ogrenci_adi { get; set; }
        [Required]
        public string ogrenci_soyadi { get; set; }
        [Required]
        public string ogrenci_no { get; set; }
        [Required]
        public string ogrenci_adresi { get; set; }
        [Required]
        public string ogrenci_tel { get; set; }

        public string ogrenci_resimurl { get; set; }

        public string ogrenci_email { get; set; }
        [Required]
        public string ogrenci_tc { get; set; }

        public int ogrenci_bitirmeprojesiid { get; set; }

        public string ogrenci_kullanici_adi { get; set; }

        public string ogrenci_parola { get; set; }

        public Nullable<int> ogrenci_sinif { get; set; }

        public Nullable<int> calısma_grub_id { get; set; }

        public Nullable<int> bolum_id { get; set; }

        public Nullable<int> fakulte_id { get; set; }

        public Nullable<System.DateTime> kayit_tarihi { get; set; }

        public virtual bolum bolum { get; set; }

        public virtual fakulte fakulte { get; set; }

        public virtual grup grup { get; set; }

        public virtual ICollection<proje> projes { get; set; }

    }
}