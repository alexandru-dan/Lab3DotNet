using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Models;
using Lab2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private IExpensesService expensesService;

        public ExpensesController(IExpensesService expensesService)
        {
            this.expensesService = expensesService;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IEnumerable<Expenses> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]Models.Type? type)
        {

            return expensesService.GetAll(from, to, type);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = expensesService.GetById(id);
            if(found == null)
            {
                return NotFound();
            }

            return Ok(found);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public void Post([FromBody] Expenses expenses)
        {
            expensesService.Create(expenses);
        }

        /// <summary>
        /// Add or update expense (upsert)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///         
        ///    PUT/    {
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
        /// <param name="expenses">expense that we want to add or modify</param>
        /// <returns>returns ok if added</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expenses expenses)
        {
            var result = expensesService.Upsert( id, expenses);

            return Ok(result);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = expensesService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}