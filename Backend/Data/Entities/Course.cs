using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
        public virtual List<Account> Students { get; set; }
        public virtual List<Schedule> Schedules { get; set; }
    }
}
