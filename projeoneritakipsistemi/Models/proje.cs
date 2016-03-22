using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class proje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proje()
        {
            this.grups = new HashSet<grup>();
            this.kaynaks = new HashSet<kaynak>();
            this.yorums = new HashSet<yorum>();
            this.begenmes = new HashSet<begenme>();
        }
        [Key]
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


        public virtual akademisyen akademisyen { get; set; }

        public virtual bolum bolum { get; set; }

        public virtual diger_kullanicilar diger_kullanicilar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<grup> grups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<kaynak> kaynaks { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<yorum> yorums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<begenme> begenmes { get; set; }

      
    }
}