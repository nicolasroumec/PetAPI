using PetAPI.Models.Entities;

namespace PetAPI.Repositories.Interfaces
{
    public interface IPetsRepository
    {
        ICollection<Pet> GetPets(int shelterId);
        Pet GetPetById(int petId);
        void UpdateState(Pet pet);
        void Save(Pet pet);
        void Remove(Pet pet);
    }
}
