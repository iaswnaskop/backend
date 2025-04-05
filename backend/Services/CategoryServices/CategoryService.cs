using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return null;
            return category;
        }

        public async Task<Category> AddCategory(CategoryModel category, int storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
                return null;

            var newCategory = new Category
            {
                Name = category.Name,
                Description = category.Description,
                Stores = new List<Store> { store }
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<List<Category>> GetCategoriesByStore(int storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
                return null;
            var categories = await _context.Categories
                .Include(c => c.Stores)
                .Where(c => c.Stores.Contains(store))
                .ToListAsync();
            return categories;
        }
    }
}
