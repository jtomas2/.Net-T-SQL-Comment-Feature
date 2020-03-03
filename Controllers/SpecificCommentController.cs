
using Prospect.Models.Domain.Comments;
using Prospect.Models.Responses;
using Prospect.Services;
using Prospect.Services.Interfaces.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Prospect.Web.Controllers.Api.Comments
{
    [AllowAnonymous]
    [RoutePrefix("api/specificcomments")]
    public class SpecificCommentController: ApiController
    {
        ISpecificCommentService _sCommentService;
        IAuthenticationService _authenticationService;

        public SpecificCommentController(ISpecificCommentService sCommentService, IAuthenticationService authenticationService)
        {
            _sCommentService = sCommentService;
            _authenticationService = authenticationService;
        }
        [Route("{tableKeyId}/{tableName}/{pageNumber}/{sortBy}"), HttpGet]
        public IHttpActionResult GetByKeyId(int tableKeyId, string tableName, int pageNumber, string sortBy)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                ItemsResponse<SpecificComment> response = new ItemsResponse<SpecificComment>
                {
                    Items = _sCommentService.GetByKeyId(tableKeyId, tableName, pageNumber, sortBy, _authenticationService.GetCurrentUserId())
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}