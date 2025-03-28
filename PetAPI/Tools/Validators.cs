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
    }
}
