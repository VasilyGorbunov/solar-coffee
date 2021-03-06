﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;
using System;
using System.Linq;

namespace SolarCoffee.Web.Controllers
{
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly ICustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
    {
      _logger = logger;
      _customerService = customerService;
    }

    [HttpGet("/api/customer")]
    public ActionResult GetCustomers()
    {
      _logger.LogInformation("Getting customers");

      var customers = _customerService.GetAllCustomers();
      var customerModel = customers.Select(customer => new CustomerModel
      {
        Id = customer.Id,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddress),
        CreatedOn = customer.CreatedOn,
        UpdatedOn = customer.UpdatedOn
      })
        .OrderByDescending(customer => customer.CreatedOn)
        .ToList();

      return Ok(customerModel);

    }



    [HttpPost("/api/customer")]
    public ActionResult CreateCustomer([FromBody] CustomerModel customer)
    {
      _logger.LogInformation("Creating a new customer");
      customer.CreatedOn = DateTime.UtcNow;
      customer.UpdatedOn = DateTime.UtcNow;
      var customerData = CustomerMapper.SerializeCustomer(customer);
      var newCustomer = _customerService.CreateCustomer(customerData);

      return Ok(newCustomer);
    }

    [HttpDelete("/api/customer/{id}")]
    public ActionResult DeleteCustomer(int id)
    {
      _logger.LogInformation("Deletiing a customer");
      var response = _customerService.DeleteCustomer(id);
      return Ok(response);
    }
  }
}
