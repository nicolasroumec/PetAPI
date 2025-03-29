using PetAPI.Models;
using PetAPI.Models.Entities;
using PetAPI.Repositories.Interfaces;

namespace PetAPI.Repositories.Implementations
{
    public class VerificationCodeRepository : BaseRepository<VerificationCode>, IVerificationCodeRepository
    {
        public VerificationCodeRepository(PetContext repositoryContext) : base(repositoryContext)
        { 
        }

        public VerificationCode GetByUserIdAndCode(int userId, string code)
        {
            return FindByCondition(v => v.userId == userId && v.code == code && !v.isUsed)
                .FirstOrDefault();
        }

        public void Save(VerificationCode code)
        {
            if (code.Id == 0)
                Create(code);
            else
                Update(code);

            SaveChanges();
        }

    }
}
