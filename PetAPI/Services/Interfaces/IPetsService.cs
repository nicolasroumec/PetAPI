using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Implementations
{
    public interface IPetsService
    {
        public Response Add(AddPetDTO model, string email);

    }
}
