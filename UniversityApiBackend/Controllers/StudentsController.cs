using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResDTO>>> GetStudents()
        {
            var students = await _service.GetStudentsAsync();

            if (students.Message == "NotFound")
            {
                students.Message = $"No Student in Database";
                return StatusCode(404, students);
            }

            return StatusCode(StatusCodes.Status200OK, students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResDTO>> GetStudent(int id)
        {
            var course = await _service.GetStudentByIdAsync(id);

            if (course.Success == false & course.Message == "NotFound")
            {
                course.Message = $"No Student found for id: {id}";
                return StatusCode(404, course);
            }

            return StatusCode(StatusCodes.Status200OK, course);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResDTO>> PutStudent(int id, StudentReqDTO Student)
        {
            var courseModified = await _service.PutStudentAsync(id, Student);
            if (courseModified.Success == false & courseModified.Message == "NotFound")
            {
                courseModified.Message = $"No Student found for id: {id}";
                return StatusCode(404, courseModified);
            }
            return Ok(courseModified);
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentResDTO>> PostStudent(StudentReqDTO StudentDto)
        {
            var course = await _service.PostStudentAsync(StudentDto);
            //if (course.Data == null & course.Message == "CatNotFound")
            //{
            //    return StatusCode(404, $"No Category found for id: {StudentDto.Category.Id}");
            //};

            return Ok(course);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var course = await _service.DeleteStudentAsync(id);
            if (course.Success == false & course.Message == "NotFound")
            {
                course.Message = $"No Student found for id: {id}";
                return StatusCode(404, course);
            }
            return StatusCode(StatusCodes.Status200OK, course);
        }

    }
}