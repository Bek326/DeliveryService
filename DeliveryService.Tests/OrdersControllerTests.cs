using DeliveryService.Controllers;
using DeliveryService.Data;
using DeliveryService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Tests;

public class OrdersControllerTests
{
    [Fact]
    public void GetFilteredOrders_ShouldReturnOrdersInDistrict()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "OrdersTestDb")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Orders.Add(new Order { OrderId = 1, District = "District1", DeliveryTime = DateTime.Now.AddMinutes(-10) });
            context.Orders.Add(new Order { OrderId = 2, District = "District1", DeliveryTime = DateTime.Now.AddMinutes(-5) });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var controller = new OrdersController(context);
            
            var result = controller.GetFilteredOrders("District1", DateTime.Now.AddMinutes(-15));
            
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var orders = Assert.IsType<List<Order>>(okResult?.Value);
            Assert.Equal(2, orders.Count);
        }
    }
}