using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController :Controller
    {
        public ViewResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                if(!FormsAuthentication.Authenticate(model.UserName, model.Password))
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                }
                
            }

            if(ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
            } 
            
            return View();
            
        }

    }
}