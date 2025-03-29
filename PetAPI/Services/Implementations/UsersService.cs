using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Interfaces;
using PetAPI.Repositories.Implementations;
using PetAPI.Services.Interfaces;
using PetAPI.Tools;
using PetAPI.Models.Entities;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace PetAPI.Services.Implementations
{
    public class UsersService : IUsersService
    {
        //Declaración de dependencias
        private readonly IUsersRepository _usersRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;
        private readonly Encrypter _encrypter;

        //Constructor que utiliza inyección de dependencias
        public UsersService(
            IUsersRepository usersRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
            _encrypter = new Encrypter();
        }

        public async Task<Response> Register(RegisterDTO model)
        {
            Response response = new Response();

            if(Validators.UsersRegisterValidator(model))
            {
                response.statusCode = 400;
                response.message = "Invalid form";
                return response;
            }

            var user = _usersRepository.GetByEmail(model.email);

            if(user != null)
            {
                response.statusCode = 401;
                response.message = "Email is already in use";
                return response;
            }

            Encrypter.EncryptString(model.password, out byte[] hash, out byte[] salt);

            User newUser = new User();

            newUser.email = model.email;
            newUser.dni = model.dni;
            newUser.firstName = model.firstName;
            newUser.lastName = model.lastName;
            newUser.phone = model.phone;
            newUser.hash = hash;
            newUser.salt = salt;
            newUser.role = Models.Enums.Role.ADOPTER;
            newUser.isVerified = false;

            _usersRepository.Save(newUser);

            string verificationCode = CodeGenerator.GenerateRandomCode(6);
            VerificationCode code = new VerificationCode()
            {
                userId = newUser.Id,
                code = verificationCode,
                createdAt = DateTime.Now,
                expiresAt = DateTime.Now.AddDays(1),
                isUsed = false
            };

            _verificationCodeRepository.Save(code);

            await _emailService.SendVerificationEmail(newUser.email, verificationCode);

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }
    }
}
