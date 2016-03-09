using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class kaynak
    {
        [Key]
        public int kaynak_id { get; set; }
        [Required]
        public string kaynak_name { get; set; }
        [Required]
        public string kaynak_aciklamasi { get; set; }
        public Nullable<System.DateTime> kaynak_tarih { get; set; }
        [MaxLength(1000)]
        public string kaynak_url { get; set; }
        public Nullable<int> proje_id { get; set; }
        public Nullable<int> kaynak_yukleyen_id { get; set; }
        public string kaynak_yukleyen_statu { get; set; }
       
        public virtual proje proje { get; set; }
    }
}