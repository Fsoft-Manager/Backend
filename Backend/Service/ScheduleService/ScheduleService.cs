using Backend.DTOs;
using Backend.Repository.ScheduleRepository;

namespace Backend.Service.ScheduleService
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ResponseDTO> InsertDataSchedule(List<List<string>> schedules)
        {
			try
			{
                var result = await _scheduleRepository.InsertDataSchedule(schedules);
                return result;
			}
			catch (Exception ex)
			{
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }
    }
}
