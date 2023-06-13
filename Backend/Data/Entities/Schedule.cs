using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Backend.Data.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public string Topic { get; set; }

        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }

        // learning time = toDate - fromDate 
        public double LearningTime { get; set; }

        //public double LearningTime => ((ToDate - FromDate).TotalHours) + (((ToDate - FromDate).TotalMinutes) /60);


        public int CourseId { get; set; }
        public virtual Course Course {  get; set; }

        public int FormatTypeId { get; set; }
        public virtual FormatType FormatType { get; set; }

        public int TypeOfTrainId { get; set; }
        public virtual TypeOfTrain TypeOfTrain { get; set; }

        public virtual List<Account> Trainer { get; set; }

    }
}
