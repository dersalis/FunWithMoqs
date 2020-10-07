using Shop.Models;

namespace Shop.Services
{
    public interface IPaymentService
    {
         bool Charge(double total, ICard card);
    }
}