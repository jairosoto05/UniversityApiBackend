using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class UserService : IUserService
    {
        private readonly UniversityDBContext _context;
        private readonly IMapper _mapper;

        public UserService(UniversityDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<UserResDTO>>> GetUsersAsync()
        {
            ServiceResponse<List<UserResDTO>> _response = new();
            try
            {
                var Users = await _context.USERS!.ToListAsync();
                if (Users == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _mapper.Map<List<UserResDTO>>(Users);

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

        public async Task<ServiceResponse<UserResDTO>> GetUserByIdAsync(int id)
        {
            ServiceResponse<UserResDTO> _response = new();
            try
            {
                var user = await _context.USERS!.FirstOrDefaultAsync(c => c.Id == id);
                if (user == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "OK";
                _response.Data = _mapper.Map<UserResDTO>(user);
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

        public async Task<ServiceResponse<UserResDTO>> PostUserAsync(UserReqDTO UserDto)
        {
            ServiceResponse<UserResDTO> _response = new();
            try
            {
                var user = new User()
                {
                    Name = UserDto.Name,
                    LastName = UserDto.LastName,
                    Username = UserDto.Username,
                    Email = UserDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(UserDto.Password),
                    Rol = UserDto.Rol,
                };

                _context.USERS!.Add(user);
                await _context.SaveChangesAsync();

                _response.Data = _mapper.Map<UserResDTO>(user);
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

        public async Task<ServiceResponse<UserResDTO>> PutUserAsync(int id, UserReqDTO UserDto)
        {
            ServiceResponse<UserResDTO> _response = new();
            try
            {
                var user = new User()
                {
                    Id= id,
                    Name = UserDto.Name,
                    LastName = UserDto.LastName,
                    Username = UserDto.Username,
                    Email = UserDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(UserDto.Password),
                    Rol = UserDto.Rol,
                };

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _response.Data = null!;
                _response.Success = true;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                if (!UserExists(id))
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

            return await GetUserByIdAsync(id);
        }

        public async Task<ServiceResponse<UserResDTO>> DeleteUserAsync(int id)
        {
            ServiceResponse<UserResDTO> _response = new();
            try
            {
                var user = await _context.USERS!.FindAsync(id);
                if (user == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _context.USERS.Remove(user);
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




        private bool UserExists(int id)
        {
            return _context.USERS!.Any(e => e.Id == id);
        }
    }
}
