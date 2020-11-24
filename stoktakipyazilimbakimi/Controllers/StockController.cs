using PagedList;
using stoktakipyazilimbakimi.filters;
using stoktakipyazilimbakimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stoktakipyazilimbakimi.Controllers
{
    [UserAuthenticationFilter]
    public class StockController : Controller
    {
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            StokContext stokContext = new StokContext();
            var stoklar = from stok in stokContext.Stok
                          join prod in stokContext.Urunler
                          on stok.ProductID equals prod.UrunID
                          orderby stok.ProductID descending
                          select new StokJoinModel
                          {
                              StockID = stok.StockID,
                              ProductName = prod.UrunAdi,
                              ProductQuantity = stok.quantity,
                              UpdatedAt = stok.UpdatedAt
                          };

            return View(stoklar.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(int StockID)
        {
            StokContext stokContext = new StokContext();
            var stoklar = stokContext.Stok.Find(StockID);
            return View(stoklar);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Stok stok)
        {
            stok.UpdatedAt = DateTime.Now;
            using (StokContext stokContext = new StokContext())
            {
                stok = stokContext.Stok.Add(stok);
                stokContext.Entry(stok).State = System.Data.Entity.EntityState.Modified;
                stokContext.SaveChanges();
            }
            TempData["message"] = "Stok güncellendi";
            return RedirectToAction("Index");
        }
    }
}