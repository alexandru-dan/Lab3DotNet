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
        private IntroDbContext context;

        public CommentsService(IntroDbContext context)
        {
        this.context = context;
        }

        public IEnumerable<CommentGetModel> GetAll(string filterString)
        {
            IQueryable<Comment> result = context.Comments.Include(f => f.Expenses);
            if ()
        }

    }
}
