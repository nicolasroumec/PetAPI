using Microsoft.EntityFrameworkCore;
using PetAPI.Models;
using PetAPI.Models.Entities;

namespace PetAPI.Repositories.Implementations
{
    public class ShelterScheduleRepository : BaseRepository<ShelterSchedule>, IShelterScheduleRepository
    {
        public ShelterScheduleRepository(PetContext repositoryContext) : base(repositoryContext) { }

        public ICollection<ShelterSchedule> GetSchedulesByShelter(int shelterId)
        {
            return FindByCondition(s => s.shelterId == shelterId)
                .Include(s => s.shelter)
                .ToList();
        }

        public ShelterSchedule GetById(int id)
        {
            return FindByCondition(s => s.Id == id)
                .Include(s => s.shelter)
                .FirstOrDefault();
        }

        public void Save(ShelterSchedule schedule)
        {
            if (schedule.Id == 0)
                Create(schedule);
            else
                Update(schedule);

            SaveChanges();
        }

        public void Remove(ShelterSchedule schedule)
        {
            Delete(schedule);
            SaveChanges();
        }

        public void RemoveRange(ICollection<ShelterSchedule> schedules)
        {
            foreach (var schedule in schedules)
            {
                Delete(schedule);
            }
            SaveChanges();
        }
    }
}
