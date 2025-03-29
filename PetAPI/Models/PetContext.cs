using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;
using PetAPI.Models.Entities;

namespace PetAPI.Models
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options) 
        {

        }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<ShelterSchedule> ShelterSchedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
    }
}
