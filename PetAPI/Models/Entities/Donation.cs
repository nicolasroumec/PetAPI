namespace PetAPI.Models.Entities
{
    public class Donation
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int shelterId { get; set; }
        public Shelter shelter { get; set; }
        public decimal amount { get; set; }
        public string paymentId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
