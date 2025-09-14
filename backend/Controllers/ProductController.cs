using backend.Services.CloudinaryServices;
using backend.Services.ProductServices;
using backend.Services.StoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        public ProductController(IProductService productService, IStoreService storeService, ICloudinaryService cloudinaryService)
        {
            _productService = productService;
            _storeService = storeService;
            _cloudinaryService = cloudinaryService;
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
        public async Task<ActionResult<ProductDetail>> AddProductDetails(ProductDetailModel productDetail, [FromHeader]int productId, [FromHeader] int storeId, [FromHeader] int categoryId)
        {

            var store = await _storeService.GetStoreByStoreId(storeId);
            if(store is null)
                return NotFound("Store not found");
            var storeAFM = store.AFM;
            //if(productDetail.ProductImage != null)
            //{
            //    var productImage = await _cloudinaryService.UploadProductPhoto(productDetail.ProductImage, storeAFM, productId);

            //}
            productDetail.ImageUrl = productDetail.ProductImage != null ? await _cloudinaryService.UploadProductPhoto(productDetail.ProductImage, storeAFM, productId) : null;
            var addedProductDetails = await _productService.AddProductDetails(productDetail, productId, storeId, categoryId);

            return Ok(addedProductDetails);
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
        public async Task<ActionResult<ProductDetail>> UpdateProductDetails(int id, ProductDetailModel productDetail)
        {
            var updatedProductDetails = await _productService.UpdateProductDetails(id, productDetail);
            if (updatedProductDetails is null)
                return NotFound("Sorryy");
            return Ok(updatedProductDetails);
        }

    }
}
