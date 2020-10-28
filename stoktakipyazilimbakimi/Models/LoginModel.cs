using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace stoktakipyazilimbakimi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Lütfen tcno giriniz.")]
        [Display(Name = "Tc Kimlik")]
        public string tcno { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string sifre { get; set; }
    }
}