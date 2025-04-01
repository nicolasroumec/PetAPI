using PetAPI.Models.Entities;

namespace PetAPI.Repositories.Interfaces
{
    public interface ISheltersRepository
    {
        ICollection<Shelter> GetShelters();
        Shelter GetById(int id);
        void Remove(Shelter shelter);
        void Save(Shelter shelter);
    }
}
