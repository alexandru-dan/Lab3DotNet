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

        /// <summary>
        /// Get All Expenses
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/expenses?from=2019-05-05&&type=food
        /// 
        /// 
        /// </remarks>
        /// <param name="from">Optional, filter by minimum Date</param>
        /// <param name="to">Optiona, filter by maximum Date</param>
        /// <param name="type">Optional, filter by expenses type</param>
        /// <returns>List of Expenses with/without filters</returns>
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

        /// <summary>
        /// Get Expenses with unique id
        /// </summary>
        /// <remark>
        /// Sample request:
        /// 
        ///   GET  /api/Expenses/5 
        /// 
        /// </remark>
        /// <param name="id">Id for the expense we're searching</param>
        /// <returns>Expense that have that id</returns>
        [HttpGet("{id}", Name = "Get")]
        public Expenses Get(int id)
        {
            return context.Expensess.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Add expense
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///    POST/    {
        ///		 "Description" : "Pizza",
        ///         "Sum" : 48.2,
        ///         "Location" : "Cluj-Napoca",
        ///         "Currency" : "Lei",
        ///         "date" : "2018-05-11T21:20:10",
        ///         "Type" : 0,
        ///         "comments" : [
        ///         	{
        ///         		"text": "A fost buna",
        ///         		"important": true
        ///
        ///             },
        ///         	{
        ///         		"text": "A fost putina",
        ///         		"important": false
        ///             }
        ///         	          ]
        ///         }
        /// 
        /// </remarks>
        /// <param name="expenses">expense that we want to add</param>
        /// <returns>returns ok if added</returns>
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
        /// <summary>
        /// Delete an expense with a certain id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///             api/Expense/2
        /// 
        /// </remarks>
        /// <param name="id">Id for the expense we want to delete</param>
        /// <returns>Ok if it was deleted, nok otherwise</returns>
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