using Microsoft.EntityFrameworkCore;
using PetAPI.Models;
using PetAPI.Models.Entities;
using PetAPI.Repositories.Interfaces;

namespace PetAPI.Repositories.Implementations
{
    public class SheltersRepository : BaseRepository<Shelter>, ISheltersRepository
    {
        public SheltersRepository(PetContext repositoryContext) : base(repositoryContext) { }

        public ICollection<Shelter> GetShelters()
        {
            return FindAll()
                .Include(s => s.Schedule)  // Incluir horarios del refugio
                .Include(s => s.Pets)      // Incluir mascotas del refugio
                .Include(s => s.Admin)     // Incluir el administrador del refugio
                .ToList();
        }
        public Shelter GetById(int id)
        {
            return FindByCondition(s => s.Id == id)
                .FirstOrDefault();
        }
        public void Remove(Shelter shelter)
        {
            Delete(shelter);
            SaveChanges();
        }

        public void Save(Shelter shelter)
        {
            if (shelter.Id == 0)
                Create(shelter);
            else
                Update(shelter);

            SaveChanges();
        }
    }
}
