using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface IUsersService
    {
        public Response ChangePassword(ChangePasswordDTO model);
        public Response ChangePhone(ChangePhoneDTO model, string email);
        public Task <Response> Register(RegisterDTO model);
        public Task<Response> ResendVerificationCode(string email);
        Response VerifyAccount(string email, string code);
    }
}
