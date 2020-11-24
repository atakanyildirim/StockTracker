using stoktakipyazilimbakimi.Models;
using System.Data.Entity;

namespace stoktakipyazilimbakimi.Controllers
{
    public class StokContext : DbContext
    {
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Urunler> Urunler { get; set; }
        public DbSet<Stok> Stok { get; set; }
        public StokContext() : base("name=stok")
        {
        }
    }
}