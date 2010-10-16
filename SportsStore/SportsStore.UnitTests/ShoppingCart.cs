using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Services;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestFixture]
    public class ShoppingCart
    {

        [Test]
        public void Cart_Starts_Empty()
        {
            new Cart().Lines.Count.ShouldEqual(0);
        }

        [Test]
        public void Cart_Combines_Lines_With_Same_Product()
        {
            Product p1 = new Product { ProductId = 1 };
            Product p2 = new Product { ProductId = 2 };

            var cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 10);

            cart.Lines.Count.ShouldEqual(2);
            cart.Lines.First(x => x.Product.ProductId == 1).Quantity.ShouldEqual(3);
            cart.Lines.First(x => x.Product.ProductId == 2).Quantity.ShouldEqual(10);

        }

        [Test]
        public void Cart_Can_Be_Cleared()
        {
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            cart.Clear();
            cart.Lines.Count.ShouldEqual(0);

        }

        [Test]
        public void Cart_TotalValue_Is_Sum_Of_Price_Times_Quantity()
        {
           
            var cart = new Cart();

            cart.AddItem(new Product{ ProductId = 1, Price = 5}, 10);
            cart.AddItem(new Product { ProductId = 2, Price = 2.1M }, 3);
            cart.AddItem(new Product { ProductId = 3, Price = 1000 }, 1);

            cart.ComputeTotalValue().ShouldEqual(1056.3M);

        }


        [Test]
        public void Can_Add_Product_To_Cart()
        {

            var mockProductsRepository = UnitTestHelpers.MockProductsRepository(
                new Product {ProductId = 123}
                , new Product {ProductId = 456}
                );


            var cartController = new CartController(mockProductsRepository, null);
            var cart = new Cart();

            cartController.AddToCart(cart, 456, null);

            cart.Lines.Count.ShouldEqual(1);
            cart.Lines[0].Product.ProductId.ShouldEqual(456);

        }


        [Test]
        public void Can_Remove_Product_From_Cart()
        {

            var mockProductsRepository = UnitTestHelpers.MockProductsRepository(
                new Product { ProductId = 123 }
                , new Product { ProductId = 456 }
                );


            var cartController = new CartController(mockProductsRepository, null);
            var cart = new Cart();

            if (mockProductsRepository.Products != null)
                foreach (var product in mockProductsRepository.Products)
                    cartController.AddToCart(cart, product.ProductId, null);

            cartController.RemoveFromCart(cart, 456, null);

            cart.Lines.Count.ShouldEqual(1);
            cart.Lines[0].Product.ProductId.ShouldEqual(123);

        }

        [Test]
        public void After_Adding_Product_To_Cart_User_Goes_To_Your_Cart_Screen()
        {

            var mockProductsRepository = UnitTestHelpers.MockProductsRepository(
                new Product { ProductId = 1 }
               
                );


            var cartController = new CartController(mockProductsRepository, null);
            var result = cartController.AddToCart(new Cart(), 1, "someReturnUrl");

            result.ShouldBeRedirectionTo(new {action = "Index", returnUrl = "someReturnUrl"});

        }

        [Test]
        public void Can_View_Cart_Contents()
        {
            var cart = new Cart();
            var result = new CartController(null, null).Index(cart, "someReturnUrl");

            var viewModel = (CartIndexViewModel) result.ViewData.Model;
        }

        [Test]
        public void Cannot_Check_Out_If_Cart_Is_Empty()
        {
            var emptyCart = new Cart();
            var shippingDetails = new ShippingDetails();

            var result = new CartController(null, null).CheckOut(emptyCart, shippingDetails);

            result.ShouldBeDefaultView();
        }


        [Test]
        public void Cannot_Check_Out_If_Shipping_Details_Are_Invalid()
        {
            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            var cartController = new CartController(null, null);
            cartController.ModelState.AddModelError("any key", "any error");

            var result = cartController.CheckOut(cart, new ShippingDetails());
            
            result.ShouldBeDefaultView();
        }

        [Test]
        public void Can_Check_Out_And_Submit_Order()
        {
            var mockOrderSubmitter = new Mock<IOrderSubmitter>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var shippingDetails = new ShippingDetails();


            var cartController = new CartController(null, mockOrderSubmitter.Object);
            var result = cartController.CheckOut(cart, shippingDetails);

            mockOrderSubmitter.Verify(x => x.SubmitOrder(cart, shippingDetails));

            result.ShouldBeView("Completed");
        }

        [Test]
        public void After_Checking_Out_Cart_Is_Emptied()
        {

            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            
            new CartController(null, new Mock<IOrderSubmitter>().Object).CheckOut(cart, new ShippingDetails());
            

           ;
        }
    }
}
