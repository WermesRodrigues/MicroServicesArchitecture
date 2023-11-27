

using wrssolutions.Domain.Entities;
using wrssolutions.Services.Interfaces;


public class OrderSystem : IOrderSystem
{
    private List<Order> CartList = new List<Order>();

    public void AddToCart(Order order)
    {
        CartList.Add(order);
    }

    public void RemoveFromCart(Order order)
    {
        CartList.Remove(order);
    }

    public decimal CalculateTotalAmount()
    {
        decimal totalAmount = CartList.Sum(order => order.Price);
        return totalAmount;
    }

    public List<(string, int)> CategoryDiscounts()
    {
        var categoryOrderDiscounts = new List<(string, int)>
        {
            ("Cheap", 0),
            ("Moderate", 0),
            ("Expensive", 0)
        };

        decimal cheapDiscount = 0.10m;
        decimal moderateDiscount = 0.20m;
        decimal expensiveDiscount = 0.30m;


        foreach (var orderItem in CartList)
        {
            if (orderItem.Price <= 10)
            {
                categoryOrderDiscounts[0] = (categoryOrderDiscounts[0].Item1, categoryOrderDiscounts[0].Item2 + (int)(orderItem.Price - (orderItem.Price * cheapDiscount)));
            }
            else if (orderItem.Price <= 20)
            {
                categoryOrderDiscounts[1] = (categoryOrderDiscounts[1].Item1, categoryOrderDiscounts[1].Item2 + (int)(orderItem.Price - (orderItem.Price * moderateDiscount)));
            }
            else
            {
                categoryOrderDiscounts[2] = (categoryOrderDiscounts[2].Item1, categoryOrderDiscounts[2].Item2 + (int)(orderItem.Price - (orderItem.Price * expensiveDiscount)));
            }
        }

        return categoryOrderDiscounts;
    }

    public List<(string, int)> CartItems()
    {
        var cartItems = CartList.GroupBy(order => order.Name)
            .Select(group => (group.Key, group.Count()))
            .ToList();

        return cartItems;
    }
}
