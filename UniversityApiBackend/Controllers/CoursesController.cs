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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResDTO>>> GetCourses()
        {
            var courses = await _service.GetCoursesAsync();

            if (courses.Message == "NotFound")
            {
                courses.Message = $"No Course in Database";
                return StatusCode(404, courses);
            }

            return StatusCode(StatusCodes.Status200OK, courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResDTO>> GetCourse(int id)
        {
            var course = await _service.GetCourseByIdAsync(id);

            if (course.Success == false & course.Message == "NotFound")
            {
                course.Message = $"No Course found for id: {id}";
                return StatusCode(404, course);
            }

            return StatusCode(StatusCodes.Status200OK, course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseResDTO>> PutCourse(int id, CourseReqDTO Course)
        {
            var courseModified = await _service.PutCourseAsync(id, Course);
            if (courseModified.Success == false & courseModified.Message == "Course NotFound")
            {
                courseModified.Message = $"No Course found for id: {id}";
                return StatusCode(404, courseModified);
            }
            else if (courseModified.Data == null & courseModified.Message[..courseModified.Message.IndexOf(':')] == "Student NotFound")
            {
                string idOfStudent = courseModified.Message[(courseModified.Message.IndexOf(':') + 1)..];
                courseModified.Message = $"No Student found for id: {idOfStudent}";
                return StatusCode(404, courseModified);
            }
            return Ok(courseModified);
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseResDTO>> PostCourse(CourseReqDTO CourseDto)
        {
            var course = await _service.PostCourseAsync(CourseDto);
            if (course.Data == null & course.Message == "Category NotFound")
            {
                course.Message = $"No Category found for id: {CourseDto.Category!.Id}";
                return StatusCode(404, course);
            }
            else if (course.Data == null & course.Message[..course.Message.IndexOf(':')] == "Student NotFound")
            {
                string id = course.Message[(course.Message.IndexOf(':')+1) ..];
                course.Message = $"No Student found for id: {id}";
                return StatusCode(404, course);
            }
            return Ok(course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
                var course = await _service.DeleteCourseAsync(id);
                if (course.Success == false & course.Message == "NotFound")
                {
                    course.Message = $"No Course found for id: {id}";
                    return StatusCode(404, course);
            }
                return StatusCode(StatusCodes.Status200OK, course);
        }

    }
}
