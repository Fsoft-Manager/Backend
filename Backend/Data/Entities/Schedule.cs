using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        public virtual Class Class { get; set; }
        public virtual Account Mentor { get; set; }
    }
}
