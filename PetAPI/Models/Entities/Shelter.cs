namespace PetAPI.Models.Entities
{
    public class Shelter
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string openingHours { get; set; }
        public bool isApproved { get; set; }


        public int? UserId { get; set; }
        public User Admin { get; set; }

        public ICollection<Pet> Pets { get; set; }
        public ICollection<ShelterSchedule> Schedule { get; set; }

    }
}
