using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace stoktakipyazilimbakimi.Models
{
    public class StokJoinModel
    {
        [Key]
        public int StockID { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}