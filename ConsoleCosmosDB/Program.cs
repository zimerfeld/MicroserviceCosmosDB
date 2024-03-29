﻿using Microsoft.EntityFrameworkCore;
using ConsoleCosmosDB.Data;
using ConsoleCosmosDB.Models;

using (var northwindContext = new NorthwindContext())
{

#region Inserting Employees

    var employee1 = new Employee()
    {
        Id = Guid.NewGuid().ToString(),
        LastName = "Davolio",
        FirstName = "Nancy",
        Title = "Sales Representative",
        BirthDate = new DateTime(1948, 12, 08),
        HireDate = new DateTime(1992, 05, 01),
        Address = "507 - 20th Ave. E. Apt. 2A",
        City = "Seattle",
        PostalCode = "98122",
        Country = "USA",
        HomePhone = "(206) 555-9857"
    };

    var employee2 = new Employee()
    {
        Id = Guid.NewGuid().ToString(),
        LastName = "Smith",
        FirstName = "John",
        Title = "Human Resource",
        BirthDate = new DateTime(1948, 12, 08),
        HireDate = new DateTime(2001, 12, 01),
        Address = "507 - 20th Ave. E. Apt. 2A",
        City = "Columbia",
        PostalCode = "98122",
        Country = "USA",
        HomePhone = "(204) 551-9857"
    };

    northwindContext.Employees?.Add(employee1);
    northwindContext.Employees?.Add(employee2);

    await northwindContext.SaveChangesAsync();

    Console.WriteLine("Employee records inserted successfully...");

#endregion

#region Inserting Customer

    Customer customer = new Customer()
    {
        Id = Guid.NewGuid().ToString(),
        CompanyName = "Alfreds Futterkiste",
        ContactName = "Maria Anders",
        ContactTitle = "Sales Representative",
        Address = "Obere Str. 57",
        City = "Berlin",
        Region = null,
        PostalCode = "12209",
        Country = "Germany",
        Phone = "030-0074321",
        Orders = new List<Order>()
        {
            new Order()
            {
                Id = Guid.NewGuid().ToString(),
                OrderDate = new DateTime(1997,08,25),
                RequiredDate = new DateTime(1997,09,22),
                ShippedDate = new DateTime(1997,09,02),
                ShipVia = 1,
                Freight = 29.46,
                ShipName = "Alfreds Futterkiste",
                ShipAddress = "Obere Str. 57",
                ShipCity = "Berlin",
                ShipRegion = null,
                ShipPostalCode = "12209",
                ShipCountry = "Germany"
            },
            new Order()
            {

                Id = Guid.NewGuid().ToString(),
                OrderDate = new DateTime(1997,10,03),
                RequiredDate = new DateTime(1997,10,31),
                ShippedDate = new DateTime(1997,10,13),
                ShipVia = 2,
                Freight = 61.02,
                ShipName = "Alfred's Futterkiste",
                ShipAddress = "Obere Str. 57",
                ShipCity = "Berlin",
                ShipRegion = null,
                ShipPostalCode = "12209",
                ShipCountry = "Germany"
            },
            new Order()
            {
                Id= Guid.NewGuid().ToString(),
                OrderDate = new DateTime(1997,10,13),
                RequiredDate = new DateTime(1997,11,24),
                ShippedDate = new DateTime(1997,10,21),
                ShipVia =  1,
                Freight = 23.94,
                ShipName = "Alfred's Futterkiste",
                ShipAddress = "Obere Str. 57",
                ShipCity = "Berlin",
                ShipRegion = null,
                ShipPostalCode = "12209",
                ShipCountry = "Germany",
            }
        }
    };

    northwindContext.Customers?.Add(customer);
    await northwindContext.SaveChangesAsync();

    Console.WriteLine("Customer record inserted successfully...");

#endregion

#region Get Employees

    if(northwindContext.Employees != null)
    {
        var employees = await northwindContext.Employees.ToListAsync();
        Console.WriteLine("");

        foreach (var employee in employees)
        {
            Console.WriteLine("First Name : " + employee.FirstName);
            Console.WriteLine("Last Name : " + employee.LastName);
            Console.WriteLine("Hire Date : " + employee.HireDate);
            Console.WriteLine("--------------------------------\n");
        }
    }

#endregion

#region Get an Employee

    if (northwindContext.Employees != null)
    {
        var employee = await northwindContext.Employees
            .Where(e => e.FirstName == "John")
            .FirstOrDefaultAsync();

        Console.WriteLine("");

        Console.WriteLine("First Name : " + employee?.FirstName);
        Console.WriteLine("Last Name : " + employee?.LastName);
        Console.WriteLine("Hire Date : " + employee?.HireDate);
        Console.WriteLine("--------------------------------\n");
    }
    
#endregion

#region Update an Employee

    if (northwindContext.Employees != null)
    {
        var employee = await northwindContext.Employees
            .Where(e => e.FirstName == "John")
            .FirstOrDefaultAsync();

        if(employee != null)
        {
            employee.LastName = "Doe";
            employee.HireDate = new DateTime(2002,12,01);

            await northwindContext.SaveChangesAsync();
            
            Console.WriteLine("\nRecord has been updated.\n");
        }        
    }
    
#endregion

#region Delete an Employee

    if (northwindContext.Employees != null)
    {
        var employee = await northwindContext.Employees
            .Where(e => e.FirstName == "John")
            .FirstOrDefaultAsync();

        if(employee != null)
        {
            northwindContext.Employees.Remove(employee);
            await northwindContext.SaveChangesAsync();
            
            Console.WriteLine("\nRecord has been deleted.\n");
        }        
    }

#endregion

}

