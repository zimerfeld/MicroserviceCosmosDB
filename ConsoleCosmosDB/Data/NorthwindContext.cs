using Microsoft.EntityFrameworkCore;
using ConsoleCosmosDB.Models;

namespace ConsoleCosmosDB.Data;

public class NorthwindContext : DbContext
{
    public DbSet<Employee>? Employees { get; set; }
    public DbSet<Customer>? Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseCosmos(
        //     "accountEndpoint", 
        //     "accountKey", 
        //     "databaseName");
        optionsBuilder.UseCosmos(
            "https://localhost:8081",
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            "northwind-db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configuring Employees
        modelBuilder.Entity<Employee>()
                .ToContainer("Employees") // ToContainer
                .HasPartitionKey(e => e.Id); // Partition Key
    
        // configuring Customers
        modelBuilder.Entity<Customer>()
            .ToContainer("Customers") // ToContainer
            .HasPartitionKey(c => c.Id); // Partition Key
    
        modelBuilder.Entity<Customer>().OwnsMany(p => p.Orders);
    }
}