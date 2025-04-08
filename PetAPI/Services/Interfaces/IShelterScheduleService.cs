using PetAPI.Models.DTOs;
using PetAPI.Models.Responses;

namespace PetAPI.Services.Interfaces
{
    public interface IShelterScheduleService
    {
        Response GetSchedulesByShelter(int shelterId);
        Response AddSchedule(ShelterScheduleDTO model, string email);
        Response UpdateSchedule(ShelterScheduleDTO model, string email);
        Response DeleteSchedule(int id, string email);
        Response SetWeekSchedule(WeekScheduleDTO model, string email);

    }
}
