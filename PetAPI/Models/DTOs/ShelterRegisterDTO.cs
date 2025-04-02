namespace PetAPI.Models.DTOs
{
    public class ShelterRegisterDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string dni { get; set; }
        public string email { get; set; }
        public string password { get; set; }


        public string shelterName { get; set; }
        public string shelterAddress { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string shelterPhone { get; set; }
        public string shelterEmail { get; set; }
    }
}
