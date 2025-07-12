using backend.Services.StoreServices;
using backend.Services.CloudinaryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services.AuthServices;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IAuthService _authService;
        public StoresController(IStoreService storeService, ICloudinaryService cloudinaryService, IAuthService authService)
        {
            _storeService = storeService;
            _cloudinaryService = cloudinaryService;
            _authService = authService;
        }



       
        [HttpGet("all-stores")]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {

            var stores = await _storeService.GetAllStores();
            return Ok(stores);
        }
        
        [HttpGet("store/{id}")]
        
        public async Task<ActionResult<Store>> GetStore(int id, [FromHeader]Guid userId)
        {
            var store = await _storeService.GetStore(id, userId);
            if (store is null)
                return NotFound("Sorryy");
            return Ok(store);
        }

        
        [HttpPost("add-store")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Store>> AddStore([FromForm] AddStoreModel store)
        {
            if (store is null)
                return BadRequest("Invalid store data or user ID");
            var urls = new List<string>();
            if (store.Logo is null || (store.DesignBackgroundURL is null && store.DesignColor is null))
                return BadRequest("Please upload images or Backround color");

            

            if (store.DesignColor is not null)
            {
                 var files = new List<IFormFile> { store.Logo, store.BgPhoto };
                 urls = await _cloudinaryService.UploadPhotosAsync(files, store.AFM);
            }
            else
            {
                var files = new List<IFormFile> { store.Logo };
                urls = await _cloudinaryService.UploadPhotosAsync(files, store.AFM);
            }

            store.ImageURL = urls.ElementAtOrDefault(0);
            if (store.DesignColor is not null)
            {
                store.DesignBackgroundURL = urls.ElementAtOrDefault(1);

            }

            //if (store.ImageURL is null || (store.DesignBackgroundURL is null && store.DesignColor is null))
            //    return BadRequest("Error uploading images or Backround color");
            var user = await _authService.GetUserAsync(User);
            if (user is null)
                return NotFound("User not found.");

            var addedStore = await _storeService.AddStore(store, user.Id);
            return Ok(addedStore);
        }

        
        [HttpPut("update-store{id}")]
        public async Task<ActionResult<Store>> UpdateStore(int id, StoreModel request)
        {
            var result = await _storeService.UpdateStore(id, request);
            if (result is null)
                return NotFound("Sorryy");

            return Ok(result);

        }
        //[HttpPut("update-store-photos")]
        //public async Task<ActionResult<Store>> UpdateStorePhotos([FromForm] UpdateStorePhotos store, [FromForm] Guid userId)
        //{
        //    var
        //    var result = await _storeService.UpdateStorePhotos(store, userId);
        //    if (result is null)
        //        return NotFound("Sorryy");

        //    return Ok(result);
        //}

        [HttpDelete("delete-store{id}")]
        public async Task<ActionResult<Store>> DeleteStore(int id)
        {
            var store = await _storeService.GetStore(id, Guid.Empty);
            if (store is null)
                return NotFound("Sorryy");
            
            return Ok("Store Removed");
        }

        [HttpGet("store-types")]
        public async Task<ActionResult<List<TypeModel>>> GetStoreTypes()
        {
            var storeTypes = await _storeService.GetStoreTypes();
            return Ok(storeTypes);
        }
        [HttpGet("store-languages")]
        public async Task<ActionResult<List<LanguageModel>>> GetStoreLanguages()
        {
            var storeLanguages = await _storeService.GetStoreLanguages();
            return Ok(storeLanguages);
        }

        [HttpGet("store-design-models")]
        public async Task<ActionResult<List<DesignModel>>> GetStoreDesignModels()
        {
            var storeDesignModels = await _storeService.GetStoreDesignModels();
            return Ok(storeDesignModels);
        }
    }
}
