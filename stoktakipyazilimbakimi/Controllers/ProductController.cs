using stoktakipyazilimbakimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using stoktakipyazilimbakimi.filters;

namespace stoktakipyazilimbakimi.Controllers
{
    [UserAuthenticationFilter]
    public class ProductController : Controller
    {
        public ActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            StokContext stok = new StokContext();
            return View(stok.Urunler.OrderByDescending(m => m.UrunID).ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Edit(int id)
        {
            Urunler urun;
            using (StokContext stok = new StokContext())
            {
                urun = stok.Urunler.Find(id);
            }
            return View(urun);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Urunler urun)
        {
            using (StokContext stok = new StokContext())
            {
                urun = stok.Urunler.Add(urun);
                stok.Entry(urun).State = System.Data.Entity.EntityState.Modified;
                stok.SaveChanges();
            }
            TempData["message"] = "Ürün güncellendi";
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(Urunler urun)
        {
            using (StokContext stokContext = new StokContext())
            {
                stokContext.Urunler.Add(urun);
                stokContext.SaveChanges();
                // Stok Tanımla
                Stok newstok = new Stok();
                newstok.ProductID = urun.UrunID;
                newstok.UpdatedAt = DateTime.Now;
                stokContext.Stok.Add(newstok);
                stokContext.SaveChanges();
            }
            TempData["message"] = "Yeni ürün eklendi";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (StokContext stok = new StokContext())
            {
                var urun = stok.Urunler.Find(id);
                stok.Urunler.Remove(urun);
                stok.SaveChanges();
            }
            TempData["message"] = "Başarıyla Silindi";
            return RedirectToAction("Index");
        }
    }
}