using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace stoktakipyazilimbakimi.Models
{
    public class Stok
    {
        [Key]
        [Display(Name = "Stok İd")]
        public int StockID { get; set; }

        [Display(Name = "Ürün İd")]
        public int ProductID { get; set; }

        [Display(Name = "Ürün Stok Miktarı")]
        public int quantity { get; set; }

        [Display(Name = "Son Değişim Tarihi")]
        public DateTime UpdatedAt { get; set; }
    }
}