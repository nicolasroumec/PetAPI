using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;
using PetAPI.Services.Implementations;
using PetAPI.Services.Interfaces;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetsService _petsService;

        public PetsController(IPetsService petsService)
        {
            _petsService = petsService;
        }

        [Authorize]
        [HttpPost("add")]
        public ActionResult<Response> AddPet([FromBody] AddPetDTO model)
        {
            Response response = new Response();

            try
            {
                string email = User.FindFirst("Account")?.Value ?? string.Empty;
                response = _petsService.Add(model, email);
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
