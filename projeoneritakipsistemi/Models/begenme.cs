using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class begenme
    {
        [Key]
        public int begenmeid { get; set; } 

        public Nullable<int>  projeid{ get; set; }

        public string begenenid { get; set; }

        public string begenmedurumu{ get; set; }

        public virtual proje proje { get; set; }

    }
}