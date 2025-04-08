namespace PetAPI.Models.DTOs
{
    public class ShelterScheduleDTO
    {
        public int Id { get; set; }
        public DayOfWeek day { get; set; }
        public TimeSpan openingTime { get; set; }
        public TimeSpan closingTime { get; set; }
        public bool isClosed { get; set; }
    }
}
