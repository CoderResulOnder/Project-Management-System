using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class universite
    {

        public universite()
        {
            this.fakultes = new HashSet<fakulte>();
        }
        [Key]
        public int universite_id { get; set; }
        [Required]
        public string universite_adi { get; set; }
        [Required]
        public string universite_adresi { get; set; }
        [Required]
        public string universite_faks { get; set; }
        [Required]
        public string universite_tel { get; set; }
        [Required]
        public string universite_sehir { get; set; }

        public virtual ICollection<fakulte> fakultes { get; set; }
    }
}