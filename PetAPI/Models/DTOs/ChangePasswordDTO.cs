namespace PetAPI.Models.DTOs
{
    public class ChangePasswordDTO
    {
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
    }
}
