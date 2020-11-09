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
        [Required(ErrorMessage = "Lütfen tcno giriniz.")]
        [Display(Name = "Tc Kimlik")]
        public string tcno { get; set; }

        public string ad { get; set; }
        public string soyad { get; set; }
        public string telefon { get; set; }
        public string eposta { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string sifre { get; set; }
    }
}