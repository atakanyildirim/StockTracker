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
        [StringLength(11, ErrorMessage = "Lütfen 11 karakteri aşmayınız.")]
        [Required(ErrorMessage = "Lütfen tcno giriniz.")]
        [Display(Name = "Tc Kimlik")]
        public string tcno { get; set; }

        [Required(ErrorMessage = "Lütfen Adı giriniz.")]
        [Display(Name = "Adı")]
        public string ad { get; set; }

        [Required(ErrorMessage = "Lütfen Soyadı giriniz.")]
        [Display(Name = "Soyadı")]
        public string soyad { get; set; }

        [Display(Name = "Telefonu")]
        public string telefon { get; set; }

        [Required(ErrorMessage = "Lütfen Eposta giriniz.")]
        [Display(Name = "E-posta")]
        public string eposta { get; set; }

        public string token { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string sifre { get; set; }
    }
}