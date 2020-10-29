using MedismartsAPI.Model;
using MedismartsAPI.Services;
using MedismartsAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Controllers
{
   // [Route(ApiRoutes.baseUrl)]
    [ApiController]
    public class StudentController:ControllerBase
    {
        private StudentsService _service;
        private IUtilityService utility;
        private Response _reponse = null;
        public StudentController(StudentsService service, IUtilityService utilityService)
        {
            this._service = service;
            this.utility = utilityService;
        }
        [Route("api/Student/getAll")]
        [HttpGet]
        [ServiceFilter(typeof(HandleException))]
        public async Task<IActionResult> GetAllStudent()
        {
            var result = _service.GetAll();

            _reponse = utility.GetResponse("00", "students list retrieved", result);

            return Ok(_reponse);
        }


        [Route("api/Student/getStudentById/{studentId}")]
        [HttpGet]
        [ServiceFilter(typeof(HandleException))]
        public async Task<IActionResult> GetStudentById([FromRoute]long studentId)
        {
            var result = _service.GetStudent(studentId);

            _reponse = utility.GetResponse("00", "students list retrieved", result);

            return Ok(_reponse);
        }


        [Route("api/Student/deleteStudent/{studentId}")]
        [HttpDelete]
        [ServiceFilter(typeof(HandleException))]
        public async Task<IActionResult> DeleteStudent([FromRoute]long studentId)
        {
            var result = _service.Delete(studentId);

            if (result)
            {
                _reponse = utility.GetResponse("00", "students deleted successufully", null);

                return Ok(_reponse);
            }
            else
            {
                _reponse = utility.GetResponse("99", "students failed to delete", null);

                return BadRequest(_reponse);
            }
        }

        [Route("api/Student/updateStudent/{studentId}")]
        [HttpPut]
        [ServiceFilter(typeof(HandleException))]
        public async Task<IActionResult> UpdateStudent([FromRoute]long studentId, [FromBody] studentInformationRequest request)
        {
            var studentGetSingle = _service.GetStudent(studentId);

            if(studentGetSingle != null)
            {
                var studentInfo = (StudentInformation)request;
                var result = _service.UpdateStudent(studentGetSingle,studentInfo);

                if (result)
                {
                    _reponse = utility.GetResponse("00", "students updated successufully", null);

                    return Ok(_reponse);
                }
                else
                {
                    _reponse = utility.GetResponse("99", "students failed to update", null);

                    return BadRequest(_reponse);
                }
            }
            else
            {
                _reponse = utility.GetResponse("99", $"no match found for student with id {studentId}", null);

                return BadRequest(_reponse);
            }
            
        }

        [Route("api/Student/addNewStudent")]
        [HttpPost]
        [ServiceFilter(typeof(HandleException))]
        public async Task<IActionResult> AddNewStudent([FromBody] studentInformationRequest request)
        {
            request.DateRegistered = DateTime.Now;
            var studentInfo = (StudentInformation)request;
            var result = _service.AddNewStudent(studentInfo);

            if(result)
            {
                _reponse = utility.GetResponse("00", "students created successufully", null);

                return Ok(_reponse);
            }
            else
            {
                _reponse = utility.GetResponse("99", "students failed to create", null);

                return BadRequest(_reponse);
            }

           
        }
    }
}
