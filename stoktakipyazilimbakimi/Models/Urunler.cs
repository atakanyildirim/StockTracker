using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;


namespace stoktakipyazilimbakimi.Models
{
    public class Urunler
    {
        [Key]
        public int UrunID { get; set; }

        [Required(ErrorMessage = "Lütfen ürün adı giriniz")]
        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }

        [Required(ErrorMessage = "Lütfen fiyat giriniz")]
        [Display(Name = "Fiyat")]
        public decimal UrunFiyat { get; set; }

        [Required(ErrorMessage = "Lütfen birim türü giriniz")]
        [Display(Name = "Birim")]
        public string UrunBirimi { get; set; }
    }
}