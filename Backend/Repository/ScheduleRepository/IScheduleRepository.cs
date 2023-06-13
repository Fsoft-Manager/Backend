using Backend.Data.Entities;
using Backend.DTOs;
using Backend.Repository.BaseRepository;

namespace Backend.Repository.ScheduleRepository
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Task<ResponseDTO> InsertDataSchedule(List<List<string>> schedules);

    }
}
