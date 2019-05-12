using Lab2.Models;
using Lab2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = Lab2.Models.Type;

namespace Lab2.Services
{
    public interface IExpensesService
    {

        IEnumerable<ExpensesGetModel> GetAll(DateTime? from, DateTime? to, Models.Type? type);
        Expenses GetById(int id);
        ExpensesPostModel Create(ExpensesPostModel expenses);
        Expenses Upsert(int id, Expenses expenses);
        Expenses Delete(int id);
    }
    public class ExpensesService : IExpensesService
    {
        private IntroDbContext context;


        /// <summary>
        /// Contructor for Service
        /// </summary>
        /// <param name="context">Repo</param>
        public ExpensesService(IntroDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Create a new Expense, using ExpensesPostModel so we can use string for type
        /// </summary>
        /// <param name="expenses">New Expense object</param>
        /// <returns>Expense added</returns>
        public ExpensesPostModel Create (ExpensesPostModel expenses)
        {
            Expenses toAdd = ExpensesPostModel.toExpenses(expenses);
            context.Expensess.Add(toAdd);
            context.SaveChanges();

            return expenses;
        }
        /// <summary>
        /// Update/Inseret 
        /// </summary>
        /// <param name="id">Id for update, otherwise add</param>
        /// <param name="expenses">New Expense Object that we want to add</param>
        /// <returns>Expense we gave</returns>
        public Expenses Upsert (int id, Expenses expenses)
        {
            var existing = context.Expensess.FirstOrDefault(c => c.Id == id);
            if (existing != null)
            {
                expenses.Id = existing.Id;
                context.Expensess.Remove(existing);
            }

            context.Expensess.Add(expenses);
            context.SaveChanges();
            return expenses;
        }
        /// <summary>
        /// Delete an expense using id
        /// </summary>
        /// <param name="id">Id for expense that we want to delete</param>
        /// <returns>Deleted expense</returns>
        public Expenses Delete(int id)
        {
            var found = context.Expensess.FirstOrDefault(c => c.Id == id);
            if (found == null)
            {
                return null;
            }

            context.Expensess.Remove(found);
            context.SaveChanges();

            return found;
        }


        /// <summary>
        /// Get All Expenses
        /// </summary>
        ///  <remarks>
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
        public IEnumerable<ExpensesGetModel> GetAll(DateTime? from, DateTime? to, Models.Type? type)
        {
            IQueryable<Expenses> result = context.Expensess.Include(f => f.Comments);
            if ((from == null && to == null) && type == null)
            {
                return result.Select(f => ExpensesGetModel.FromExpenses(f));
            }
            if (from != null)
            {
                result = result.Where(f => f.Date >= from);
            }
            if (to != null)
            {
                result = result.Where(f => f.Date <= to);
            }
            if (type != null)
            {
                result = result.Where(f => f.Type.Equals(type));
            }
            return result.Select(f => ExpensesGetModel.FromExpenses(f));
        }
        /// <summary>
        /// See one expense in detail using id
        /// </summary>
        /// <param name="id">Id for expense that we want to see</param>
        /// <returns>founded expense with details</returns>
        public Expenses GetById(int id)
        {
            return context.Expensess
                .Include(f => f.Comments)
                .FirstOrDefault(f => f.Id == id);
        }
    }
}
