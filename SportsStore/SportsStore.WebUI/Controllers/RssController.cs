using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class RssController:Controller
    {
        private IProductsRepository _productsRepository;

        public RssController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ContentResult Feed()
        {
            var recent20prods = _productsRepository.Products.OrderByDescending(p => p.CreatedDate).Take(20);

            string endcoding = Response.ContentEncoding.WebName;
            XDocument rss = new XDocument(new XDeclaration("1.0", endcoding, "yes"),
                new XElement("rss", new XAttribute("version", "2.0"),
                    new XElement("channel", new XElement("title", "SportStore new products")
                        , new XElement("description", "Buy all the hottest new sports gear")
                        , new XElement("link", "http://sportsstore.example.com")
                        , from prod in recent20prods
                              select new XElement("item",
                                  new XElement("title", prod.Name)
                                  , new XElement("description", prod.Description)
                                  , new XElement("link", string.Format("http://localhost:1083/{0}", prod.Category))
                          )
                     )
                 )
            );

            return Content(rss.ToString(), "application/rss+xml");
        }
    }
}