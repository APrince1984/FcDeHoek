
using System;

namespace WebAppFcDeHoek.Models
{
    public class PersonModel
    {
        public int IdPerson { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public int? PerkezNumber { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsStaff { get; set; }
        public string Picture { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? PhoneNumber { get; set; }
        
    }
}