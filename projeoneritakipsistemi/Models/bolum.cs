using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class bolum
    {

        public bolum()
        {
            this.akademisyens = new HashSet<akademisyen>();
            this.ogrencis = new HashSet<ogrenci>();
            this.projes = new HashSet<proje>();
        }
        [Key]
        public int bolum_id { get; set; }

        [Required]
        public string bolum_adi { get; set; }
        [Required]
        public string bolum_adresi { get; set; }
        [Required]
        public string bolum_tel { get; set; }
        [Required]
        public string bolum_faks { get; set; }
        [Required]
        public string bolum_kodu { get; set; }
        public string bolum_sitesi { get; set; }
        public Nullable<int> bol_fakulte_id { get; set; }


        public virtual ICollection<akademisyen> akademisyens { get; set; }
        public virtual fakulte fakulte { get; set; }
        public virtual ICollection<ogrenci> ogrencis { get; set; }
        public virtual ICollection<proje> projes { get; set; }
    }
}