using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Implementations;
using PetAPI.Repositories.Interfaces;
using PetAPI.Services.Interfaces;
using PetAPI.Tools;

namespace PetAPI.Services.Implementations
{
    public class SheltersService : ISheltersService
    {
        private readonly ISheltersRepository _sheltersRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IEmailService _emailService;
        private readonly IRoleVerificationService _roleVerificationService;
        private readonly Encrypter _encrypter;

        public SheltersService(
            ISheltersRepository sheltersRepository,
            IUsersRepository usersRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IEmailService emailService,
            IRoleVerificationService roleVerificationService)
        {
            _sheltersRepository = sheltersRepository;
            _usersRepository = usersRepository;
            _verificationCodeRepository = verificationCodeRepository;
            _emailService = emailService;
            _encrypter = new Encrypter();
            _roleVerificationService = roleVerificationService;
        }

        public async Task<Response> Register (ShelterRegisterDTO model)
        {
            Response response = new Response();

            if (Validators.ShelterRegisterValidator(model))
            {
                response.statusCode = 400;
                response.message = "Invalid form";
                return response;
            }

            var user = _usersRepository.GetByEmail(model.email);

            if (user != null)
            {
                response.statusCode = 401;
                response.message = "Email is already in use";
                return response;
            }

            Encrypter.EncryptString(model.password, out byte[] hash, out byte[] salt);

            User newUser = new User
            {
                email = model.email,
                dni = model.dni,
                firstName = model.firstName,
                lastName = model.lastName,
                phone = model.phone,
                hash = hash,
                salt = salt,
                role = Models.Enums.Role.SHELTER_ADMIN,
                isVerified = false
            };

            _usersRepository.Save(newUser);

            Shelter newShelter = new Shelter
            {
                name = model.shelterName,
                address = model.shelterAddress,
                latitude = model.latitude,
                longitude = model.longitude,
                phone = model.shelterPhone,
                email = model.shelterEmail,
                UserId = newUser.Id,
                Admin = newUser
            };

            _sheltersRepository.Save(newShelter);

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
            response.message = "Shelter registration successful. Please check your email to activate your account.";
            return response;
        }

        public Response Approve(int id)
        {
            Response response = new Response();

            var shelter = _sheltersRepository.GetById(id);

            if(shelter == null)
            {
                response.statusCode = 404;
                response.message = "Shelter not found";
                return response;
            }

            shelter.isApproved = true;
            _sheltersRepository.Save(shelter);

            response.statusCode = 200;
            response.message = "Shelter approved successfully";
            return response;
        }

        public Response GetPending(bool isApproved)
        {
            Response response = new Response();

            var shelters = _sheltersRepository.GetPending(false).ToList();

            if(shelters == null)
            {
                response.statusCode = 404;
                response.message = "No pending shelters found";
                return response;
            }

            response = new ResponseCollection<Shelter>(200, "Ok", shelters);
            return response;
        }

        public Response Update (ShelterUpdateDTO model, string email)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);

            response = _roleVerificationService.VerifyShelterAdmin(user);

            if(response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var shelter = _sheltersRepository.GetById(model.Id);

            if(shelter == null)
            {
                response.statusCode = 404;
                response.message = "Shelter not found";
                return response;
            }

            if (shelter.email != email)
            {
                response.statusCode = 403;
                response.message = "Unauthorized";
                return response;
            }

            if (Validators.ShelterUpdateValidator(model))
            {
                response.statusCode = 400;
                response.message = "Invalid form";
                return response;
            }

            shelter.name = model.name;
            shelter.address = model.address;
            shelter.latitude = model.latitude;
            shelter.longitude = model.longitude;
            shelter.phone = model.phone;
            shelter.email = model.email;

            _sheltersRepository.Save(shelter);

            response.statusCode = 200;
            response.message = "Shelter updated successfully";
            return response;
        }

        public Response Delete (int id, string email)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);

            response = _roleVerificationService.VerifySuperAdmin(user);

            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var shelter = _sheltersRepository.GetById(id);

            if (shelter == null)
            {
                response.statusCode = 404;
                response.message = "Shelter not found";
                return response;
            }

            _sheltersRepository.Remove(shelter);

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }
    }
}
