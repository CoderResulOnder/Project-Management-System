using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class yorum
    {
        [Key]
        public int yorum_id { get; set; }
        [Required]
        public string yorum_icerigi { get; set; }
        public string yorumu_yapan_id { get; set; }
        [Required]
        public string yorumu_yapan_statu { get; set; }
        public Nullable<int> yor_proje_id { get; set; }
        public Nullable<System.DateTime> yorum_tarihi { get; set; }

        public virtual proje proje { get; set; }
    }
}