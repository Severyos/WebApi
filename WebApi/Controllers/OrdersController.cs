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
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderEntity>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderEntity>> GetOrderEntity(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var orderEntity = await _context.Orders.FindAsync(id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            return orderEntity;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderEntity(int id, OrderEntity orderEntity)
        {
            if (id != orderEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderEntityExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderEntity>> PostOrderEntity(OrderEntity orderEntity)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'DataContext.Orders'  is null.");
          }
            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderEntity", new { id = orderEntity.Id }, orderEntity);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderEntity(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderEntityExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
