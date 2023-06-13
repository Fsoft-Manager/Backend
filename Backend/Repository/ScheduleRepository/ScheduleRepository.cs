using Backend.Data;
using Backend.Data.Entities;
using Backend.DTOs;
using Backend.Repository.BaseRepository;
using Microsoft.AspNetCore.Connections.Features;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                var length = schedules.Count();
                string nameCourse = schedules[1][1];
                DateTime startDate = DateTime.Parse(schedules[2][2]);
                DateTime endDate = DateTime.Parse(schedules[3][2]);
                var isExistCourse = context.Courses.Where(e => e.Name.Equals(nameCourse)).FirstOrDefault();
                if (isExistCourse == null)
                {
                    Course course = new Course();
                    course.Name = nameCourse;
                    course.Start = startDate;
                    course.End = endDate;
                    await context.Courses.AddAsync(course);
                }
                else
                {
                    for (int i = 5; i < length; i++)
                    {
                        if (!schedules[i][3].Equals("-") && !schedules[i][5].Equals("-") && !schedules[i][6].Equals("-"))
                        {
                            Schedule schedule = new Schedule();
                            schedule.Topic = schedules[i][0];
                            schedule.FormatTypeId = (context.FormatTypes.Where(e => e.Name.Equals(schedules[i][1])).FirstOrDefault()).Id ;
                            schedule.FromDate = DateTime.Parse(schedules[i][5]);
                            schedule.ToDate = DateTime.Parse(schedules[i][6]);
                            schedule.LearningTime = ((schedule.ToDate - schedule.FromDate).TotalHours) 
                                + (((schedule.ToDate - schedule.FromDate).TotalMinutes) / 60) ;

                            if(schedules[i][3].Contains("/") || schedules[i][3].Contains("->") || schedules[i][3].Contains("-->"))
                            {

                            }

                            schedule.Trainer = new List<Account>() ;
                            


                        }
                        
                    }
                }


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
