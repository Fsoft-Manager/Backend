using Backend.Data;
using Backend.Data.Entities;
using Backend.DTOs;
using Backend.Repository.BaseRepository;

namespace Backend.Repository.ScheduleRepository
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(DataContext context) : base(context)
        {
        }

        public async Task<ResponseDTO> InsertDataSchedule(List<List<string>> schedules)
        {
            try
            {
                return null;
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
