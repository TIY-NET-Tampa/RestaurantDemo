using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResturantDemo.Models;
using Microsoft.AspNet.Identity;

namespace ResturantDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var menu = HttpContext.Cache["menu"] as IEnumerable<Category>;
            if (menu == null)
            {
                var db = new ApplicationDbContext();
                menu = db.Categories.Include("Menu").OrderBy(o => o.Name).ToList();
                HttpContext.Cache.Add(
                    "menu",
                    menu,
                    null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 5, 0),
                    System.Web.Caching.CacheItemPriority.Default,
                    null
                    );
            }

            return View(menu);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult Create(Category category)
        {
            var db = new ApplicationDbContext();
            HttpContext.Cache.Remove("menu");
            db.Categories.Add(category);
            db.SaveChanges();
            var menu = db.Categories.Include("Menu").OrderBy(o => o.Name).ToList();
            HttpContext.Cache.Add(
                "menu",
                menu,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                new TimeSpan(0, 5, 0),
                System.Web.Caching.CacheItemPriority.Default,
                null
                );
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ShoppingCart()
        {
            var cart = HttpContext.Session["cart"] as Order;
            return PartialView("_ShoppingCart", cart); 
        }

        [HttpPost]
        public ActionResult ShoppingCart(int id)
        {
            var cart = HttpContext.Session["cart"] as Order;
            if (cart == null)
            {
                cart = new Order();
            }
            var db = new ApplicationDbContext();
            var item = db.MenuItems.FirstOrDefault(f => f.Id == id);
            cart.Items.Add(item);
            if (HttpContext.Session["cart"] as Order == null)
            {
                HttpContext.Session.Add("cart", cart);
            }
            else
            {
                HttpContext.Session["cart"] = cart;
            }
            return PartialView("_ShoppingCart", cart);

        }

        public ActionResult ViewCart()
        {
            var cart = HttpContext.Session["cart"] as Order;
            return View(cart);
        }

        [Authorize(Roles ="customer")]
        public ActionResult PlaceOrder()
        {
            var cart = HttpContext.Session["cart"] as Order;
            var db = new ApplicationDbContext();
            if (cart != null)
            {
                cart.CustomerId = User.Identity.GetUserId();
                db.Orders.Add(cart);
                db.SaveChanges();
                HttpContext.Session.Remove("cart"); //["cart"] = null;
            }

            return View();
        }
    }
}