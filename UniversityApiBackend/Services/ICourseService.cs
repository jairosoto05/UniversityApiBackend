using UniversityApiBackend.DTO;

namespace UniversityApiBackend.Services
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<CourseResDTO>>> GetCoursesAsync();
        Task<ServiceResponse<CourseResDTO>> GetCourseByIdAsync(int id);
        Task<ServiceResponse<CourseResDTO>> PostCourseAsync(CourseReqDTO CourseDto);
        Task<ServiceResponse<CourseResDTO>> PutCourseAsync(int id, CourseReqDTO Course);
        Task<ServiceResponse<CourseResDTO>> DeleteCourseAsync(int id);

    }
}
