namespace PetAPI.Models.Entities
{
    public class AdoptionRequest
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int petId { get; set; }
        public Pet pet { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
    }
}
