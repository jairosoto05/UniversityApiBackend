﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly UniversityDBContext _context;
        private readonly IMapper _mapper;

        public CategoryService(UniversityDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<CategoryResDTO>>> GetCategorysAsync()
        {
            ServiceResponse<List<CategoryResDTO>> _response = new();
            try
            {
                var categories = await _context.CATEGORIES!.Include(c => c.Courses).ToListAsync();
                if (categories == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _mapper.Map<List<CategoryResDTO>>(categories);

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

        public async Task<ServiceResponse<CategoryResDTO>> GetCategoryByIdAsync(int id)
        {
            ServiceResponse<CategoryResDTO> _response = new();
            try
            {
                var category = await _context.CATEGORIES!.Include(c => c.Courses).FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                _response.Success = true;
                _response.Message = "OK";
                _response.Data = _mapper.Map<CategoryResDTO>(category);
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

        public async Task<ServiceResponse<CategoryResDTO>> PostCategoryAsync(CategoryReqDTO CategoryDto)
        {
            ServiceResponse<CategoryResDTO> _response = new();
            try
            {
                var category = new Category()
                {
                    Name = CategoryDto.Name,
                };
                _context.CATEGORIES!.Add(category);
                await _context.SaveChangesAsync();

                _response.Data = _mapper.Map<CategoryResDTO>(category);
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

        public async Task<ServiceResponse<CategoryResDTO>> PutCategoryAsync(int id, CategoryReqDTO CategoryDto)
        {
            ServiceResponse<CategoryResDTO> _response = new();
            try
            {
                Category CategoryModified = new() { Id = id, Name = CategoryDto.Name };
                _context.Entry(CategoryModified).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _response.Data = null!;
                _response.Success = true;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                if (!CategoryExists(id))
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

            return await GetCategoryByIdAsync(id);
        }

        public async Task<ServiceResponse<CategoryResDTO>> DeleteCategoryAsync(int id)
        {
            ServiceResponse<CategoryResDTO> _response = new();
            try
            {
                var category = await _context.CATEGORIES!.Include(c => c.Courses).FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }
                else if (category.Courses != null)
                {
                    _response.Success = false;
                    _response.Message = "HasCourses";
                    return _response;
                }
                _context.CATEGORIES!.Remove(category);
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




        private bool CategoryExists(int id)
        {
            return _context.CATEGORIES!.Any(e => e.Id == id);
        }
    }
}
