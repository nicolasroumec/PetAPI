using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Interfaces;
using PetAPI.Repositories.Implementations;
using PetAPI.Services.Interfaces;
using PetAPI.Tools;
using PetAPI.Models.Entities;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.OpenApi.Any;

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

        public Response ChangePassword(ChangePasswordDTO model)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(model.email);

            if (user == null)
            {
                response.statusCode = 404;
                response.message = "User not found";
                return response;
            }

            if (user.phone != model.phone)
            {
                response.statusCode = 404;
                response.message = "User not found";
                return response;
            }

            Encrypter.EncryptString(model.password, out byte[] hash, out byte[] salt);

            user.hash = hash;
            user.salt = salt;

            _usersRepository.Save(user);

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }

        public Response ChangePhone(ChangePhoneDTO model, string email) 
        { 
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);

            if(user == null) 
            {
                response.statusCode = 404;
                response.message = "User not found";
                return response;
            }

            user.phone = model.phone;

            _usersRepository.Save(user);

            response.statusCode = 200;
            response.message = "Ok";
            return response;
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
            response.message = "Registration successful. Please check your email to activate your account.";
            return response;
        }

        public Response VerifyAccount(string email, string code)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);

            if (user == null)
            {
                response.statusCode = 404;
                response.message = "User not found";
                return response;
            }

            var verificationCode = _verificationCodeRepository.GetByUserIdAndCode(user.Id, code);

            if (verificationCode == null)
            {
                response.statusCode = 400;
                response.message = "Invalid verification code";
                return response;
            }

            if (verificationCode.expiresAt < DateTime.Now)
            {
                response.statusCode = 400;
                response.message = "Verification code has expired";
                return response;
            }

            verificationCode.isUsed = true;
            _verificationCodeRepository.Save(verificationCode);

            user.isVerified = true;
            _usersRepository.Save(user);

            response.statusCode = 200;
            response.message = "Account successfully verified";
            return response;
        }

        public async Task<Response> ResendVerificationCode(string email) {

            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);

            if (user == null)
            {
                response.statusCode = 404;
                response.message = "User not found";
                return response;
            }

            if (user.isVerified)
            {
                response.statusCode = 400;
                response.message = "Account already verified";
                return response;
            }

            string verificationCode = CodeGenerator.GenerateRandomCode(6);
            VerificationCode code = new VerificationCode()
            {
                userId = user.Id,
                code = verificationCode,
                createdAt = DateTime.Now,
                expiresAt = DateTime.Now.AddDays(1),
                isUsed = false
            };

            _verificationCodeRepository.Save(code);

            await _emailService.SendVerificationEmail(user.email, verificationCode);

            response.statusCode = 200;
            response.message = "Verification code sent.";
            return response;
        }
        
    }
}
