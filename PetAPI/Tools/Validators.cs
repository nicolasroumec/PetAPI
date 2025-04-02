using PetAPI.Models.DTOs;

namespace PetAPI.Tools
{
    public class Validators
    {
        public static bool UsersRegisterValidator(RegisterDTO model)
        {
            return String.IsNullOrEmpty(model.email) ||
                   String.IsNullOrEmpty(model.password) ||
                   String.IsNullOrEmpty(model.firstName) ||
                   String.IsNullOrEmpty(model.lastName) ||
                   String.IsNullOrEmpty(model.phone) ||
                   String.IsNullOrEmpty(model.dni);
        }
        public static bool ShelterRegisterValidator(ShelterRegisterDTO model)
        {
            return String.IsNullOrEmpty(model.email) ||
                   String.IsNullOrEmpty(model.password) ||
                   String.IsNullOrEmpty(model.firstName) ||
                   String.IsNullOrEmpty(model.lastName) ||
                   String.IsNullOrEmpty(model.phone) ||
                   String.IsNullOrEmpty(model.dni) ||
                   String.IsNullOrEmpty(model.shelterName) ||
                   String.IsNullOrEmpty(model.shelterAddress) ||
                   String.IsNullOrEmpty(model.shelterPhone) ||
                   String.IsNullOrEmpty(model.shelterEmail);
        }
    }
}
