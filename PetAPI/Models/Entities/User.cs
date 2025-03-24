﻿using PetAPI.Models.Enums;

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
    }
}
