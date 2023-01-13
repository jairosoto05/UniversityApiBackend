using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentService : IStudentService
    {
        private readonly UniversityDBContext _context;
        private readonly IMapper _mapper;

        public StudentService(UniversityDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<StudentResDTO>>> GetStudentsAsync()
        {
            ServiceResponse<List<StudentResDTO>> _response = new();
            try
            {
                var Students = await _context.STUDENTS!.Include(c => c.Courses).ToListAsync();
                if (Students == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _mapper.Map<List<StudentResDTO>>(Students);

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null!;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

            }
            return _response;

        }

        public async Task<ServiceResponse<StudentResDTO>> GetStudentByIdAsync(int id)
        {
            ServiceResponse<StudentResDTO> _response = new();
            try
            {
                var student = await _context.STUDENTS!.Include(c => c.Courses).FirstOrDefaultAsync(c => c.Id == id);
                if (student == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "OK";
                _response.Data = _mapper.Map<StudentResDTO>(student);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null!;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<StudentResDTO>> PostStudentAsync(StudentReqDTO StudentDto)
        {
            ServiceResponse<StudentResDTO> _response = new();
            try
            {
                var student = new Student()
                {
                    FirstName = StudentDto.FirstName,
                    LastName = StudentDto.LastName,
                    Dob = StudentDto.Dob
                };
                var course = new Course();
                if (StudentDto.Courses.Count > 0)
                {
                    foreach(CourseMinDTO C in StudentDto.Courses)
                    {
                        course = _context.COURSES!.FirstOrDefault(c => c.Id == C.Id);
                        if (course == null)
                        {
                            _response.Success = false;
                            _response.Message = "Course NotFound";
                            return _response;
                        }
                        student.Courses.Add(course);
                    }
                }
                _context.STUDENTS!.Add(student);
                await _context.SaveChangesAsync();

                _response.Data = _mapper.Map<StudentResDTO>(student);
                _response.Success = true;
                _response.Message = "Created";

            }
            catch (Exception ex)
            {
                _response.Data = null!;
                _response.Success = false;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                return _response;
            }
            return _response;
        }

        public async Task<ServiceResponse<StudentResDTO>> PutStudentAsync(int id, StudentReqDTO StudentDto)
        {
            ServiceResponse<StudentResDTO> _response = new();
            try
            {
                var student = new Student()
                {
                    Id = id,
                    FirstName = StudentDto.FirstName,
                    LastName = StudentDto.LastName,
                    Dob = StudentDto.Dob
                };
                var course = new Course();
                if (StudentDto.Courses.Count > 0)
                {
                    foreach (CourseMinDTO C in StudentDto.Courses)
                    {
                        course = _context.COURSES!.FirstOrDefault(c => c.Id == C.Id);
                        if (course == null)
                        {
                            _response.Success = false;
                            _response.Message = "Course NotFound";
                            return _response;
                        }
                        student.Courses.Add(course);
                    }
                }
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _response.Data = null!;
                _response.Success = true;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                if (!StudentExists(id))
                {
                    _response.Data = null!;
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                else
                {
                    _response.Data = null!;
                    _response.Success = false;
                    _response.Message = "Error";
                    _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                    return _response;
                }
            }

            return await GetStudentByIdAsync(id);
        }

        public async Task<ServiceResponse<StudentResDTO>> DeleteStudentAsync(int id)
        {
            ServiceResponse<StudentResDTO> _response = new();
            try
            {
                var student = await _context.STUDENTS!.FindAsync(id);
                if (student == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _context.STUDENTS.Remove(student);
                await _context.SaveChangesAsync();

                _response.Success = true;
                _response.Message = "Deleted";
                _response.Data = null!;

            }
            catch (Exception ex)
            {
                _response.Data = null!;
                _response.Success = false;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                return _response;
            }

            return _response;
        }




        private bool StudentExists(int id)
        {
            return _context.STUDENTS!.Any(e => e.Id == id);
        }
    }
}
