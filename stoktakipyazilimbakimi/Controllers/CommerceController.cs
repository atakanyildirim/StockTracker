using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stoktakipyazilimbakimi.Controllers
{
    public class CommerceController : Controller
    {
        public ActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buy(int productID, int quantity)
        {
            using (StokContext stokcontext = new StokContext())
            {
                var product = stokcontext.Stok.Where(stok => stok.ProductID == productID).Single();
                if (quantity <= product.quantity)
                {
                    product.quantity -= quantity;
                    stokcontext.SaveChanges();
                    TempData["color"] = "alert alert-success";
                    TempData["message"] = "Stoktan " + quantity.ToString() + " miktar düşürüldü";
                }
                else
                {
                    TempData["color"] = "alert alert-danger";
                    TempData["message"] = "Satış için stok yetersiz!!!!!";
                }
                    
            }
            return View();
        }
    }
}