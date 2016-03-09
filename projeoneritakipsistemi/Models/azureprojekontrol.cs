using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    public class azureprojekontrol
    {

        [Key]
        public int odevid { get; set; }
        public string kullanici { get; set; }
        public string bulutSistemleriblogurl { get; set; }
        public string sanalmakineblogurl { get; set; }
        public string webservisleriblogurl { get; set; }
        public string storageblogurl { get; set; }
        public string kullanmadiginizbulutservisiblogurl { get; set; }
        public string dataset { get; set; }
        public string MachineLearningProjeadi { get; set; }
        public string projeaciklama { get; set; }
        public string projegrupuyeleri { get; set; }
        public string kullanilanmodeltanit { get; set; }
        public string bitmisproje { get; set; }
        public DateTime teslimtarihi { get; set; }

    }
}