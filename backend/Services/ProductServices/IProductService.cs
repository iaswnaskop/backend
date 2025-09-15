namespace backend.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(ProductModel product);
        Task<ProductDetail> AddProductDetails(ProductDetailModel productDetail, int productId, int storeId, int categoryId);
        Task<ProductDetail> GetProductDetails(int id);
        Task<Product> UpdateProduct(int id, ProductModel product);
        Task<ProductDetail> UpdateProductDetails(int id, ProductDetailModel productDetail);
        Task<bool> DeleteProduct(int id);
        Task<bool> DeleteProductDetails(int id);
    }
}
