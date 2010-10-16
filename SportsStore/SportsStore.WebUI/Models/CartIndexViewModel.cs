using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public string ReturnUrl { get; set; }
        public Cart Cart { get; set; }
        
    }
}