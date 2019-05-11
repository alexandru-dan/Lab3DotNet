using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public enum Type
    {
        Food,
        Utilities,
        Outing,
        Groceries,
        Clothes,
        Transport,
        Other
    }
    public class Expenses
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Sum { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public Type Type { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
