
using wrssolutions.Domain.Entities;

namespace wrssolutions.Services.Interfaces
{
    public interface IOrderSystem
    {
        void AddToCart(Order order);
        void RemoveFromCart(Order order);
        decimal CalculateTotalAmount();
        List<(string, int)> CategoryDiscounts();
        List<(string, int)> CartItems();
    }
}
