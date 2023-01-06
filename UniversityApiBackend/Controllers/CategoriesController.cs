using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResDTO>>> GetCATEGORIES()
        {
            var categories = await _service.GetCategoriesAsync();

            if(categories == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No categories in database");
            }

            return StatusCode(StatusCodes.Status200OK, categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResDTO>> GetCategory(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"No Category found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResDTO>> PutCategory(int id, CategoryReqDTO category)
        {
            var categoryModified = await _service.PutCategoryAsync(id, category);
            if (categoryModified == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"No Category found for id: {id}");
            }
            return categoryModified;
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryResDTO>> PostCategory(CategoryReqDTO categoryDto)
        {
            var category = await _service.PostCategoryAsync(categoryDto);
            return CreatedAtAction("GetCategory", new {id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var isDeleted = await _service.DeleteCategoryAsync(id);
                if (!isDeleted)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"No Category found for id: {id}");
                }
                return StatusCode(StatusCodes.Status200OK, "Category eliminated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

    }
}
