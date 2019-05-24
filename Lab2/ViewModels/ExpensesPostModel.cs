using Lab2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Type = Lab2.Models.Type;

namespace Lab2.ViewModels
{
    public class ExpensesPostModel
    {
        public string Description { get; set; }
        public double Sum { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public List<Comment> Comments { get; set; }



        /// <summary>
        /// Add model, so we can add Type(ENUM) using letters
        /// </summary>
        /// <param name="expenses">Model received from "input"</param>
        /// <returns>added Expense</returns>
        public static Expenses ToExpenses(ExpensesPostModel expenses)
        {
            Type type = Models.Type.Food;
            if (expenses.Type == "Utilities")
            {
                type = Models.Type.Utilities;
            }

            else if(expenses.Type == "Outing")
            {
                type = Models.Type.Outing;
            }

            else if(expenses.Type == "Groceries")
            {
                type = Models.Type.Groceries;
            }
            
            else if(expenses.Type == "Clothes")
            {
                type = Models.Type.Clothes;
            }

            else if(expenses.Type == "Transport")
            {
                type = Models.Type.Transport;
            }

            else if(expenses.Type == "Other")
            {
                type = Models.Type.Other;
            }

            return new Expenses
            {
                Description = expenses.Description,
                Sum = expenses.Sum,
                Location = expenses.Location,
                Date = expenses.Date,
                Currency = expenses.Currency,
                Type = type,
                Comments = expenses.Comments
            };
        }
    }
}
