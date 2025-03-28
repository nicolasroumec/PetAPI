using PetAPI.Models.Entities;
using PetAPI.Models.Enums;
using PetAPI.Models.Responses;
using PetAPI.Services.Interfaces;

namespace PetAPI.Services.Implementations
{
    public class RoleVerificationService : IRoleVerificationService
    {
        public Response VerifySuperAdmin(User user)
        {
            Response response = new Response();
            if (user == null)
            {
                response.statusCode = 401;
                response.message = "Invalid session";
                return response;
            }

            if (user.role != Role.SUPERADMIN)
            {
                response.statusCode = 403;
                response.message = "Forbidden";
                return response;
            }

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }

        public Response VerifyShelterAdmin(User user)
        {
            Response response = new Response();
            if (user == null)
            {
                response.statusCode = 401;
                response.message = "Invalid session";
                return response;
            }

            if (user.role != Role.SHELTER_ADMIN && user.role != Role.SUPERADMIN)
            {
                response.statusCode = 403;
                response.message = "Forbidden";
                return response;
            }

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }

        public Response VerifyAdopter(User user)
        {
            Response response = new Response();
            if (user == null)
            {
                response.statusCode = 401;
                response.message = "Invalid session";
                return response;
            }

            if (user.role != Role.ADOPTER)
            {
                response.statusCode = 403;
                response.message = "Forbidden";
                return response;
            }

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }
    }
}
