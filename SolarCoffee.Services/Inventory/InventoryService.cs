using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarCoffee.Services.Inventory
{
  public class InventoryService : IInventoryService
  {
    private readonly SolarDbContext _db;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(SolarDbContext db, ILogger<InventoryService> logger)
    {
      _db = db;
      _logger = logger;
    }
    
    public void CreateSnapshot()
    {
      
    }

    public ProductInventory GetByProductId(int productId)
    {
      
    }

    public List<ProductInventory> GetCurrentInventory()
    {
      return _db.ProductInventories
        .Include(pi => pi.Product)
        .Where(pi => !pi.Product.IsArchived)
        .ToList();
    }

    public List<ProductInventorySnapshot> GetSnapshotHistory()
    {
      
    }

    public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
    {
      var now = DateTime.UtcNow;

      try
      {
        var inventory = _db.ProductInventories
        .Include(inv => inv.Product)
        .First(inv => inv.Product.Id == id);

        inventory.QuantityOnHand += adjustment;

        try
        {
          CreateSnapshot();
        }
        catch (Exception ex)
        {
          _logger.LogError("Error creating inventory snapshot.");
          _logger.LogError(ex.StackTrace);
        }

        _db.SaveChanges();

        return new ServiceResponse<ProductInventory> { 
          IsSuccess = true,
          Data = inventory,
          Message = $"Product {id} inventory adjusted.",
          Time = now
        };
      }
      catch (Exception ex)
      {
        return new ServiceResponse<ProductInventory>
        {
          IsSuccess = false,
          Data = null,
          Message = ex.Message,
          Time = now
        };
      }
      
    }
  }
}
