using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Infrastructure
{
    public class EnumerableHelpers
    {
        public static IEnumerable<NavLink> MakeLinks(IEnumerable<string> sequence, Func<string, NavLink> makeLink)
        {
            return sequence.Select(makeLink);
        }
    }

    public class CartModelBinder: IModelBinder
    {
        private const string cartSessionKey = "_cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if(bindingContext.Model != null)
            {
                throw new InvalidOperationException("Cannot update instances");
            }

            Cart cart = (Cart) controllerContext.HttpContext.Session[cartSessionKey];
            if(cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[cartSessionKey] = cart;

            }

            return cart;
        }
    }
}