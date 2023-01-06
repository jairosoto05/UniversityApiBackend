using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResDTO>> GetCategoriesAsync();
        Task<CategoryResDTO> GetCategoryByIdAsync(int id);
        Task<CategoryResDTO> PostCategoryAsync(CategoryReqDTO categoryDto);
        Task<CategoryResDTO> PutCategoryAsync(int id, CategoryReqDTO category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
