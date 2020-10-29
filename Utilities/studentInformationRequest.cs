using MedismartsAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Utilities
{
    public class studentInformationRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string Level { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Faculty { get; set; }
        public string GuardianName { get; set; }

        public static explicit operator StudentInformation(studentInformationRequest r)
        {
            return new StudentInformation()
            {
                EmailAddress = r.EmailAddress,
                 Country = r.Country,
                  DateOfBirth = DateTime.Parse(r.DateOfBirth),
                   DateRegistered = r.DateRegistered,
                    Department = r.Department,
                    Faculty = r.Faculty,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                     GuardianName = r.GuardianName,
                     Level = r.Level

                   
            };
        }

        public static implicit operator studentInformationRequest(StudentInformation r)
        {
            return new studentInformationRequest()
            {
                EmailAddress = r.EmailAddress,
                Country = r.Country,
                DateOfBirth = r.DateOfBirth.ToString(),
                DateRegistered = r.DateRegistered,
                Department = r.Department,
                Faculty = r.Faculty,
                FirstName = r.FirstName,
                LastName = r.LastName,
                GuardianName = r.GuardianName,
                Level = r.Level


            };
        }
    }
}
