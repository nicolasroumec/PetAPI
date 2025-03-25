using PetAPI.Models.Enums;

namespace PetAPI.Models.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string breed { get; set; }
        public PetSize size { get; set; }
        public PetGender gender { get; set; }
        public string healthStatus { get; set; }
        public string description { get; set; }
        public List<string> photos { get; set; }
        public bool goodWithKids { get; set; }
        public bool goodWithPets { get; set; }


        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }

        public ICollection<AdoptionRequest> adoptionRequests { get; set; }
    }
}
