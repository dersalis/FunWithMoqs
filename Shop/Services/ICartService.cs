using System.Collections.Generic;
using Shop.Models;

namespace Shop.Services
{
    public interface ICartService
    {
        double Total();
        IEnumerable<ICartItem> Items();
    }
}