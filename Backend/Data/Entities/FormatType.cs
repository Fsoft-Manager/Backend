using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class FormatType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
