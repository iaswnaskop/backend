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
        public async Task<ActionResult<CategoryModel>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category is null)
                return NotFound("Sorryy");
            return Ok(category);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<CategoryModel>> AddCategory(CategoryModel category, [FromHeader] int storeId)
        {
            var addedCategory = await _categoryService.AddCategory(category, storeId);
            return Ok(addedCategory);
        }

        [HttpGet("category-by-store")]
        public async Task<ActionResult<List<Category>>> GetCategoriesByStore(int storeId)
        {
            var categories = await _categoryService.GetCategoriesByStore(storeId);
            return Ok(categories);
        }

        [HttpPut("update-category/{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, CategoryModel category)
        {
            var updatedCategory = await _categoryService.UpdateCategory(id, category);
            if (updatedCategory is null)
                return NotFound("Category not found");

            return Ok(updatedCategory);
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound("Category not found");
            
            var deletedCategory = await _categoryService.DeleteCategory(id);
            return Ok(deletedCategory);
        }
    }
}
