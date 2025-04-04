using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface IAuthService
    {
        public Response LoginAdopter(LoginDTO model, User user);
        public Response LoginShelter(LoginDTO model, User user);
        public string MakeToken(string email, string role, int minutes);
    }
}
