namespace PetAPI.Models.DTOs
{
    public class ShelterScheduleDTO
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public bool IsClosed { get; set; }
    }
}
