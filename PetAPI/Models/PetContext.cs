using Microsoft.EntityFrameworkCore;
using PetAPI.Models.Entities;

namespace PetAPI.Models
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options) 
        {

        }
        public DbSet<User> Users { get; set; }

    }
}
