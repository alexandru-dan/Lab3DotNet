using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface IExpensesService
    {

        IEnumerable<Expenses> GetAll(DateTime? from, DateTime? to, Models.Type? type);
        Expenses GetById(int id);
        Expenses Create(Expenses expenses);
        Expenses Upsert(int id, Expenses expenses);
        Expenses Delete(int id);
    }
    public class ExpensesService : IExpensesService
    {
        private IntroDbContext context;

        public ExpensesService(IntroDbContext context)
        {
            this.context = context;
        }
        public Expenses Create (Expenses expenses)
        {
            context.Expensess.Add(expenses);
            context.SaveChanges();

            return expenses;
        }

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
        public IEnumerable<Expenses> GetAll(DateTime? from, DateTime? to, Models.Type? type)
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
            if (type != null)
            {
                result = result.Where(f => f.Type.Equals(type));
            }
            return result;
        }

        public Expenses GetById(int id)
        {
            return context.Expensess
                .Include(f => f.Comments)
                .FirstOrDefault(f => f.Id == id);
        }
    }
}
