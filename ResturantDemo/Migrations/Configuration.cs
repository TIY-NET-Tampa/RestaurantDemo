namespace ResturantDemo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ResturantDemo.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ResturantDemo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ResturantDemo.Models.ApplicationDbContext context)
        {

            var winList = new Category { Name = "Wines" };
            var Entrees = new Category { Name = "Entrees" };
            //  This method will be called after migrating to the latest version.
            context.Categories.AddOrUpdate(cat => cat.Name,
               winList,
               Entrees);

            context.SaveChanges();

            var wine1 = new MenuItem { Name = "Riseling", Price = 2.45, CategoryId = winList.Id };
            var wine2 = new MenuItem { Name = "Chardenauy", Price = 10.12, CategoryId = winList.Id };
            context.MenuItems.AddOrUpdate(mi => mi.Name,
                wine1,
                wine2);

            var entress = new List<MenuItem> {
                new MenuItem {Name = "Pizza" ,Price = 5},
                new MenuItem {Name = "Shrimp", Price = 12 },
                new MenuItem {Name = "Steakums", Price = 3.5 },
            };
            entress = entress.Select(s => new MenuItem(s) { CategoryId = Entrees.Id }).ToList();

            entress.ForEach(menuItem =>
            {
                context.MenuItems.AddOrUpdate(mi => mi.Name, menuItem);
            });
            context.SaveChanges();
        }
    }
}
