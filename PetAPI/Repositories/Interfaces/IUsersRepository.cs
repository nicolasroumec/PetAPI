using PetAPI.Models.Entities;

namespace PetAPI.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        User GetByEmail(string email);
        void Remove(User user);
        void Save(User user);
    }
}
