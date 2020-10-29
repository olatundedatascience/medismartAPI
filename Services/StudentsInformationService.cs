using MedismartsAPI.Model;
using MedismartsAPI.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Services
{
    public interface StudentsService
    {
        bool AddNewStudent(StudentInformation student);
        bool Delete(StudentInformation student);
        bool Delete(long studentsId);
        bool UpdateStudent(StudentInformation studentToUpdate, StudentInformation modifiedStudent);
        //bool UpdateStudent(StudentInformation student, long Id);
        StudentInformation GetStudent(long Id);
        StudentInformation GetStudent(long Id, string email);
        List<StudentInformation> GetAll();
        List<StudentInformation> GetAll(Func<StudentInformation, bool> filter);
    }
    public class StudentsInformationService : StudentsService
    {
        private IBaseDbRepo<StudentInformation> _studentsRepo; // encapsulated repo for maniputing student information

        private IUtilityService _logger; // for logging
        private bool serviceResult;
        private StudentDbContext _ctx;
        public StudentsInformationService(IUtilityService _logger)
        {
            _studentsRepo = new BaseDbRepo<StudentInformation>();
            _ctx = new StudentDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<StudentDbContext>());
            this._logger = _logger;
            serviceResult = false;
        }
        public bool AddNewStudent(StudentInformation student)
        {
            try
            {
                // log request details
                _logger.LogInfo($"adding student request details:- {JsonConvert.SerializeObject(student)}");
                //_ctx.StudentInformation.Add(student);
                //serviceResult = _ctx.SaveChanges() > 0;
                serviceResult = _studentsRepo.AddNew(student); // add new data to context (in memory) yet to any db operation
                // serviceResult = _studentsRepo.SaveChanges(); // save data to Db

                return serviceResult; // returns outcome of db call (either true or false)
            }
            catch(Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message); 
            }
        }

        public bool Delete(StudentInformation student)
        {
            try
            {
                // log request details
                _logger.LogInfo($"adding student request details:- {JsonConvert.SerializeObject(student)}");
                serviceResult = _studentsRepo.Delete(student); // remove data to context (in memory) yet to any db operation
            

                return serviceResult; // returns outcome of db call (either true or false)
            }
            catch (Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message);
            }
        }

        public bool Delete(long studentsId)
        {
            // this task actually does two things, first retrive student by id and the delete if found
            try
            {
                // retrieve by student ID;
                StudentInformation student = GetStudent(studentsId);

                if(student != null)
                {
                    // log request details
                    _logger.LogInfo($"Deleting student request details:- {JsonConvert.SerializeObject(student)}");
                    serviceResult = _studentsRepo.Delete(student); // save data to Db

                    return serviceResult; // returns outcome of db call (either true or false)
                }
                else
                {
                    serviceResult = false;
                    return serviceResult;
                }
            
            }
            catch (Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message);
            }
        }

        public List<StudentInformation> GetAll()
        {
            try
            {
                return _studentsRepo.GetAll();
            }
            catch (Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message);
            }
        }

        public List<StudentInformation> GetAll(Func<StudentInformation, bool> filter)
        {
            try
            {
                return _studentsRepo.GetAll(filter);
            }
            catch(Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message);
            }
        }

        public StudentInformation GetStudent(long Id)
        {
            try
            {
                var single =  _studentsRepo.GetSingle(x => x.Id == Id);
                return single;
            }
            catch (Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message);
            }
            
        }

        public StudentInformation GetStudent(long Id, string email)
        {
            try
            {
                return _studentsRepo.GetSingle(x => x.Id == Id && x.EmailAddress.ToLower() == email.ToLower()); ;
            }
            catch (Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message);
            }

        }

        public bool UpdateStudent(StudentInformation studentToUpdate, StudentInformation modifiedStudent)
        {
            try
            {
                // log request details
                _logger.LogInfo($"update student request details:- {JsonConvert.SerializeObject(modifiedStudent)}");
                serviceResult = _studentsRepo.update(studentToUpdate, modifiedStudent);
               
                return serviceResult;
            }
            catch(Exception es)
            {
                // throws out exception for custom middleware to handle and log and allow
                // the application to fail gracefully
                throw new Exception(es.Message);
            }
        }

        public bool UpdateStudent(StudentInformation student, long Id)
        {
            try
            {
                // retrieve by student ID;
                StudentInformation studentInfo = GetStudent(Id);

                if (studentInfo != null)
                {
                    // log request details
                    _logger.LogInfo($"update student request details:- {JsonConvert.SerializeObject(student)}");
                    serviceResult = _studentsRepo.update(studentInfo, student);
                    return serviceResult;
                }
                else
                {
                    serviceResult = false;
                    return serviceResult;
                }
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
        }
    }
}
