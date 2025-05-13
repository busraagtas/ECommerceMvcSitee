using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using ECommerceMvcSite.Models;

namespace ECommerceMvcSite.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ECommerceMvcSite.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ECommerceMvcSite.Models.MyDbContext context)
        {
            // Ürünleri ekle
            if (!context.Products.Any())
            {
                context.Products.AddOrUpdate(
                    p => p.Name,
                    new Product { Name = "Ürün 1", Price = 100, Description = "Açıklama 1", ImageUrl = "/Content/deneme.png" },
                    new Product { Name = "Ürün 2", Price = 200, Description = "Açıklama 2", ImageUrl = "/Content/deneme2.png" }
                );
            }

            // Admin kullanıcıyı Admins tablosuna ekle (gerekli ise)
            if (!context.Admins.Any(a => a.Email == "agtasbusra96@gmail.com"))
            {
                context.Admins.AddOrUpdate(
                    a => a.Email,
                    new Admin
                    {
                        Email = "agtasbusra96@gmail.com",
                        Password = "admin123"
                    }
                );
            }

            // Admin kullanıcıyı Users tablosuna da ekle
            if (!context.Users.Any(u => u.Email == "agtasbusra96@gmail.com"))
            {
                context.Users.AddOrUpdate(
                    u => u.Email,
                    new User
                    {
                        Email = "agtasbusra96@gmail.com",
                        Password = "admin123",
                        FirstName = "Admin",
                        LastName = "Yetkili",
                        Username = "admin",
                        IsAdmin = true
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
