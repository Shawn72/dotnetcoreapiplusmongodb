using CorePlusMongoDBApi.Models;
using CorePlusMongoDBApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorePlusMongoDBApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;
        private readonly CategoryService _categoryService;

        public ProductsController(ProductsService pService, CategoryService cService)
        {
            _productsService = pService;
            _categoryService = cService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAll()
        {
            var products = await _productsService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetById(string id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (product.Categories.Count > 0)
            {
                var tempList = new List<Category>();
                foreach (var courseId in product.Categories)
                {
                    var category = await _categoryService.GetByIdAsync(courseId);
                    if (category != null)
                        tempList.Add(category);
                }
                product.CategoryList = tempList;
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _productsService.CreateAsync(product);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Products updatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var queriedProduct = await _productsService.GetByIdAsync(id);
            if (queriedProduct == null)
            {
                return NotFound();
            }

            await _productsService.UpdateAsync(id, updatedProduct);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productsService.DeleteAsync(id);

            return NoContent();
        }
    }
}

