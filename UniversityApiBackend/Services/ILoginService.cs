using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ILoginService
    {
        Task<ServiceResponse<string>> LoginAsync(LoginUser loginUser);
    }
}
