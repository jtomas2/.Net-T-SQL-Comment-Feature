using Prospect.Models.Domain.Comments;
using Prospect.Models.Requests.Comments;
using Prospect.Models.Responses;
using Prospect.Services;
using Prospect.Services.Interfaces.Comments;
using Prospect.Services.Interfaces.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Prospect.Web.Controllers.Api.Comments
{
    
    [RoutePrefix("api/commentvotes")]
    public class CommentVoteController : ApiController
    {
        ICommentVoteService _commentVoteService;
        IErrorLogService _errorLogService;
        IAuthenticationService _authentcationService;

        public CommentVoteController(ICommentVoteService CommentVoteService, IErrorLogService errorLogService, IAuthenticationService authenticationService)
        {
            _commentVoteService = CommentVoteService;
            _errorLogService = errorLogService;
            _authentcationService = authenticationService;
        }

        [Route(), HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                ItemsResponse<CommentVote> response = new ItemsResponse<CommentVote>
                {
                    Items = _commentVoteService.GetAll()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });
                return BadRequest(ex.Message);
            }
        }
        [Route(), HttpPost]
        public IHttpActionResult Post(CommentVoteAddRequest model)
        {
            try
            {
                model.CreatedById = _authentcationService.GetCurrentUserId();
                if(!ModelState.IsValid) { return BadRequest(ModelState); }

                _commentVoteService.Post(model);
                
                return Ok(new SuccessResponse());
            }
            catch (Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });
                return BadRequest(ex.Message);
            }
        }
        [Route("{commentId:int}"), HttpPut]
        public IHttpActionResult Put(CommentVoteAddRequest model)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                _commentVoteService.Put(model);

                return Ok(new SuccessResponse());
            }
            catch (Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });
                return BadRequest(ex.Message);
            }
        }
        [Route("{commentId:int}"),HttpGet]
        public IHttpActionResult GetById(int commentId)
        {
            try
            {
                ItemResponse<CommentVote> response = new ItemResponse<CommentVote>
                {
                    Item = _commentVoteService.GetById(commentId)
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });
                return BadRequest(ex.Message);
            }
        }
        [Route("{commentId:int}"), HttpDelete]
        public IHttpActionResult Delete(int commentId)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest("invalid model!"); }
                _commentVoteService.Delete(commentId); // change

                return Ok(new SuccessResponse());
            }
            catch (Exception ex)
            {
                _errorLogService.Post(new Models.Requests.Logs.ErrorLogAddRequest
                {
                    ErrorSourceTypeId = 1,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name
                });
                return BadRequest(ex.Message);
            }
        }
    }
}