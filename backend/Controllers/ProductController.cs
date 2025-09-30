using backend.Services.CloudinaryServices;
using backend.Services.ProductServices;
using backend.Services.StoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<StoresController> _logger;

        public ProductController(IProductService productService, IStoreService storeService, ICloudinaryService cloudinaryService, ILogger<StoresController> logger)
        {
            _productService = productService;
            _storeService = storeService;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }
        [HttpGet("products")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);

        }
        [HttpGet("product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product is null)
                return NotFound("Sorryy");
            return Ok(product);
        }
        [HttpPost("add-product")]
        public async Task<ActionResult<Product>> AddProduct(ProductModel product)
        {
            var addedProduct = await _productService.AddProduct(product);
            return Ok(addedProduct);
        }

        [HttpPost("add-product-details")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductDetailModel>> AddProductDetails(AddProductDeatilForm productDetail, [FromHeader]int productId, [FromHeader] int storeId, [FromHeader] int categoryId)
        {
            try {
                var model = JsonSerializer.Deserialize<AddProductDetailModel>(productDetail.JsonData);
                var store = await _storeService.GetStoreByStoreId(storeId);
                if (store is null)
                    return NotFound("Store not found");
                var storeAFM = store.AFM;
                //if(productDetail.ProductImage != null)
                //{
                //    var productImage = await _cloudinaryService.UploadProductPhoto(productDetail.ProductImage, storeAFM, productId);

                //}
                model.ImageUrl = productDetail.ProductImage != null ? await _cloudinaryService.UploadProductPhoto(productDetail.ProductImage, storeAFM, productId) : null;

                var addedProductDetails = await _productService.AddProductDetails(model, productId, storeId, categoryId);

                var response = new ProductDetailModel
                {
                    Id = addedProductDetails.Id,
                    Name = addedProductDetails.Name,
                    Description = addedProductDetails.Description,
                    Price = addedProductDetails.Price,
                    NameEng = addedProductDetails.NameEng,
                    DescriptionEng = addedProductDetails.DescriptionEng,
                    NameDu = addedProductDetails.NameDu,
                    DescriptionDu = addedProductDetails.DescriptionDu,
                    NameFr = addedProductDetails.NameFr,
                    DescriptionFr = addedProductDetails.DescriptionFr,
                    NameIt = addedProductDetails.NameIt,
                    DescriptionIt = addedProductDetails.DescriptionIt,
                    NameEs = addedProductDetails.NameEs,
                    DescriptionEs = addedProductDetails.DescriptionEs,
                    Available = addedProductDetails.Available,
                    StoreId = addedProductDetails.StoreId,
                    ProductId = addedProductDetails.ProductId,
                    CategoryId = addedProductDetails.CategoryId,
                    IsHidden = addedProductDetails.IsHidden,
                    IsVegan = addedProductDetails.IsVegan,
                    GlutenFree = addedProductDetails.GlutenFree,
                    IsKosher = addedProductDetails.IsKosher,
                    IsSpicy = addedProductDetails.IsSpicy,
                    ContainsNuts = addedProductDetails.ContainsNuts,
                    SuggestedProduct = addedProductDetails.SuggestedProducts != null ? new List<int> { addedProductDetails.SuggestedProducts.SuggestedProductDetailId } : new List<int>(),
                    ImageUrl = addedProductDetails.ImageUrl

                };

                return Ok(response);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error while Add Product Details with ProductId {ProductId}", productId);
                return BadRequest(new { error = "JSON parse error", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while Add Product Details with ProductId {ProductId}", productId);
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }

            
        }

        [HttpGet("product-details/{id}")]
        public async Task<ActionResult<ProductDetailModel>> GetProductDetails(int id)
        {
            var productDetails = await _productService.GetProductDetails(id);
            if (productDetails is null)
                return NotFound("Product Details Not Found");

            return Ok(productDetails);
        }
        [HttpPut("update-product/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductModel product)
        {
            var updatedProduct = await _productService.UpdateProduct(id, product);
            if (updatedProduct is null)
                return NotFound("Sorryy");
            return Ok(updatedProduct);
        }
        [HttpPut("update-product-details/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductDetailModel>> UpdateProductDetails(int id, AddProductDeatilForm productDetail, [FromHeader]int storeId)
        {
            try {
                var model = JsonSerializer.Deserialize<AddProductDetailModel>(productDetail.JsonData);
                var store = await _storeService.GetStoreByStoreId(storeId);
                if (store is null)
                    return NotFound("Store not found");
                model.ImageUrl = productDetail.ProductImage != null ? await _cloudinaryService.UploadProductPhoto(productDetail.ProductImage, store.AFM, id) : null;
                var updatedProductDetails = await _productService.UpdateProductDetails(id, model);
                if (updatedProductDetails is null)
                    return NotFound("Product Details Not Found");
                var response = new ProductDetailModel
                {
                    Id = updatedProductDetails.Id,
                    Name = updatedProductDetails.Name,
                    Description = updatedProductDetails.Description,
                    Price = updatedProductDetails.Price,
                    NameEng = updatedProductDetails.NameEng,
                    DescriptionEng = updatedProductDetails.DescriptionEng,
                    NameDu = updatedProductDetails.NameDu,
                    DescriptionDu = updatedProductDetails.DescriptionDu,
                    NameFr = updatedProductDetails.NameFr,
                    DescriptionFr = updatedProductDetails.DescriptionFr,
                    NameIt = updatedProductDetails.NameIt,
                    DescriptionIt = updatedProductDetails.DescriptionIt,
                    NameEs = updatedProductDetails.NameEs,
                    DescriptionEs = updatedProductDetails.DescriptionEs,
                    Available = updatedProductDetails.Available,
                    StoreId = updatedProductDetails.StoreId,
                    ProductId = updatedProductDetails.ProductId,
                    CategoryId = updatedProductDetails.CategoryId,
                    IsHidden = updatedProductDetails.IsHidden,
                    IsVegan = updatedProductDetails.IsVegan,
                    GlutenFree = updatedProductDetails.GlutenFree,
                    IsKosher = updatedProductDetails.IsKosher,
                    IsSpicy = updatedProductDetails.IsSpicy,
                    ContainsNuts = updatedProductDetails.ContainsNuts,
                    SuggestedProduct = updatedProductDetails.SuggestedProducts != null ? new List<int> { updatedProductDetails.SuggestedProducts.SuggestedProductDetailId } : new List<int>(),
                    ImageUrl = updatedProductDetails.ImageUrl
                };

                return Ok(response);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error while Update Product Details with ProductDetailId {ProductDetailId}", id);
                return BadRequest(new { error = "JSON parse error", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while Update Product Details with ProductDetailId {ProductDetailId}", id);
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }
            
        }
        [HttpDelete("delete-product/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product is null)
                return NotFound("Product not found");
            
            var result = await _productService.DeleteProduct(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the product");
            return NoContent();
        }

        [HttpDelete("delete-product-details/{id}")]
        public async Task<ActionResult> DeleteProductDetails(int id)
        {
            var productDetails = await _productService.GetProductDetails(id);
            if (productDetails is null)
                return NotFound("Product Details not found");
            var result = await _productService.DeleteProductDetails(id);
            if (!result)
                return StatusCode(500, "An error occurred while deleting the product details");
            return NoContent();
        }

    }
}
