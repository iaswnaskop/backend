namespace backend.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(CategoryModel category, int storeId);
        Task<List<Category>> GetCategoriesByStore(int storeId);

    }
}
