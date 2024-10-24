using DeliveryService.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO; 

namespace DeliveryService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetFilteredOrders(string district, DateTime firstDeliveryTime)
    {
        try
        {
            var timeLimit = firstDeliveryTime.AddMinutes(30);
            var filteredOrders = _context.Orders
                .Where(o => o.District == district)
                .ToList();

            if (filteredOrders.Count == 0)
            {
                return NotFound("No orders found for the specified criteria.");
            }

            Log($"Filtered orders for district {district} from {firstDeliveryTime} to {timeLimit}");

            return Ok(filteredOrders);
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet("download")]
    public IActionResult DownloadLog()
    {
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "logs.txt");

        if (!System.IO.File.Exists(logFilePath))
        {
            return NotFound("Log file not found.");
        }
        
        var fileBytes = System.IO.File.ReadAllBytes(logFilePath);
        var contentType = "application/octet-stream"; 
        
        return File(fileBytes, contentType, "logs.txt");
    }

    private void Log(string message)
    {
        string logDirectory = @"C:\Users\Бексултан\RiderProjects\DeliveryService\DeliveryService\Logs";
        string logFilePath = Path.Combine(logDirectory, "logs.txt");
        
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
        
        if (!System.IO.File.Exists(logFilePath))
        {
            System.IO.File.Create(logFilePath).Dispose(); 
        }
        
        System.IO.File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
    }
}
