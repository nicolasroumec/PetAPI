using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface ISheltersService
    {
        public Task<Response> Register(ShelterRegisterDTO model);
        public Response Approve(int id);
        public Response GetPending(bool isApproved);
    }
}
