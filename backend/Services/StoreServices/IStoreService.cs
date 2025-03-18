namespace backend.Services.StoreServices
{
    public interface IStoreService
    {
        Task<List<Store>> GetAllStores();

        Task<Store> GetStore(int id, Guid userId);

        Task<Store> AddStore(StoreModel store, Guid userId);

        Task<Store> UpdateStore(int id, StoreModel request);
    }
}
