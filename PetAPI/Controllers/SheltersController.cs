using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;
using PetAPI.Services.Interfaces;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheltersController : ControllerBase
    {
        private readonly ISheltersService _sheltersService;

        public SheltersController(ISheltersService sheltersService)
        {
            _sheltersService = sheltersService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AnyType>> Register([FromBody] ShelterRegisterDTO model)
        {
            Response response = new Response();

            try
            {
                response = await _sheltersService.Register(model);
                return new JsonResult(response);
            }
            catch (Exception e) 
            {
                response.statusCode = 500;
                response.message = e.Message;
                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpPut("approve/{id}")]
        public ActionResult<Response> Approve(int id)
        {
            Response response = new Response();

            try
            {
                response = _sheltersService.Approve(id);
                return new JsonResult(response);
            }
            catch (Exception e)
            {
                response.statusCode = 500;
                response.message = e.Message;
                return new JsonResult(response);
            }
        }

        [Authorize]
        [HttpGet("pending")]
        public ActionResult<Response> GetPending(bool isApproved)
        {
            Response response = new Response();

            try
            {
                response = _sheltersService.GetPending(false);
                return new JsonResult(response);
            }
            catch (Exception e)
            {
                response.statusCode = 500;
                response.message = e.Message;
                return new JsonResult(response);
            }
        }
    }
}
