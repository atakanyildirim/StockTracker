using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace stoktakipyazilimbakimi.Models
{
    public class Personel
    {
        [Key]
        public string tcno { get; set; }

        public string ad { get; set; }
        public string soyad { get; set; }
        public string telefon { get; set; }
        public string eposta { get; set; }
        public string sifre { get; set; }
    }
}