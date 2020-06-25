using SolarCoffee.Data.Models;
using System.Collections.Generic;

namespace SolarCoffee.Services.Order
{
  public interface IOrderService
  {
    List<SalesOrder> GetOrders();
    ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order);
    ServiceResponse<bool> MarkFullfilled(int id);
  }
}
