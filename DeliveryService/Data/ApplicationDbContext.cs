using DeliveryService.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}