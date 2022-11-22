using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerEntity>>> GetCustomers()
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerEntity>> GetCustomerEntity(int id)
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            var customerEntity = await _context.Customers.FindAsync(id);

            if (customerEntity == null)
            {
                return NotFound();
            }

            return customerEntity;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerEntity(int id, CustomerEntity customerEntity)
        {
            if (id != customerEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerEntity>> PostCustomerEntity(CustomerEntity customerEntity)
        {
          if (_context.Customers == null)
          {
              return Problem("Entity set 'DataContext.Customers'  is null.");
          }
            _context.Customers.Add(customerEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerEntity", new { id = customerEntity.Id }, customerEntity);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerEntity(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customerEntity = await _context.Customers.FindAsync(id);
            if (customerEntity == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerEntityExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
