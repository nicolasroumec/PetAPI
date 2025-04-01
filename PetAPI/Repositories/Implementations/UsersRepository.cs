using Microsoft.EntityFrameworkCore;
using PetAPI.Models;
using PetAPI.Models.Entities;
using PetAPI.Repositories.Interfaces;

namespace PetAPI.Repositories.Implementations
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(PetContext repositoryContext) : base(repositoryContext){ }

        public User GetByEmail(string email)
        {
            return FindByCondition(u => u.email == email)
                .FirstOrDefault();
        }
        public void Remove(User user)
        {
            Delete(user);
            SaveChanges();
        }

        public void Save(User user)
        {
            if (user.Id == 0)
                Create(user);
            else
                Update(user);

            SaveChanges();
        }
    }
}
