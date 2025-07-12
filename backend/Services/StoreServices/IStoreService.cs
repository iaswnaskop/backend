namespace backend.Services.StoreServices
{
    public interface IStoreService
    {
        Task<List<Store>> GetAllStores();

        Task<Store> GetStore(int id, Guid userId);
        Task<StoreModel> GetStoreByStoreId(int id);

        Task<Store> AddStore(AddStoreModel store, Guid userId);

        Task<Store> UpdateStore(int id, StoreModel request);
        Task<bool> DeleteStore(int id, Guid userId);
        Task<List<TypeModel>> GetStoreTypes();
        Task<List<LanguageModel>> GetStoreLanguages();
        Task <List<DesignModel>> GetStoreDesignModels();
        //Task<Store> UpdateStorePhotos(UpdateStorePhotos store, Guid userId);
    }
}
