using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Data.Seed
{
    public class SeedData
    {
        public static async void SeedUsers(DataContext context)
        {
            if (context.Roles.Any()) return;
            List<Role> roles = new List<Role>()
            {
                new Role() {Name="admin"},
                new Role() {Name="student"},
                new Role() {Name="trainer"},
            };
            await context.Roles.AddRangeAsync(roles);

            context.SaveChanges();
        }
    }
}
