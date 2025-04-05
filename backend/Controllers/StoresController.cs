using backend.Services.StoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all-stores")]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {

            var stores = await _storeService.GetAllStores();
            return Ok(stores);
        }
        
        [RolePermission("View")]
        [HttpGet]
        [Route("store{id}")]
        public async Task<ActionResult<Store>> GetStore(int id, Guid userId)
        {
            var store = await _storeService.GetStore(id, userId);
            if (store is null)
                return NotFound("Sorryy");
            return Ok(store);
        }

        
        [HttpPost("add-store")]
        public async Task<ActionResult<Store>> AddStore(StoreModel store, Guid userId)
        {

            var addedStore = await _storeService.AddStore(store, userId);
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

        [HttpDelete("delete-store{id}")]
        public async Task<ActionResult<Store>> DeleteStore(int id)
        {
            var store = await _storeService.GetStore(id, Guid.Empty);
            if (store is null)
                return NotFound("Sorryy");
            
            return Ok("Store Removed");
        }
    }
}
