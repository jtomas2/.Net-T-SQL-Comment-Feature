using Prospect.Models.Requests.Comments;
using Prospect.Models.Responses;
using Prospect.Services.Interfaces.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;



namespace Prospect.Web.Controllers.Api.Comments
{
    [RoutePrefix("api/workingvotes")]
    public class WorkingCommentVoteController : ApiController
    {
        IWorkingCommentVoteService _workingCommentVoteService;

        public WorkingCommentVoteController(IWorkingCommentVoteService workingCommentVoteService)
        {
            _workingCommentVoteService = workingCommentVoteService;
        }

        [Route(), HttpPut]
        public IHttpActionResult Put(WorkingCommentVoteUpdateRequest model)
        {
            try
            {
               

                _workingCommentVoteService.Put(model); // change

                return Ok(new SuccessResponse());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}