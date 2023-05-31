﻿using CustomerAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IApplicationDbContext _context;
        //public CustomerController(IApplicationDbContext context)
        //{
        //    _context = context;
        //}

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IApplicationDbContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger =logger;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Entities.Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChanges();
            _logger.LogInformation("Create Controller visited at {DT}", DateTime.UtcNow.ToLongTimeString());
            return Ok(customer.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null) return NotFound();
            _logger.LogInformation("GetAll Controller visited at {DT}", DateTime.UtcNow.ToLongTimeString());
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _context.Customers.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _context.Customers.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();
            _context.Customers.Remove(customer);
            await _context.SaveChanges();
            return Ok(customer.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Entities.Customer customerData)
        {
            var customer = _context.Customers.Where(a => a.Id == id).FirstOrDefault();

            if (customer == null) return NotFound();
            else
            {
                customer.City = customerData.City;
                customer.Name = customerData.Name;
                customer.Contact = customerData.Contact;
                customer.Email = customerData.Email;
                await _context.SaveChanges();
                return Ok(customer.Id);
            }
        }
    }
}