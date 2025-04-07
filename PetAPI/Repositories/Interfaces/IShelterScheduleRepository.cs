using PetAPI.Models.Entities;

namespace PetAPI.Repositories.Implementations
{
    public interface IShelterScheduleRepository
    {
        ICollection<ShelterSchedule> GetSchedulesByShelter(int shelterId);
        ShelterSchedule GetById(int id);
        void Save(ShelterSchedule schedule);
        void Remove(ShelterSchedule schedule);
        void RemoveRange(ICollection<ShelterSchedule> schedules);
    }
}
