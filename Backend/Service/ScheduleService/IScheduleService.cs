using Backend.DTOs;

namespace Backend.Service.ScheduleService
{
    public interface IScheduleService
    {
        Task<ResponseDTO> InsertDataSchedule(List<List<string>> schedules);
    }
}
