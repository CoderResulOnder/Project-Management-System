using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projeoneritakipsistemi.Models
{
    
    public class yazilimmuh
    {
        [Key]
        public int projeid { get; set; }
        public string Projeadi { get; set; }
        public string projeaciklama { get; set; }
        public string projegrupuyeleri { get; set; }
        public string projedil { get; set; }
        public string projeide { get; set; }
        public string surumkontsisadi { get; set; }
        public DateTime teslimtarihi { get; set; }
        public string baclocimgurl { get; set; }
        public string sprintimgurl { get; set; } 
        public string checkinimgurl { get; set; }
        public string youtubecalismavideourl { get; set; }
        public string kullaniciadi { get; set; }
        public string parola { get; set; }


    }
}