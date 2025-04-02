using PetAPI.Models.Entities;

namespace PetAPI.Models.DTOs
{
    public class ShelterUpdateDTO
    {
        public int Id { get; set; } 
        public string name { get; set; }
        public string address { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

    }
}
