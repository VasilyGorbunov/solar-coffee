using SolarCoffee.Data.Models;
using System.Collections.Generic;

namespace SolarCoffee.Services.Order
{
  public class OrderService : IOrderService
  {
    public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
    {
      throw new System.NotImplementedException();
    }

    public List<SalesOrder> GetOrders()
    {
      throw new System.NotImplementedException();
    }

    public ServiceResponse<bool> MarkFullfilled(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}