Console.WriteLine("Welcome to the EFCore Cosmos DB Provider...");

for (int i = 0; i < 10; i++)
{
    var job = new Job
    {
        Id = Guid.NewGuid(),
        Address = new Address
        {
            Line1 = $"{i} Some Street {i}",
            Line2 = $"Somewhere{i}",
            Town = "Birmingham",
            PostCode = "B90 {i}SS",
        },
        Contacts = new List<Contact>()
                    {
                        new Contact { Title = "Mr", FirstName = $"Craig {i}", LastName = "Mellon", TelephoneNumber = "34441234" },
                        new Contact { Title = "Mrs", FirstName = $"Cara {i}", LastName = "Mellon", TelephoneNumber = "53665554" }
                    },
        AssignedResource = new Resource
        {
            Id = Guid.NewGuid(),
            Title = "Mr",
            FirstName = "Bob",
            LastName = "Builder",
            TelephoneNumber = "0800 1234567"
        }
    };
    using (var context = new JobContext())
    {
        context.Database.EnsureCreated();
        context.Add(job);
        context.SaveChanges();
    }
}

using (var context = new JobContext())
{
    var job = context.Jobs.First();
    // now load the resource and assign it to the Job
    var resource1 = context.Resources.First(x => x.Id == job.AssignedResourceId);
    job.AssignedResource = resource1;
    Console.WriteLine($"Job created and retrieved with address: {job.Address.Line1}, {job.Address.PostCode}");
    Console.WriteLine($"  Contacts ({job.Contacts.Count()})");
    job.Contacts.ForEach(x =>
    {
        Console.WriteLine($"    Name: {x.FirstName} {x.LastName}");
    });
    Console.WriteLine($"  Assigned Resource: {job.AssignedResource?.FirstName} {job.AssignedResource?.LastName}");
}



var resourceId = Guid.NewGuid();
var resource = new Resource
{
    Id = resourceId,
    Title = "Mr",
    FirstName = "Bob",
    LastName = "Builder",
    TelephoneNumber = "0800 1234567"
};
var job1 = new Job
{
    Id = Guid.NewGuid(),
    Address = new Address
    {
        Line1 = "Job 1 Address"
    },
    AssignedResource = resource
};
var job2 = new Job
{
    Id = Guid.NewGuid(),
    Address = new Address
    {
        Line1 = "Job 2 Address"
    },
    AssignedResource = resource
};
using (var context = new JobContext())
{
    context.Database.EnsureCreated();
    context.Add(job1);
    context.Add(job2);
    context.SaveChanges();
}


using (var context = new JobContext())
{
    var loadedResource = context.Resources.First(x => x.Id == resourceId);
    // Load all jobs with the same assigned resource id
    var jobs = context.Jobs.Where(x => x.AssignedResourceId == resourceId).ToList();
    jobs.ForEach(job =>
    {
        Console.WriteLine($"Job: {job.Id} - Resource: {job.AssignedResource?.FirstName} {job.AssignedResource?.FirstName}");
    });
}