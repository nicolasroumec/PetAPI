using Microsoft.EntityFrameworkCore;
using PetAPI.Models;
using PetAPI.Models.Entities;
using PetAPI.Repositories.Interfaces;

namespace PetAPI.Repositories.Implementations
{
    public class PetsRepository : BaseRepository<Pet>, IPetsRepository
    {
        public PetsRepository(PetContext repositoryContext) : base(repositoryContext) { }

        public ICollection<Pet> GetAll(int shelterId)
        {
            return FindByCondition(p => p.ShelterId == shelterId)
                .Include(p => p.Shelter)
                .ToList();
        }

        public Pet GetPetById(int petId)
        {
            return FindByCondition(p => p.Id == petId)
                .Include(p => p.Shelter)
                .Include(p => p.adoptionRequests)
                .FirstOrDefault();
        }

        public void UpdateState(Pet pet)
        {
            Update(pet);
            SaveChanges();
        }

        public void Save(Pet pet)
        {
            if (pet.Id == 0)
                Create(pet);
            else
                Update(pet);
            SaveChanges();
        }

        public void Remove(Pet pet)
        {
            Delete(pet);
            SaveChanges();
        }
    }
}
