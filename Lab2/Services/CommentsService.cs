using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Services
{
    public interface ICommentsService
    {
        IEnumerable<CommentGetModel> GetAll(string filterString);
    }
    public class CommentsService : ICommentsService  
    {
        public IntroDbContext context;

        public CommentsService(IntroDbContext context)
        {
        this.context = context;
        }

        public IEnumerable<CommentGetModel> GetAll(string filterString)
        {
            IQueryable<CommentGetModel> result = context
                .Comments
                .Where(c => string.IsNullOrEmpty(filterString) || c.Text.Contains(filterString))
                .OrderBy(c => c.Id)
                .Select(c => new CommentGetModel() {
                    Id = c.Id,
                    Text = c.Text,
                    Important = c.Important,
                    ExpensesId = (from e in context.Expensess
                                  where e.Comments.Contains(c)
                                  select e.Id).FirstOrDefault()
                });

            return result.ToList();
        }

    }
}
