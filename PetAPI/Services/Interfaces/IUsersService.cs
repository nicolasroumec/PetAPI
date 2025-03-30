using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface IUsersService
    {
        public Task <Response> Register(RegisterDTO model);
        Response VerifyAccount(string email, string code);
        public Task<Response> ResendVerificationCode(string email);
    }
}
