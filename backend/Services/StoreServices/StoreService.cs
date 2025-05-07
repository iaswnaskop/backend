
using backend.Data;
using backend.Entities;
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
        public async Task<Store> AddStore(AddStoreModel store, Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var storeTypes = await _context.Types
                .Where(t => store.TypeId.Any(st => st.Equals(t.Id)))
                .ToListAsync();

            var storeLanguages = await _context.Languages
                .Where(t => store.LanguageId.Any(st => st.Equals(t.Id)))
                .ToListAsync();

            if (user is null)
                return null;

            var newStore = new Store
            {
                Name = store.Name,
                Address = store.Address,
                Description = store.Description,
                Phone = store.Phone,
                Users = new List<User> { user },
                AFM = store.AFM,
                ImageURL = store.ImageURL,
                Email = store.Email,
                Facebook = store.Facebook,
                Instagram = store.Instagram,
                TikTok = store.TikTok,
                TripAdvisor = store.TripAdvisor,
                GoogleBusiness = store.GoogleBusiness,
                Design = new Design
                {
                    BgColor = store.DesignColor,
                    Font = store.DesignFont,
                    BgURL = store.DesignBackgroundURL,
                    DesignModelId = store.DesignModelId
                }
                

            };

            _context.Stores.Add(newStore);
            await _context.SaveChangesAsync();
            // Εδώ προσθέτουμε τους StoreTypes
            newStore.StoreTypes =  storeTypes.Select(type => new StoreType
            {
                StoreId = newStore.Id,
                TypeId = type.Id
            }).ToList();

            newStore.StoreLanguages = storeLanguages.Select(language => new StoreLanguage
            {
                StoreId = newStore.Id,
                LanguageId = language.Id
            }).ToList();

            //var newDesign = new Design
            //{
            //    BgColor = store.DesignColor,
            //    Font = store.DesignFont,
            //    BgURL = store.DesignBackgroundURL,
            //    DesignModelId = store.DesignModelId
            //};
            //_context.Design.Add(newDesign);

            //await _context.SaveChangesAsync();
            //newStore.DesignId = newDesign.Id;
            _context.Stores.Update(newStore);
            user.HasPassOnBoarding = true;
            _context.Users.Update(user);
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
            var store = await _context.Stores
                .Include(s => s.StoreTypes)
                .FirstOrDefaultAsync(s => s.Id == id);

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
            if (request.AFM != null && request.AFM != store.AFM)
            {
                store.AFM = request.AFM;
            }
            if (request.ImageURL != null && request.ImageURL != store.ImageURL)
            {
                store.ImageURL = request.ImageURL;
            }
            if(request.Email != null && request.Email != store.Email)
            {
                store.Email = request.Email;
            }
            if (request.Facebook != null && request.Facebook != store.Facebook)
            {
                store.Facebook = request.Facebook;
            }
            if (request.Instagram != null && request.Instagram != store.Instagram)
            {
                store.Instagram = request.Instagram;
            }
            if (request.TikTok != null && request.TikTok != store.TikTok)
            {
                store.TikTok = request.TikTok;
            }
            if (request.TripAdvisor != null && request.TripAdvisor != store.TripAdvisor)
            {
                store.TripAdvisor = request.TripAdvisor;
            }
            if (request.GoogleBusiness != null && request.GoogleBusiness != store.GoogleBusiness)
            {
                store.GoogleBusiness = request.GoogleBusiness;
            }


            // Ενημέρωση των StoreTypes
            if (request.TypeModel != null)
            {
                // Διαγραφή των παλιών StoreTypes
                _context.StoreTypes.RemoveRange(store.StoreTypes);

                // Προσθήκη των νέων StoreTypes
                var storeTypes = await _context.Types
                    .Where(t => request.TypeModel.Any(st => st.Id == t.Id))
                    .ToListAsync();

                store.StoreTypes = storeTypes.Select(type => new StoreType
                {
                    StoreId = store.Id,
                    TypeId = type.Id
                }).ToList();
            }
            

            await _context.SaveChangesAsync();

            return store;
        }
        public async Task<bool> DeleteStore(int id, Guid userId)
        {
            var store = await _context.Stores
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.Id == id && s.Users.Any(u => u.Id == userId));
            if (store is null)
                return false;
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<TypeModel>> GetStoreTypes()
        {
            var storeTypes = await _context.Types.ToListAsync();
            var typeModels = storeTypes.Select(type => new TypeModel
            {
                Id = type.Id,
                Name = type.Name
            }).ToList();

            return typeModels;
        }
        public async Task<List<LanguageModel>> GetStoreLanguages()
        {
            var storeLanguages = await _context.Languages.ToListAsync();
            var languageModels = storeLanguages.Select(language => new LanguageModel
            {
                Id = language.Id,
                CountryLang = language.CountryLang,
                Abbr = language.Abbr
            }).ToList();

            return languageModels;
        }

        public async Task<List<DesignModel>> GetStoreDesignModels()
        {
            var storeDesignModels = await _context.DesignModel.ToListAsync();
            var designModels = storeDesignModels.Select(design => new DesignModel
            {
                Id = design.Id,
                Model = design.Model
            }).ToList();
            return designModels;
        }
    }
}
