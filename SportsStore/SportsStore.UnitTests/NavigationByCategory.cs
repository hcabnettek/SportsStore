using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class NavigationByCategory
    {
        [Test]
        public void NavMenu_Includes_Alphabetical_List_Of_Distinct()
        {
            var mockRepository = UnitTestHelpers.MockProductsRepository(
                new Product {Category = "Vegetable", Name = "ProductA"}
                , new Product {Category = "Animal", Name = "ProductB"}
                , new Product {Category = "Vegetable", Name = "ProductC"}
                , new Product {Category = "Mineral", Name = "ProductD"}
                );

            var result = new NavController(mockRepository).Menu(null);

            var categorylinks =
                ((IEnumerable<NavLink>) result.ViewData.Model).Where(x => x.RouteValues["category"] != null);

            CollectionAssert.AreEqual(
                new[]{"Animal", "Mineral", "Vegetable"}
                , categorylinks.Select(x=>x.RouteValues["category"])
            );

            foreach (var link in categorylinks)
            {
                link.RouteValues["controller"].ShouldEqual("Products");
                link.RouteValues["action"].ShouldEqual("List");
                link.RouteValues["page"].ShouldEqual(1);
                link.Text.ShouldEqual(link.RouteValues["category"]);
                
            }
        }


        [Test]
        public void NavMenu_Shows_Home_Link_At_Top()
        {
            var mockRepository = UnitTestHelpers.MockProductsRepository();

            var result = new NavController(mockRepository).Menu(null);

            var topLink =((IEnumerable<NavLink>)result.ViewData.Model).First();


            topLink.RouteValues["controller"].ShouldEqual("Products");
            topLink.RouteValues["action"].ShouldEqual("List");
            topLink.RouteValues["page"].ShouldEqual(1);
            topLink.RouteValues["category"].ShouldEqual(null);
            topLink.Text.ShouldEqual("Home");

           
        }

        [Test]
        public void NavMenu_Highlights_Current_Category()
        {
            var mockRepository = UnitTestHelpers.MockProductsRepository( new Product{Category = "A", Name = "ProductA"}, new Product{Category = "B", Name = "ProductB"});
            var result = new NavController(mockRepository).Menu("B");

            var highlightedLinks = ((IEnumerable<NavLink>) result.ViewData.Model).Where(x => x.IsSelected).ToList();
            highlightedLinks.Count.ShouldEqual(1);


        }
    }
}
