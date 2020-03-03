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
    [RoutePrefix("api/comments")]
    public class CommentController : ApiController
    {
        ICommentService _commentService;
        IErrorLogService _errorLogService;
        IAuthenticationService _authenticationService;

        public CommentController(ICommentService commentService, IErrorLogService errorLogService, IAuthenticationService authenticationService)
        {
            _commentService = commentService;
            _errorLogService = errorLogService;
            _authenticationService = authenticationService;
        }
        [Route(), HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                ItemsResponse<Comment> response = new ItemsResponse<Comment>
                {
                    Items = _commentService.GetAll()
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
        [Route(), HttpPut]
        public IHttpActionResult Put(CommentUpdateRequest model)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                _commentService.Put(model);

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
        [Route(), HttpPost]
        public IHttpActionResult Post(CommentAddRequest model)
        {
            try
            {

                model.CreatedById = _authenticationService.GetCurrentUserId();

                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                ItemResponse<int> response = new ItemResponse<int>
                {
                    Item = _commentService.Post(model)
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
        [Route("{id:int}"), HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                ItemResponse<Comment> response = new ItemResponse<Comment>
                {
                    Item = _commentService.GetById(id)
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
        [Route("{id:int}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest("invalid model!"); }
                _commentService.Delete(id); // change

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