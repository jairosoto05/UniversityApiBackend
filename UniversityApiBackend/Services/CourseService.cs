using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CourseService : ICourseService
    {
        private readonly UniversityDBContext _context;
        private readonly IMapper _mapper;

        public CourseService(UniversityDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<CourseResDTO>>> GetCoursesAsync()
        {
            ServiceResponse<List<CourseResDTO>> _response = new();
            try
            {
                var courses = await _context.COURSES!.Include(c => c.Category).Include(c => c.Students).ToListAsync();
                if(courses == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _mapper.Map<List<CourseResDTO>>(courses);

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

        public async Task<ServiceResponse<CourseResDTO>> GetCourseByIdAsync(int id)
        {
            ServiceResponse<CourseResDTO> _response = new();
            try
            {
                var course = await _context.COURSES!.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
                if (course == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "OK";
                _response.Data = _mapper.Map<CourseResDTO>(course);
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

        public async Task<ServiceResponse<CourseResDTO>> PostCourseAsync(CourseReqDTO CourseDto)
        {
            ServiceResponse<CourseResDTO> _response = new();
            try
            {
                var course = new Course()
                {
                    Name = CourseDto.Name,
                    ShortDescription = CourseDto.ShortDescription,
                    Description = CourseDto.Description,
                    Level = CourseDto.Level
                };

                ////////////////Category///////////////////
                var category = new Category();
                if (CourseDto.Category != null)
                {
                    category = _context.CATEGORIES!.FirstOrDefault(c => c.Id == CourseDto.Category.Id);
                    if(category == null)
                    {
                        _response.Success= false;
                        _response.Message= "Category NotFound";
                        return _response;
                    }
                    course.Category = category;
                }
         

                //////////////////Students/////////////////
                var students = new Student();
                if (CourseDto.Students.Count > 0)
                {
                    foreach (StudentMinDTO S in CourseDto.Students)
                    {
                        students = _context.STUDENTS!.FirstOrDefault(s => s.Id == S.Id);
                        if (students == null)
                        {
                            _response.Success = false;
                            _response.Message = $"Student NotFound:{S.Id}";
                            return _response;
                        }
                        course.Students!.Add(students);
                    }
                }

                _context.COURSES!.Add(course);
                await _context.SaveChangesAsync();
                _response.Data = _mapper.Map<CourseResDTO>(course);
                _response.Success = true;
                _response.Message = "Created";

            }
            catch (Exception ex)
            {
                _response.Data = null!;
                _response.Success= false;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                return _response;
            }
            return _response;
        }

        public async Task<ServiceResponse<CourseResDTO>> PutCourseAsync(int id, CourseReqDTO CourseDto)
        {
            ServiceResponse<CourseResDTO> _response = new();
            try
            {
                var course = new Course()
                {
                    Id = id,
                    Name = CourseDto.Name,
                    ShortDescription = CourseDto.ShortDescription,
                    Description = CourseDto.Description,
                    Level = CourseDto.Level,
                };

                ////////////////Category///////////////////
                var category = new Category();
                if (CourseDto.Category != null)
                {
                    category = _context.CATEGORIES!.FirstOrDefault(c => c.Id == CourseDto.Category.Id);
                    if (category == null)
                    {
                        _response.Success = false;
                        _response.Message = "Category NotFound";
                        return _response;
                    }
                    course.Category = category;
                }
                _context.COURSES!.Add(course);

                //////////////////Students/////////////////
                var students = new Student();
                if (CourseDto.Students.Count > 0)
                {
                    foreach (StudentMinDTO S in CourseDto.Students)
                    {
                        students = _context.STUDENTS!.FirstOrDefault(s => s.Id == S.Id);
                        if (students == null)
                        {
                            _response.Success = false;
                            _response.Message = $"Student NotFound:{S.Id}";
                            return _response;
                        }
                        course.Students!.Add(students);
                    }
                }

                _context.Entry(course).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _response.Data = null!;
                _response.Success = true;
                _response.Message= "OK";
            }
            catch (Exception ex)
            {
                if (!CourseExists(id))
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

            return await GetCourseByIdAsync(id);
        }

        public async Task<ServiceResponse<CourseResDTO>> DeleteCourseAsync(int id)
        {
            ServiceResponse<CourseResDTO> _response = new();
            try
            {
                var course = await _context.COURSES!.FindAsync(id);
                if (course == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _context.COURSES.Remove(course);
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




        private bool CourseExists(int id)
        {
            return _context.COURSES!.Any(e => e.Id == id);
        }
    }
}
