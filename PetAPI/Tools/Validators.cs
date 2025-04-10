﻿using PetAPI.Models.DTOs;

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
        public static bool ShelterUpdateValidator(ShelterUpdateDTO model)
        {
            return String.IsNullOrEmpty(model.name) ||
                   String.IsNullOrEmpty(model.address) ||
                   String.IsNullOrEmpty(model.phone) ||
                   String.IsNullOrEmpty(model.email);
        }
        public static bool AddPetValidator(AddPetDTO model)
        {
            return String.IsNullOrEmpty(model.name) ||
                   model.age <= 0 ||
                   String.IsNullOrEmpty(model.breed) ||
                   String.IsNullOrEmpty(model.healthStatus) ||
                   String.IsNullOrEmpty(model.description) ||
                   model.photos == null || model.photos.Count == 0;
        }
        public static bool PetUpdateValidator(PetUpdateDTO model)
        {
            return model == null ||
                   string.IsNullOrEmpty(model.name) ||
                   model.age <= 0 ||
                   string.IsNullOrEmpty(model.breed) ||
                   string.IsNullOrEmpty(model.healthStatus) ||
                   string.IsNullOrEmpty(model.description) ||
                   model.photos == null || model.photos.Count == 0;
        }
    }
}
