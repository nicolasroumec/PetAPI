using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Enums;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Implementations;
using PetAPI.Repositories.Interfaces;
using PetAPI.Services.Interfaces;
using PetAPI.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;
        private readonly IRoleVerificationService _roleVerificationService;
        private readonly ISheltersRepository _sheltersRepository;

        public AuthService(IConfiguration configuration,
            IUsersRepository usersRepository,
            IRoleVerificationService roleVerificationService,
            ISheltersRepository sheltersRepository)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
            _roleVerificationService = roleVerificationService;
            _sheltersRepository = sheltersRepository;
        }

        public Response LoginAdopter(LoginDTO model, User user)
        {
            Response response = new Response();

            if (user == null)
            {
                response.statusCode = 401;
                response.message = "Invalid form";
                return response;
            }

            response = _roleVerificationService.VerifyAdopter(user);

            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "This account is not registered as an adopter";
                return response;
            }


            if (!(Encrypter.ValidateText(model.password, user.hash, user.salt)))
            {
                response.statusCode = 401;
                response.message = "Wrong password";
                return response;
            }

            if (!user.isVerified)
            {
                response.statusCode = 403;
                response.message = "Account not verified.";
                return response;
            }

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }

        public Response LoginShelter(LoginDTO model, User user)
        {
            Response response = new Response();
            if (user == null)
            {
                response.statusCode = 401;
                response.message = "Invalid form";
                return response;
            }

            response = _roleVerificationService.VerifyShelterAdmin(user);
            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "This account is not registered as a shelter administrator";
                return response;
            }

            if (!(Encrypter.ValidateText(model.password, user.hash, user.salt)))
            {
                response.statusCode = 401;
                response.message = "Wrong password";
                return response;
            }

            if (!user.isVerified)
            {
                response.statusCode = 403;
                response.message = "Account not verified.";
                return response;
            }

            if (user.role == Role.SHELTER_ADMIN)
            {
                var shelter = _sheltersRepository.GetByAdminId(user.Id);

                if (shelter == null)
                {
                    response.statusCode = 404;
                    response.message = "Shelter not found for this account";
                    return response;
                }

                if (!shelter.isApproved)
                {
                    response.statusCode = 403;
                    response.message = "Your shelter is pending approval by an administrator";
                    return response;
                }
            }

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }
        public string MakeToken(string email, string role, int minutes)
        {
            var claims = new[]
{
                new Claim("Account", email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(minutes),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
