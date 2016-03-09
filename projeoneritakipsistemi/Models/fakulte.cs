using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class fakulte
    {

        public fakulte()
        {
            this.bolums = new HashSet<bolum>();
            this.ogrencis = new HashSet<ogrenci>();
        }
        [Key]
        public int fakulte_id { get; set; }
        [Required]
        public string fakulte_adi { get; set; }
        [Required]
        public string fakulte_tel { get; set; }
        [Required]
        public string fakulte_faks { get; set; }
        public Nullable<int> fak_uni_id { get; set; }


        public virtual ICollection<bolum> bolums { get; set; }
        public virtual universite universite { get; set; }

        public virtual ICollection<ogrenci> ogrencis { get; set; }

    }
}