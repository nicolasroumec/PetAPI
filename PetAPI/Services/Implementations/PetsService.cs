using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Implementations;
using PetAPI.Repositories.Interfaces;
using PetAPI.Services.Interfaces;
using PetAPI.Tools;

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
        public Response GetAll(int shelterId)
        {
            Response response = new Response();

            var pets = _petsRepository.GetAll(shelterId).ToList();

            if (pets == null || pets.Count == 0)
            {
                response.statusCode = 404;
                response.message = "Pets not found";
                return response;
            }

            var petDtos = pets.Select(p => new PetDTO
            {
                Id = p.Id,
                name = p.name,
                age = p.age,
                breed = p.breed,
                size = p.size,
                gender = p.gender,
                healthStatus = p.healthStatus,
                description = p.description,
                photos = p.photos,
                goodWithKids = p.goodWithKids,
                goodWithPets = p.goodWithPets
            }).ToList();

            response = new ResponseCollection<PetDTO>(200, "Ok", petDtos);
            return response;
        }
        public Response Update(PetUpdateDTO model, string email)
        {
            Response response; response = new Response();

            var user = _usersRepository.GetByEmail(email);

            response = _roleVerificationService.VerifyShelterAdmin(user);

            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var pet = _petsRepository.GetPetById(model.Id);

            if (pet == null)
            {
                response.statusCode = 404;
                response.message = "Pet not found";
                return response;
            }

            if (Validators.PetUpdateValidator(model))
            {
                response.statusCode = 400;
                response.message = "Invalid form";
                return response;
            }

            pet.name = model.name;
            pet.age = model.age;
            pet.breed = model.breed;
            pet.size = model.size;
            pet.gender = model.gender;
            pet.healthStatus = model.healthStatus;
            pet.description = model.description;
            pet.photos = model.photos;
            pet.goodWithKids = model.goodWithKids;
            pet.goodWithPets = model.goodWithPets;

            _petsRepository.Save(pet);

            response.statusCode = 200;
            response.message = "Pet updated successfully";
            return response;
        }
        public Response Delete(int id, string email)
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

            var pet = _petsRepository.GetPetById(id);

            if (pet == null)
            {
                response.statusCode = 404;
                response.message = "Pet not found";
                return response;
            }

            _petsRepository.Remove(pet);

            response.statusCode = 200;
            response.message = "Ok";
            return response;
        }

    }
}
