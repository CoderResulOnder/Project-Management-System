using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class grup
    {

        public grup()
        {
            this.ogrencis = new HashSet<ogrenci>();
        }
        [Key]
        public int grup_id { get; set; }
        [Required]
        public string grup_adi { get; set; }
        [Required]
        public Nullable<int> grup_uye_sayisi { get; set; }
      
        public Nullable<int> grup_proje_id { get; set; }
        public Nullable<int> grub_akademisyen_id { get; set; }
        public virtual akademisyen akademisyen { get; set; }
        public virtual proje proje { get; set; }
        public virtual ICollection<ogrenci> ogrencis { get; set; }
    }
}