using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Authorize(Roles = ("Admin"))]
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
        public async Task<ActionResult<IEnumerable<CategoryResDTO>>> GetCategorys()
        {
            var categorys = await _service.GetCategorysAsync();

            if (categorys.Message == "NotFound")
            {
                categorys.Message = $"No Category in Database";
                return StatusCode(404, categorys);
            }

            return StatusCode(StatusCodes.Status200OK, categorys);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResDTO>> GetCategory(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);

            if (category.Success == false & category.Message == "NotFound")
            {
                category.Message = $"No Category found for id: {id}";
                return StatusCode(404, category);
            }

            return StatusCode(StatusCodes.Status200OK, category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResDTO>> PutCategory(int id, CategoryReqDTO Category)
        {
            var categoryModified = await _service.PutCategoryAsync(id, Category);
            if (categoryModified.Success == false & categoryModified.Message == "NotFound")
            {
                categoryModified.Message = $"No Category found for id: {id}";
                return StatusCode(404, categoryModified);
            }
            return Ok(categoryModified);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryResDTO>> PostCategory(CategoryReqDTO CategoryDto)
        {
            var category = await _service.PostCategoryAsync(CategoryDto);
            return Ok(category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _service.DeleteCategoryAsync(id);
            if (category.Success == false & category.Message == "NotFound")
            {
                category.Message = $"No Category found for id: {id}";
                return StatusCode(404, category);
            }
            else if (category.Success == false & category.Message == "HasCourses")
            {
                category.Message = "Courses have this category";
                return StatusCode(StatusCodes.Status400BadRequest, category);
            }
            return StatusCode(StatusCodes.Status200OK, category);
        }

    }
}
