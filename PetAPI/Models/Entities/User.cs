using PetAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetAPI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string phone {  get; set; }
        public string dni { get; set; }
        public string email { get; set; }
        public byte[] hash { get; set; }
        public byte[] salt { get; set; }
        public Role role { get; set; }

        public ICollection<Shelter> AdministeredShelters { get; set; }
        public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
        public ICollection<Donation> Donations { get; set; }
    }
}
