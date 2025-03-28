using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface IAuthService
    {
        public Response Login(LoginDTO model, User user);
        public string MakeToken(string email, string role, int minutes);
    }
}
