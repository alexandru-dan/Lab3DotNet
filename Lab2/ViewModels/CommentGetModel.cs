using Lab2.Models;

namespace Lab3.Services
{
    public class CommentGetModel
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int? ExpensesId { get; set; }

        

        //public static CommentGetModel FromComments(Comment comment)
        //{
        //    return new CommentGetModel

        //    {
        //        Id = comment.Id,
        //        Text = comment.Text,
        //        Important = comment.Important,
        //        ExpensesId = comment.
        //    };
        //} 

    }
}