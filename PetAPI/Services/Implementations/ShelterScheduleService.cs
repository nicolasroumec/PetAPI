using PetAPI.Models.DTOs;
using PetAPI.Models.Entities;
using PetAPI.Models.Responses;
using PetAPI.Repositories.Implementations;
using PetAPI.Repositories.Interfaces;
using PetAPI.Services.Interfaces;

namespace PetAPI.Services.Implementations
{
    public class ShelterScheduleService : IShelterScheduleService
    {
        private readonly IShelterScheduleRepository _shelterScheduleRepository;
        private readonly ISheltersRepository _sheltersRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IRoleVerificationService _roleVerificationService;

        public ShelterScheduleService(
            IShelterScheduleRepository shelterScheduleRepository,
            ISheltersRepository sheltersRepository,
            IUsersRepository usersRepository,
            IRoleVerificationService roleVerificationService)
        {
            _shelterScheduleRepository = shelterScheduleRepository;
            _sheltersRepository = sheltersRepository;
            _usersRepository = usersRepository;
            _roleVerificationService = roleVerificationService;
        }

        public Response GetSchedulesByShelter(int shelterId)
        {
            Response response = new Response();

            var shelter = _sheltersRepository.GetById(shelterId);

            if (shelter == null)
            {
                response.statusCode = 404;
                response.message = "Shelter not found";
                return response;
            }

            var schedules = _shelterScheduleRepository.GetSchedulesByShelter(shelterId);
            response = new ResponseCollection<ShelterSchedule>(200, "Ok", schedules.ToList());
            return response;
        }

        public Response AddSchedule(ShelterScheduleDTO model, string email)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);
            response = _roleVerificationService.VerifyShelterAdmin(user);
            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var shelter = _sheltersRepository.GetByAdminId(user.Id);
            if (shelter == null)
            {
                response.statusCode = 403;
                response.message = "Unauthorized";
                return response;
            }

            if (model.openingTime >= model.closingTime && !model.isClosed)
            {
                response.statusCode = 400;
                response.message = "Opening time must be before closing time";
                return response;
            }

            var existingSchedules = _shelterScheduleRepository.GetSchedulesByShelter(shelter.Id);
            var existingSchedule = existingSchedules.FirstOrDefault(s => s.day == model.day);
            if (existingSchedule != null)
            {
                response.statusCode = 400;
                response.message = "Schedule for this day already exists";
                return response;
            }

            ShelterSchedule newSchedule = new ShelterSchedule
            {
                shelterId = shelter.Id, 
                day = model.day,
                openingTime = model.openingTime,
                closingTime = model.closingTime,
                isClosed = model.isClosed
            };

            _shelterScheduleRepository.Save(newSchedule);
            response.statusCode = 200;
            response.message = "Schedule added successfully";
            return response;
        }

        public Response UpdateSchedule (ShelterScheduleDTO model, string email)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);
            response = _roleVerificationService.VerifyShelterAdmin(user);

            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var schedule = _shelterScheduleRepository.GetById(model.Id);

            if (schedule == null)
            {
                response.statusCode = 404;
                response.message = "Schedule not found";
                return response;
            }

            var shelter = _sheltersRepository.GetByAdminId(user.Id);
            if (shelter == null)
            {
                response.statusCode = 403;
                response.message = "Unauthorized";
                return response;
            }

            if (model.openingTime >= model.closingTime && !model.isClosed)
            {
                response.statusCode = 400;
                response.message = "Opening time must be before closing time";
                return response;
            }

            schedule.day = model.day;
            schedule.openingTime = model.openingTime;
            schedule.closingTime = model.closingTime;
            schedule.isClosed = model.isClosed;

            _shelterScheduleRepository.Save(schedule);

            response.statusCode = 200;
            response.message = "Schedule updated successfully";
            return response;
        }

        public Response DeleteSchedule(int id, string email)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);
            response = _roleVerificationService.VerifyShelterAdmin(user);

            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var schedule = _shelterScheduleRepository.GetById(id);
            if (schedule == null)
            {
                response.statusCode = 404;
                response.message = "Schedule not found";
                return response;
            }

            var shelter = _sheltersRepository.GetByAdminId(user.Id);
            if (shelter == null)
            {
                response.statusCode = 403;
                response.message = "Unauthorized";
                return response;
            }

            _shelterScheduleRepository.Remove(schedule);

            response.statusCode = 200;
            response.message = "Schedule deleted successfully";
            return response;
        }

        public Response SetWeekSchedule(WeekScheduleDTO model, string email)
        {
            Response response = new Response();

            var user = _usersRepository.GetByEmail(email);
            response = _roleVerificationService.VerifyShelterAdmin(user);

            if (response.statusCode != 200)
            {
                response.statusCode = 403;
                response.message = "Invalid credentials";
                return response;
            }

            var shelter = _sheltersRepository.GetByAdminId(user.Id);
            if (shelter == null)
            {
                response.statusCode = 403;
                response.message = "Unauthorized";
                return response;
            }

            foreach (var scheduleDTO in model.Schedules)
            {
                if (scheduleDTO.openingTime >= scheduleDTO.closingTime && !scheduleDTO.isClosed)
                {
                    response.statusCode = 400;
                    response.message = $"Opening time must be before closing time for {scheduleDTO.day}";
                    return response;
                }
            }

            var existingSchedules = _shelterScheduleRepository.GetSchedulesByShelter(model.shelterId);
            if (existingSchedules.Any())
            {
                _shelterScheduleRepository.RemoveRange(existingSchedules);
            }

            foreach (var scheduleDTO in model.Schedules)
            {
                ShelterSchedule newSchedule = new ShelterSchedule
                {
                    shelterId = model.shelterId,
                    day = scheduleDTO.day,
                    openingTime = scheduleDTO.openingTime,
                    closingTime = scheduleDTO.closingTime,
                    isClosed = scheduleDTO.isClosed
                };

                _shelterScheduleRepository.Save(newSchedule);
            }

            response.statusCode = 200;
            response.message = "Week schedule set successfully";
            return response;
        }
    }
}
