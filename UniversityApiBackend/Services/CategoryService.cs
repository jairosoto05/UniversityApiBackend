using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<List<CategoryResDTO>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.CATEGORIES.Include(c => c.Courses)
                            .ToListAsync();
                return _mapper.Map<List<CategoryResDTO>>(categories);

            }
            catch (Exception)
            {
                return null!;
            }
            
        }

        public async Task<CategoryResDTO> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.CATEGORIES.Include(c => c.Courses)
                            .FirstOrDefaultAsync(c => c.Id == id);
                return _mapper.Map<CategoryResDTO>(category);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<CategoryResDTO> PostCategoryAsync(CategoryReqDTO categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                _context.CATEGORIES.Add(category);
                await _context.SaveChangesAsync();

                return _mapper.Map<CategoryResDTO>(category);
            }
            catch (Exception)
            { 
                return null!; 
            }

        }

        public async Task<CategoryResDTO> PutCategoryAsync(int id, CategoryReqDTO category)
        {
            try
            {
                Category categoryModified = new() { Id = id, CategoryName = category.CategoryName };
                _context.Entry(categoryModified).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!CategoryExists(id))
                {
                    return null!;
                }
                else
                {
                    throw;
                }
            }

            return await GetCategoryByIdAsync(id);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.CATEGORIES.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.CATEGORIES.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }




        private bool CategoryExists(int id)
        {
            return _context.CATEGORIES.Any(e => e.Id == id);
        }
    }
}
