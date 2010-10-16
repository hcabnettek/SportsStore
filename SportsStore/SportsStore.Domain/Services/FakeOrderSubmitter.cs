using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Services
{
    public class FakeOrderSubmitter:IOrderSubmitter
    {
        public void SubmitOrder(Cart cart, ShippingDetails shippingDetails)
        {
            //do nada
        }
    }
}
