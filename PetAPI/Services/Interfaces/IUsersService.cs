using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface IUsersService
    {
        public Response Register(RegisterDTO model);

    }
}
