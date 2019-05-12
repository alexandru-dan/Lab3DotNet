using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class IntroDbContext : DbContext
    {
        public IntroDbContext(DbContextOptions<IntroDbContext> options) : base(options)
        {

        }
        public DbSet<Expenses> Expensess { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
