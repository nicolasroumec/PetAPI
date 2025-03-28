using PetAPI.Models.Entities;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface IRoleVerificationService
    {
        public Response VerifySuperAdmin(User user);
        public Response VerifyShelterAdmin(User user);
        public Response VerifyAdopter(User user);
    }
}
