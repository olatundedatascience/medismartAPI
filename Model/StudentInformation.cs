using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Model
{
    public class StudentInformation
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string Level { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Faculty { get; set; }
        public string GuardianName { get; set; }
    }

    public class StudentLevel
    {
        public const string FirstYear = "FirstYear";
        public const string SecondYear = "SecondYear";
        public const string ThirdYear = "ThirdYear";
        public const string FourthYear = "FourthYear";
    }
}
