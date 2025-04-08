using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;
using PetAPI.Services.Interfaces;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterScheduleController : ControllerBase
    {
        private readonly IShelterScheduleService _shelterScheduleService;

        public ShelterScheduleController(IShelterScheduleService shelterScheduleService)
        {
            _shelterScheduleService = shelterScheduleService;
        }

        [HttpGet("shelter/{shelterId}")]
        public ActionResult<Response> GetSchedulesByShelter(int shelterId)
        {
            Response response = new Response();

            try
            {
                response = _shelterScheduleService.GetSchedulesByShelter(shelterId);
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
        [HttpPost("add")]
        public ActionResult<Response> AddSchedule([FromBody] ShelterScheduleDTO model)
        {
            Response response = new Response();

            try
            {
                string email = User.FindFirst("Account")?.Value ?? string.Empty;
                response = _shelterScheduleService.AddSchedule(model, email);
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
        [HttpPut("update")]
        public ActionResult<Response> UpdateSchedule([FromBody] ShelterScheduleDTO model)
        {
            Response response = new Response();

            try
            {
                string email = User.FindFirst("Account")?.Value ?? string.Empty;
                response = _shelterScheduleService.UpdateSchedule(model, email);
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
        [HttpDelete("delete/{id}")]
        public ActionResult<Response> DeleteSchedule(int id)
        {
            Response response = new Response();

            try
            {
                string email = User.FindFirst("Account")?.Value ?? string.Empty;
                response = _shelterScheduleService.DeleteSchedule(id, email);
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
        [HttpPost("set-week")]
        public ActionResult<Response> SetWeekSchedule([FromBody] WeekScheduleDTO model)
        {
            Response response = new Response();

            try
            {
                string email = User.FindFirst("Account")?.Value ?? string.Empty;
                response = _shelterScheduleService.SetWeekSchedule(model, email);
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