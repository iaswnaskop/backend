namespace backend.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<CategoryModel> GetCategoryById(int id);
        Task<CategoryModel> AddCategory(CategoryModel category, int storeId);
        Task<List<Category>> GetCategoriesByStore(int storeId);
        Task<Category> UpdateCategory(int id, CategoryModel category);
        Task<bool> DeleteCategory(int id);


    }
}
