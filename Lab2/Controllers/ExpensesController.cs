using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private IntroDbContext context;

        public ExpensesController(IntroDbContext context)
        {
            this.context = context;
        }

        // GET: api/Expensess
        [HttpGet]
        public IEnumerable<Expenses> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]Models.Type? type)
        {

            IQueryable<Expenses> result = context.Expensess.Include(f => f.Comments);
            if ((from == null && to == null) && type == null)
            {
                return result;
            }
            if (from != null)
            {
                result = result.Where(f => f.Date >= from);
            }
            if (to != null)
            {
                result = result.Where(f => f.Date <= to);
            }
            if(type != null)
            {
                result = result.Where(f => f.Type.Equals(type));
            }
            return result;
        }

        // GET: api/Expensess/5
        [HttpGet("{id}", Name = "Get")]
        public Expenses Get(int id)
        {
            return context.Expensess.FirstOrDefault(c => c.Id == id);
        }

        // POST: api/Expensess
        [HttpPost]
        public IActionResult Post([FromBody] Expenses expenses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Expensess.Add(expenses);
            context.SaveChanges();
            return Ok();
        }

        // PUT: api/Expensess/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expenses expenses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = context.Expensess.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                expenses.Id = existing.Id;
                context.Expensess.Remove(existing);
            }

            context.Expensess.Add(expenses);
            context.SaveChanges();
            return Ok();
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var found = context.Expensess.FirstOrDefault(c => c.Id == id);
            if (found == null)
            {
                return NotFound();
            }

            context.Expensess.Remove(found);
            context.SaveChanges();
            return Ok();
        }
    }
}