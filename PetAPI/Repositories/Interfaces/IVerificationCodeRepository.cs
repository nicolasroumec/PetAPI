using PetAPI.Models.Entities;

namespace PetAPI.Repositories.Interfaces
{
    public interface IVerificationCodeRepository
    {
        VerificationCode GetByUserIdAndCode(int userId, string code);
        void Save(VerificationCode code);
    }
}
