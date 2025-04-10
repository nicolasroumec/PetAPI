using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Implementations
{
    public interface IPetsService
    {
        public Response Add(AddPetDTO model, string email);
        public Response GetAll(int shelterId);
        public Response Update(PetUpdateDTO model, string email);
        public Response Delete(int id, string email);
    }
}
