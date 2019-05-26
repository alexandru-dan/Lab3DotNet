using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentsService commentsService;

        public CommentsController(ICommentsService comments)
        {
            this.commentsService = comments;
        }
        /// <summary>
        /// Displays all the comments from db, using filter
        /// </summary>
        /// <param name="filter">Optional string filter</param>
        /// <returns>Return all the comments</returns>
        // GET: api/Comments
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<CommentGetModel> Get(string filter)
        {
            return commentsService.GetAll(filter);
        }

    }
}
