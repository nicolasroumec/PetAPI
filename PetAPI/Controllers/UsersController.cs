using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;
using PetAPI.Services.Interfaces;

namespace PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("changePassword")]
        public ActionResult<AnyType> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            Response response = new Response();

            try
            {
                response = _usersService.ChangePassword(model);
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
        [HttpPost("changePhone")]
        public ActionResult<AnyType> ChangePhone([FromBody] ChangePhoneDTO model)
        {
            Response response = new Response();

            try
            {
                string email = User.FindFirst("Account") != null ? User.FindFirst("Account").Value : string.Empty;
                response = _usersService.ChangePhone(model, email);
                return new JsonResult(response);
            }
            catch (Exception e)
            {
                response.statusCode = 500;
                response.message = e.Message;
                return new JsonResult(response);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<AnyType>> Register([FromBody] RegisterDTO model)
        {

            Response response = new Response();

            try
            {
                response = await _usersService.Register(model);
                return new JsonResult(response);
            }
            catch (Exception e)
            {
                response.statusCode = 500;
                response.message = e.Message;
                return new JsonResult(response);
            }
        }

        [HttpPost("verify")]
        public ActionResult <AnyType> VerifyAccount([FromBody] VerifyAccountDTO model)
        {
            Response response = new Response();

            try
            {
                response = _usersService.VerifyAccount(model.email, model.code);
                return new JsonResult(response);
            }
            catch(Exception e)
            {
                response.statusCode = 500;
                response.message = e.Message;
                return new JsonResult(response);
            }
        }

        [HttpPost("resendCode")]
        public async Task<ActionResult<AnyType>> ResendVerificationCode([FromBody] ResendVerificationCodeDTO model)
        {
            Response response = new Response();

            try
            {
                response = await _usersService.ResendVerificationCode(model.email);
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
