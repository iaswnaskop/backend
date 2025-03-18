
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace backend.Services.StoreServices
{

    public class StoreService : IStoreService
    {

        private readonly DataContext _context;
        

        public StoreService(DataContext context)
        {
            _context = context;
        }
        public async Task<Store> AddStore(StoreModel store, Guid userId)
        {

            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                return null;
            var newStore = new Store
            {
                Name = store.Name,
                Address = store.Address,
                Description = store.Description,
                Phone = store.Phone,
                Users = new List<User> { user }
            };

            

            _context.Stores.Add(newStore);
            await _context.SaveChangesAsync();

            return newStore;

        }

        public async Task<List<Store>> GetAllStores()
        {
            var stores = await _context.Stores.ToListAsync();
            return stores;
        }

        public async Task<Store> GetStore(int id, Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                return null;

            var store = await _context.Stores
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.Id == id && s.Users.Any(u => u.Id == userId));
            if (store is null)
                return null;
            return store;
        }

        public async Task<Store> UpdateStore(int id, StoreModel request)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store is null)
                return null;

            if (request.Name != null && request.Name != store.Name)
            {
                store.Name = request.Name;
            }

            if (request.Address != null && request.Address != store.Address)
            {
                store.Address = request.Address;
            }

            if (request.Description != null && request.Description != store.Description)
            {
                store.Description = request.Description;
            }

            if (request.Phone != null && request.Phone != store.Phone)
            {
                store.Phone = request.Phone;
            }

            await _context.SaveChangesAsync();

            return store;
        }
    }
}
