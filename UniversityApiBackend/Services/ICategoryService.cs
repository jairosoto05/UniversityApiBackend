using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<CategoryResDTO>>> GetCategorysAsync();
        Task<ServiceResponse<CategoryResDTO>> GetCategoryByIdAsync(int id);
        Task<ServiceResponse<CategoryResDTO>> PostCategoryAsync(CategoryReqDTO CategoryDto);
        Task<ServiceResponse<CategoryResDTO>> PutCategoryAsync(int id, CategoryReqDTO Category);
        Task<ServiceResponse<CategoryResDTO>> DeleteCategoryAsync(int id);

    }
}
