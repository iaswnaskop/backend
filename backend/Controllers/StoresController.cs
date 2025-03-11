using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private static List<Store> stores = new List<Store>
        {
            new Store
            {
                 Id = 1,
                Name = "Store 1" ,
                Address="test",
                Description="test"
            },

             new Store
            {
                 Id = 2,
                Name = "Store 2" ,
                Address="test",
                Description="test"
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {
           
            return Ok(stores);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = stores.Find(x => x.Id == id);
            if (store is null)
                return NotFound("Sorryy");
            return Ok(store);
        }


        [HttpPost]
        public async Task<ActionResult<List<Store>>> AddStore(Store store)
        {
            stores.Add(store);
            return Ok(stores);
        }
    }
}
