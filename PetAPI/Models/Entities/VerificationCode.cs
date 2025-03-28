namespace PetAPI.Models.Entities
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public string code { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime expiresAt { get; set; }
        public bool isUsed { get; set; }
    }
}
