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
        public async Task<CategoryModel> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            var response = new CategoryModel
            {
                Name = category.Name,
                Description = category.Description,
                NameEng = category.NameEng,
                DescriptionEng = category.DescriptionEng,
                NameDu = category.NameDu,
                DescriptionDu = category.DescriptionDu,
                NameFr = category.NameFr,
                DescriptionFr = category.DescriptionFr,
                NameIt = category.NameIt,
                DescriptionIt = category.DescriptionIt,
                NameEs = category.NameEs,
                DescriptionEs = category.DescriptionEs
            };

            if (category == null)
                return null;
            return response;
        }

        public async Task<CategoryModel> AddCategory(CategoryModel category, int storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
                return null;

            var newCategory = new Category
            {
                Name = category.Name,
                Description = category.Description,
                NameEng = category.NameEng,
                DescriptionEng = category.DescriptionEng,
                NameDu = category.NameDu,
                DescriptionDu = category.DescriptionDu,
                NameFr = category.NameFr,
                DescriptionFr = category.DescriptionFr,
                NameIt = category.NameIt,
                DescriptionIt = category.DescriptionIt,
                NameEs = category.NameEs,
                DescriptionEs = category.DescriptionEs,
                Stores = new List<Store> { store }
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            var response = new CategoryModel
            {
                Name = newCategory.Name,
                Description = newCategory.Description,
                NameEng = newCategory.NameEng,
                DescriptionEng = newCategory.DescriptionEng,
                NameDu = newCategory.NameDu,
                DescriptionDu = newCategory.DescriptionDu,
                NameFr = newCategory.NameFr,
                DescriptionFr = newCategory.DescriptionFr,
                NameIt = newCategory.NameIt,
                DescriptionIt = newCategory.DescriptionIt,
                NameEs = newCategory.NameEs,
                DescriptionEs = newCategory.DescriptionEs,
                StoreId = storeId
            };
            return response;
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

        public async Task<Category> UpdateCategory(int id, CategoryModel category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return null;

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.NameEng = category.NameEng;
            existingCategory.DescriptionEng = category.DescriptionEng;
            existingCategory.NameDu = category.NameDu;
            existingCategory.DescriptionDu = category.DescriptionDu;
            existingCategory.NameFr = category.NameFr;
            existingCategory.DescriptionFr = category.DescriptionFr;
            existingCategory.NameIt = category.NameIt;
            existingCategory.DescriptionIt = category.DescriptionIt;
            existingCategory.NameEs = category.NameEs;
            existingCategory.DescriptionEs = category.DescriptionEs;

            _context.Categories.Update(existingCategory);

            await _context.SaveChangesAsync();
            return existingCategory;
        }
        
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
