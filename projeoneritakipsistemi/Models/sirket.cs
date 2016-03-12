using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class sirket
    {
        public sirket()
        {
            this.projes = new HashSet<proje>();
        }


        [Key]
        public string sirket_adi { get; set; }
        [Required]
        public string sirket_adresi { get; set; }
        [Required]
        public string sirket_tel { get; set; }

        public string sirket_site { get; set; }
        [Required]
        public string sirket_mail { get; set; }

        public string sirket_fax_adresi { get; set; }

        public string sirketcalismaalani { get; set; }

        public string sirket_hakkinda { get; set; }
         
        public string sirketi_ekleyen_kullaniciid { get; set; }

        public virtual ICollection<proje> projes { get; set; }
    }
}