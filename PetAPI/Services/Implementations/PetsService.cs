using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Implementations;
using PetAPI.Repositories.Interfaces;
using PetAPI.Services.Interfaces;

namespace PetAPI.Services.Implementations
{
    public class PetsService : IPetsService
    {
        private readonly IPetsRepository _petsRepository;
        private readonly IRoleVerificationService _roleVerificationService;
        private readonly IUsersRepository _usersRepository;
        private readonly ISheltersRepository _sheltersRepository;

        public PetsService(IPetsRepository petsRepository,
            IRoleVerificationService roleVerificationService,
            IUsersRepository usersRepository,
            ISheltersRepository sheltersRepository)
        {
            _petsRepository = petsRepository;
            _roleVerificationService = roleVerificationService;
            _usersRepository = usersRepository;
            _sheltersRepository = sheltersRepository;
        }

        public Response Add (AddPetDTO model, string email)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);

            response = _roleVerificationService.VerifyShelterAdmin(user);

            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var shelter = _sheltersRepository.GetByAdminId(user.Id);

            if (shelter == null)
            {
                response.statusCode = 403;
                response.message = "Unauthorized";
                return response;
            }

            if (Tools.Validators.AddPetValidator(model))
            {
                response.statusCode = 400;
                response.message = "Invalid Form";
                return response;
            }

            Pet newPet = new Pet();

            newPet.name = model.name;
            newPet.age = model.age;
            newPet.breed = model.breed;
            newPet.size = model.size;
            newPet.gender = model.gender;
            newPet.healthStatus = model.healthStatus;
            newPet.description = model.description;
            newPet.goodWithKids = model.goodWithKids;
            newPet.goodWithPets = model.goodWithPets;
            newPet.photos = model.photos;
            newPet.ShelterId = shelter.Id;

            _petsRepository.Save(newPet);

            response.statusCode = 200;
            response.message = "Pet added successfully";
            return response;
        }
    }
}
