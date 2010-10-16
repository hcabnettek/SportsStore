using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Services;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory :DefaultControllerFactory
    {
        private IKernel kernel = new StandardKernel(new SportsStoreServices());

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;

            return (IController) kernel.Get(controllerType);
        }

        private class SportsStoreServices : NinjectModule
        {
            public override void Load()
            {
                Bind<IProductsRepository>()
                    .To<SqlProductsRepository>()
                    .WithConstructorArgument("connectionString",
                        ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString
                    );


                Bind<IOrderSubmitter>()
                    .To<EmailOrderSubmitter>()
                    .WithConstructorArgument("mailTo",
                        ConfigurationManager.AppSettings["EmailOrderSubmitter.MailTo"]
                    );
            }
        }


    }
}