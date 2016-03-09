using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class akademisyen
    {
        public akademisyen()
        {
            this.grups = new HashSet<grup>();
            this.projes = new HashSet<proje>();
        }
        [Key]
        public int akademisyen_id { get; set; }
        [Required]
        public string akademisyen_adi { get; set; }
        [Required]
        public string akademisyen_soyadi { get; set; }
        [Required]
        public string akademisyen_tc { get; set; }
        [Required]
        public string akademisyen_tel { get; set; }
        public string akademisyen_sitesi { get; set; }
        public string akademisyen_mail { get; set; }
        public string akademisyen_parola { get; set; }
        public string akademisyen_kullanici_adi { get; set; }
        public string akademisyen_unvani { get; set; }
        public string akademisyen_uzmanlik_alani { get; set; }
        [Required]
        public Nullable<int> akademisyen_oda_no { get; set; }
        public Nullable<int> aka_bolum_id { get; set; }

        public virtual bolum bolum { get; set; }

        public virtual ICollection<grup> grups { get; set; }
        public virtual ICollection<proje> projes { get; set; }
      
    }
}