using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Backend.Data.Entities
{
    public class Account
    {
        [Key]
        [MaxLength(10)]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public string? Avatar { get; set; }
        [MaxLength(12)]
        [Phone]
        public string? Phone { get; set; }
        public DateTime? Dob { get; set; }
        [DefaultValue(false)]
        public bool IsDelete { get; set; } = false;
        [MaxLength(512)]
        public string Token { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 6);
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Gender { get; set; } = true;


        [Required]
        [Range(0, 100)]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }


        public virtual List<Schedule> Schedules { get; set; }
    }
}
