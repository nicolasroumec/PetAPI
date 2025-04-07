namespace PetAPI.Models.Entities
{
    public class ShelterSchedule
    {
        public int Id { get; set; }
        public int shelterId { get; set; }
        public Shelter shelter { get; set; }
        public DayOfWeek day { get; set; }
        public TimeSpan openingTime { get; set; }
        public TimeSpan closingTime { get; set; }
        public bool isClosed { get; set; }
    }
}