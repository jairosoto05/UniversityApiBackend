using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<StudentResDTO>>> GetStudentsAsync();
        Task<ServiceResponse<StudentResDTO>> GetStudentByIdAsync(int id);
        Task<ServiceResponse<StudentResDTO>> PostStudentAsync(StudentReqDTO StudentDto);
        Task<ServiceResponse<StudentResDTO>> PutStudentAsync(int id, StudentReqDTO Student);
        Task<ServiceResponse<StudentResDTO>> DeleteStudentAsync(int id);

    }
}
