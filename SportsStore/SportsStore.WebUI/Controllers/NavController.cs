using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.Infrastructure;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductsRepository productsRepository;
        public NavController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }


        public ViewResult Menu(string category)
        {
            Func<string, NavLink> makeLink = categoryName => new NavLink{
                Text = categoryName ?? "Home"
                , RouteValues = new RouteValueDictionary(new{controller = "Products", action="List", category=categoryName, page=1})
                , IsSelected = (categoryName == category)
            };

            List<NavLink> navLinks = new List<NavLink>();
            navLinks.Add(makeLink(null));

            var categories = productsRepository.Products.Select(x => x.Category);
            navLinks.AddRange(EnumerableHelpers.MakeLinks(categories.Distinct().OrderBy(x => x), makeLink));

            return View(navLinks);
        }

    }
}
