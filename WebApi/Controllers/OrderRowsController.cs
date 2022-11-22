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
    public class OrderRowsController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderRowsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/OrderRows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRowEntity>>> GetOrderRows()
        {
          if (_context.OrderRows == null)
          {
              return NotFound();
          }
            return await _context.OrderRows.ToListAsync();
        }

        // GET: api/OrderRows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRowEntity>> GetOrderRowEntity(int id)
        {
          if (_context.OrderRows == null)
          {
              return NotFound();
          }
            var orderRowEntity = await _context.OrderRows.FindAsync(id);

            if (orderRowEntity == null)
            {
                return NotFound();
            }

            return orderRowEntity;
        }

        // PUT: api/OrderRows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderRowEntity(int id, OrderRowEntity orderRowEntity)
        {
            if (id != orderRowEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderRowEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderRowEntityExists(id))
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

        // POST: api/OrderRows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderRowEntity>> PostOrderRowEntity(OrderRowEntity orderRowEntity)
        {
          if (_context.OrderRows == null)
          {
              return Problem("Entity set 'DataContext.OrderRows'  is null.");
          }
            _context.OrderRows.Add(orderRowEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderRowEntity", new { id = orderRowEntity.Id }, orderRowEntity);
        }

        // DELETE: api/OrderRows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderRowEntity(int id)
        {
            if (_context.OrderRows == null)
            {
                return NotFound();
            }
            var orderRowEntity = await _context.OrderRows.FindAsync(id);
            if (orderRowEntity == null)
            {
                return NotFound();
            }

            _context.OrderRows.Remove(orderRowEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderRowEntityExists(int id)
        {
            return (_context.OrderRows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
