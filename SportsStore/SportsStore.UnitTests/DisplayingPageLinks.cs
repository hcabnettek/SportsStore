using System;
using System.Web.Mvc;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class DisplayingPageLinks
    {
        [Test]
        public void Can_Generate_Links_To_Other_Pages()
        {
            HtmlHelper html = null;

            PagingInfo pagingInfo = new PagingInfo{
                CurrentPage = 2
                , TotalItems = 28
                , ItemsPerPage = 10
            };


            Func<int, string> pageUrl = i => "Page" + i;

            MvcHtmlString result = html.PageLinks(pagingInfo, pageUrl);

            result.ToString().ShouldEqual(@"<a href=""Page1"">1</a>
<a class=""selected"" href=""Page2"">2</a>
<a href=""Page3"">3</a>
");
        }

        [Test]
        public void Product_Lists_Include_Correct_Page_Numbers()
        {
            var mockRepository = UnitTestHelpers.MockProductsRepository(
               new Product { Name = "P1" }
               , new Product { Name = "P2" }
               , new Product { Name = "P3" }
               , new Product { Name = "P4" }
               , new Product { Name = "P5" }

               );

            var controller = new ProductsController(mockRepository){PageSize = 3};

            var result = controller.List(null, 2);

            var viewModel = (ProductsListViewModel) result.ViewData.Model;
            PagingInfo pagingInfo = viewModel.PagingInfo;
            pagingInfo.CurrentPage.ShouldEqual(2);
            pagingInfo.ItemsPerPage.ShouldEqual(3);
            pagingInfo.TotalItems.ShouldEqual(5);
            pagingInfo.TotalPages.ShouldEqual(2);


        }
    }

  
}