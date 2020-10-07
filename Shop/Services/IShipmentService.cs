using System.Collections.Generic;
using Shop.Models;

namespace Shop.Services
{
    public interface IShipmentService
    {
         void Ship(IAddressInfo info, IEnumerable<ICartItem> items);
    }
}