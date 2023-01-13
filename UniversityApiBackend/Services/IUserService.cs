using UniversityApiBackend.DTO;

namespace UniversityApiBackend.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserResDTO>>> GetUsersAsync();
        Task<ServiceResponse<UserResDTO>> GetUserByIdAsync(int id);
        Task<ServiceResponse<UserResDTO>> PostUserAsync(UserReqDTO UserDto);
        Task<ServiceResponse<UserResDTO>> PutUserAsync(int id, UserReqDTO User);
        Task<ServiceResponse<UserResDTO>> DeleteUserAsync(int id);
    }
}
