using backend.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category is null)
                return NotFound("Sorryy");
            return Ok(category);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<Category>> AddCategory(CategoryModel category, int storeId)
        {
            var addedCategory = await _categoryService.AddCategory(category, storeId);
            return Ok(addedCategory);
        }

        [HttpGet("category-by-store/{id}")]
        public async Task<ActionResult<List<Category>>> GetCategoriesByStore(int storeId)
        {
            var categories = await _categoryService.GetCategoriesByStore( storeId);
            return Ok(categories);
        }
    }
}
