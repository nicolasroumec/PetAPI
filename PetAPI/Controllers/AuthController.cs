using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Interfaces;
using PetAPI.Services.Interfaces;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthController(IUsersRepository usersRepository, IAuthService authService)
        {
            _userRepository = usersRepository;
            _authService = authService;

        }

        [HttpPost("login")]
        public ActionResult <AnyType> Login([FromBody] LoginDTO model)
        {
            Response response = new Response();

            try
            {
                if (String.IsNullOrEmpty(model.email) || String.IsNullOrEmpty(model.password))
                {
                    response.statusCode = 401;
                    response.message = "Invalid form";
                    return new JsonResult(response);
                }

                User user = _userRepository.GetByEmail(model.email);

                response = _authService.Login(model, user);

                if (response.statusCode != 200)
                    return new JsonResult(response);

                string token = _authService.MakeToken(user.email, user.role.ToString(), 15);

                response = new ResponseModel<string>(200, "Ok", token);

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
