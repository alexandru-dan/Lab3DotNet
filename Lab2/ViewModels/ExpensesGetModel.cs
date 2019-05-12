using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    public class ExpensesGetModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfComments { get; set; }


        public static ExpensesGetModel FromExpenses(Expenses expenses)
        {
            return new ExpensesGetModel
            {
                Id = expenses.Id,
                Description = expenses.Description,
                Location = expenses.Location,
                Date = expenses.Date,
                NumberOfComments = expenses.Comments.Count
            };
        }
    }
}
