using System.ComponentModel.DataAnnotations;

namespace Backend.Data.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }
}
