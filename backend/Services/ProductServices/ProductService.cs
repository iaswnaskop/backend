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
                Name = product.Name,
                NameEng = product.NameEng,
                NameDu = product.NameDu,
                NameFr = product.NameFr,
                NameIt = product.NameIt,
                NameEs = product.NameEs


            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<ProductDetail> AddProductDetails(AddProductDetailModel productDetail, int productId, int storeId, int categoryId)
        {
            var newProductDetail = new ProductDetail
            {
                ProductId = productId,
                Description = productDetail.Description,
                DescriptionEng = productDetail.DescriptionEng,
                DescriptionDu = productDetail.DescriptionDu,
                DescriptionFr = productDetail.DescriptionFr,
                DescriptionIt = productDetail.DescriptionIt,
                DescriptionEs = productDetail.DescriptionEs,
                Price = productDetail.Price,
                Name = productDetail.Name,
                NameEng = productDetail.NameEng,
                NameDu = productDetail.NameDu,
                NameFr = productDetail.NameFr,
                NameIt = productDetail.NameIt,
                NameEs = productDetail.NameEs,
                IsHidden = productDetail.IsHidden,
                IsVegan = productDetail.IsVegan,
                GlutenFree = productDetail.GlutenFree,
                IsKosher = productDetail.IsKosher,
                IsSpicy = productDetail.IsSpicy,
                ContainsNuts = productDetail.ContainsNuts,
                Available = true,
                StoreId = storeId,
                CategoryId = categoryId,
                ImageUrl = productDetail.ImageUrl

            };
            
            _context.ProductDetails.Add(newProductDetail);
            await _context.SaveChangesAsync();
            foreach (var suggestedId in productDetail.SuggestedProduct)
            {
                var suggestProduct = new SuggestProduct
                {
                    ProductDetailId = newProductDetail.Id,
                    SuggestedProductDetailId = suggestedId,
                    
                };
                _context.SuggestProducts.Add(suggestProduct);
            }
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
                .Include(p => p.SuggestedProducts)
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
        public async Task<ProductDetail> UpdateProductDetails(int id, AddProductDetailModel productDetail)
        {
            var existingProductDetail = await _context.ProductDetails.FindAsync(id);
            if (existingProductDetail == null)
                return null;

            if (!string.Equals(existingProductDetail.Name, productDetail.Name, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.Name))
            {
                existingProductDetail.Name = productDetail.Name;
            }
            if (!string.Equals(existingProductDetail.NameEng, productDetail.NameEng, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.NameEng))
            {
                existingProductDetail.NameEng = productDetail.NameEng;
            }
            if (!string.Equals(existingProductDetail.NameDu, productDetail.NameDu, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.NameDu))
            {
                existingProductDetail.NameDu = productDetail.NameDu;
            }
            if (!string.Equals(existingProductDetail.NameFr, productDetail.NameFr, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.NameFr))
            {
                existingProductDetail.NameFr = productDetail.NameFr;
            }
            if (!string.Equals(existingProductDetail.NameIt, productDetail.NameIt, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.NameIt))
            {
                existingProductDetail.NameIt = productDetail.NameIt;
            }
            if (!string.Equals(existingProductDetail.NameEs, productDetail.NameEs, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.NameEs))
            {
                existingProductDetail.NameEs = productDetail.NameEs;
            }

            if (existingProductDetail.Price != productDetail.Price)
            {
                existingProductDetail.Price = productDetail.Price;
            }

            if (!string.Equals(existingProductDetail.Description, productDetail.Description, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.Description))
            {
                existingProductDetail.Description = productDetail.Description;
            }
            if (!string.Equals(existingProductDetail.DescriptionEng, productDetail.DescriptionEng, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.DescriptionEng))
            {
                existingProductDetail.DescriptionEng = productDetail.DescriptionEng;
            }
            if (!string.Equals(existingProductDetail.DescriptionDu, productDetail.DescriptionDu, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.DescriptionDu))
            {
                existingProductDetail.DescriptionDu = productDetail.DescriptionDu;
            }
            if (!string.Equals(existingProductDetail.DescriptionFr, productDetail.DescriptionFr, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.DescriptionFr))
            {
                existingProductDetail.DescriptionFr = productDetail.DescriptionFr;
            }
            if (!string.Equals(existingProductDetail.DescriptionIt, productDetail.DescriptionIt, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.DescriptionIt))
            {
                existingProductDetail.DescriptionIt = productDetail.DescriptionIt;
            }
            if (!string.Equals(existingProductDetail.DescriptionEs, productDetail.DescriptionEs, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.DescriptionEs))
            {
                existingProductDetail.DescriptionEs = productDetail.DescriptionEs;
            }

            if (existingProductDetail.Available != productDetail.Available)
            {
                existingProductDetail.Available = productDetail.Available;
            }
            if (existingProductDetail.IsHidden != productDetail.IsHidden)
            {
                existingProductDetail.IsHidden = productDetail.IsHidden;
            }
            if (existingProductDetail.IsVegan != productDetail.IsVegan)
            {
                existingProductDetail.IsVegan = productDetail.IsVegan;
            }
            if (existingProductDetail.GlutenFree != productDetail.GlutenFree)
            {
                existingProductDetail.GlutenFree = productDetail.GlutenFree;
            }
            if (existingProductDetail.IsKosher != productDetail.IsKosher)
            {
                existingProductDetail.IsKosher = productDetail.IsKosher;
            }
            if (existingProductDetail.IsSpicy != productDetail.IsSpicy)
            {
                existingProductDetail.IsSpicy = productDetail.IsSpicy;
            }
            if (existingProductDetail.ContainsNuts != productDetail.ContainsNuts)
            {
                existingProductDetail.ContainsNuts = productDetail.ContainsNuts;
            }
            if (!string.Equals(existingProductDetail.ImageUrl, productDetail.ImageUrl, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(productDetail.ImageUrl))
            {
                existingProductDetail.ImageUrl = productDetail.ImageUrl;
            }


            if (existingProductDetail.SuggestedProducts != null)
            {
                    _context.SuggestProducts.RemoveRange(_context.SuggestProducts.Where(sp => sp.ProductDetailId == existingProductDetail.Id));
                    foreach (var suggestedId in productDetail.SuggestedProduct)
                    {
                        var suggestProduct = new SuggestProduct
                        {
                            ProductDetailId = existingProductDetail.Id,
                            SuggestedProductDetailId = suggestedId,
                        };
                        _context.SuggestProducts.Add(suggestProduct);
                    }
            }
                
            


            // Update other properties as needed
            await _context.SaveChangesAsync();
            return existingProductDetail;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductDetails(int id)
        {
            var productDetails = await _context.ProductDetails.FindAsync(id);
            if (productDetails == null)
                return false;
            _context.ProductDetails.Remove(productDetails);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
