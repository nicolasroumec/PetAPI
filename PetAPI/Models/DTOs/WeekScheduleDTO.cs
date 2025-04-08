namespace PetAPI.Models.DTOs
{
    public class WeekScheduleDTO
    {
        public int shelterId { get; set; }
        public List<ShelterScheduleDTO> Schedules { get; set; }
    }
}
