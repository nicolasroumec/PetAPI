using PetAPI.Models.Entities;

namespace PetAPI.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        void Remove(User user);
        void Save(User user);
    }
}
