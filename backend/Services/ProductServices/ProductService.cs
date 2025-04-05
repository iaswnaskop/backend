using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            return product;
        }

        public async Task<Product> AddProduct(ProductModel product)
        {
            var newProduct = new Product
            {
                Name = product.Name
                
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<ProductDetail> AddProductDetails(ProductDetailModel productDetail, int productId, int storeId, int categoryId)
        {
            var newProductDetail = new ProductDetail
            {
                ProductId = productId,
                Description = productDetail.Description,
                Price = productDetail.Price,
                Name = productDetail.Name,
                Available = productDetail.Available,
                StoreId = storeId,
                CategoryId = categoryId
            };
            _context.ProductDetails.Add(newProductDetail);
            await _context.SaveChangesAsync();
            return newProductDetail;
        }
        public async Task<ProductDetail> GetProductDetails(int id)
        {
            var productDetails = await _context.ProductDetails.FindAsync(id);
            if (productDetails == null)
                return null;

            
            var productDetail = await _context.ProductDetails
                .Include(p => p.Store)
                .Include(p => p.Category)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
            if(productDetail == null)
                return null;

            return productDetail;
        }

        public async Task<Product> UpdateProduct(int id, ProductModel product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return null;
            existingProduct.Name = product.Name;
            // Update other properties as needed
            await _context.SaveChangesAsync();
            return existingProduct;
        }
        public async Task<ProductDetail> UpdateProductDetails(int id, ProductDetailModel productDetail)
        {
            var existingProductDetail = await _context.ProductDetails.FindAsync(id);
            if (existingProductDetail == null)
                return null;
            existingProductDetail.Description = productDetail.Description;
            existingProductDetail.Price = productDetail.Price;
            existingProductDetail.Name = productDetail.Name;
            existingProductDetail.Available = productDetail.Available;
            // Update other properties as needed
            await _context.SaveChangesAsync();
            return existingProductDetail;
        }
    }
}
