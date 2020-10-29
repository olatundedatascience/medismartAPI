using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Utilities
{
    public class ApiRoutes
    {
        public const string baseUrl = "api/Student";

        public class Student
        {
            public const string getAll = "/getAll";
            public const string addNewStudent =  "/addNew";
            public const string deleteStudent =  "/deleteStudent/{studentId}";
            public const string updateStudent =  "/updateStudent/{studentId}";
            public const string getStudentById =  "/updateStudent/{studentId}";
        }

        
    }
}
