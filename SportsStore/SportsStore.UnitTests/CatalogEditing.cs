using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class CatalogEditing
    {
        [Test]
        public void Can_Access_Product_Editing_Screen()
        {
            // Arrange: Repository that can return any IQueryable<Product>
            var testProducts = new[] { new Product { ProductId = 12 }, new Product { ProductId = 17 } };
            var mockRepository = UnitTestHelpers.MockProductsRepository(testProducts);

            // Act
            var result = new AdminController(mockRepository).Edit(17);

            // Assert
            result.ShouldBeDefaultView();
            ((Product)result.ViewData.Model).ProductId.ShouldEqual(17);
        }

        [Test]
        public void Can_Save_Edited_Product()
        {
            var mockRepository = new Mock<IProductsRepository>();
            var product = new Product {ProductId = 123};
            mockRepository.Setup(x => x.Products).Returns(new[] {product}.AsQueryable());

            var controller = new AdminController(mockRepository.Object).WithIncomingValues(
                new FormCollection
                    {
                        { "Name", "SomeName"}
                        , { "Description", "SomeDescription"}
                        , { "Price", "1"}
                        , { "Category", "SomeCategory"}
                    }
                );

            var result = controller.Edit(123, null);

            mockRepository.Verify(x => x.SaveProduct(product));
            result.ShouldBeRedirectionTo(new { action = "Index"});
        }

        [Test]
        public void Can_Delete_Product()
        {
            var mockRepository = new Mock<IProductsRepository>();
            var product = new Product {ProductId = 24, Name = "P24"};

            mockRepository.Setup(x => x.Products).Returns(new[] {product}.AsQueryable());

            var controller = new AdminController(mockRepository.Object);
            var result = controller.Delete(24);

            mockRepository.Verify(x=>x.DeleteProduct(product));
            result.ShouldBeRedirectionTo(new {action = "Index"});
            controller.TempData["message"].ShouldEqual("P24 was deleted");
        }
    }
}
