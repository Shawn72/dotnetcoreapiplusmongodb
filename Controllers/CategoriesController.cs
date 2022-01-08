using Microsoft.AspNetCore.Mvc;
using CorePlusMongoDBApi.Models;
using CorePlusMongoDBApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorePlusMongoDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {      
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService cService)
        {         
            _categoryService = cService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var category = await _categoryService.GetAllAsync();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _categoryService.CreateAsync(category);
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Category updatedCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var queriedCategory = await _categoryService.GetByIdAsync(id);
            if (queriedCategory == null)
            {
                return NotFound();
            }

            await _categoryService.UpdateAsync(id, updatedCategory);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteAsync(id);

            return NoContent();
        }
    }
}
