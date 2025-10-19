
using backend.Data;
using backend.Entities;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
                //Design = new Design
                //{
                //    BgColor = store.DesignColor,
                //    Font = store.DesignFont,
                //    BgURL = store.DesignBackgroundURL,
                //    DesignModelId = store.DesignModelId
                //}
                

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

            var newDesign = new Design
            {
                BgColor = store.DesignColor,
                Font = store.DesignFont,
                BgURL = store.DesignBackgroundURL,
                DesignModelId = store.DesignModelId
            };
            _context.Design.Add(newDesign);

            await _context.SaveChangesAsync();
            newStore.DesignId = newDesign.Id;
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
                .Include(s => s.ProductDetails)
                    .ThenInclude(s => s.SuggestedProducts)
                .Include(s => s.Categories)
                .Include(s => s.StoreTypes)
                    .ThenInclude(st => st.Type)
                .Include(s => s.StoreLanguages)
                    .ThenInclude(sl => sl.Language)
                .Include(s => s.Schedules)
                .FirstOrDefaultAsync(s => s.Id == id && s.Users.Any(u => u.Id == userId));

            store.Design = await _context.Design.Where(d => d.Id == store.DesignId)
                .Include(d => d.DesignModel)
                .ToListAsync();

            
            
                

            if (store == null)
                return null;

            return store;
        }

        public async Task<Store> UpdateStore(int id, UpdateStoreModel request)
        {
            var store = await _context.Stores
                .Include(s => s.StoreLanguages)
                .Include(s => s.Schedules)
                .FirstOrDefaultAsync(s => s.Id == id);

            var storeLanguages = await _context.StoreLanguages
                .Where(t => t.StoreId == store.Id)
                .ToListAsync();
            var languages = await _context.Languages
                .Where(t => request.Languages.Any(st => st.Equals(t.Id)))
                .ToListAsync();

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
            if (request.PurchasingManager != null && request.PurchasingManager != store.PurchasingManager)
            {
                store.PurchasingManager = request.PurchasingManager;
            }
            if (request.WifiName != null && request.WifiName != store.WifiName)
            {
                store.WifiName = request.WifiName;
            }
            if (request.WifiPassword != null && request.WifiPassword != store.WifiPassword)
            {
                store.WifiPassword = request.WifiPassword;
            }
            if (request.StorePhone != null && request.StorePhone != store.StorePhone)
            {
                store.StorePhone = request.StorePhone;
            }
            //if (request.QRCodeURL != null && request.QRCodeURL != store.QRCodeURL)
            //{
            //    store.QRCodeURL = request.QRCodeURL;
            //}
            if (request.Cash.HasValue && request.Cash != store.Cash)
            {
                store.Cash = request.Cash.Value;
            }
            if (request.Card.HasValue && request.Card != store.Card)
            {
                store.Card = request.Card.Value;
            }
            if (request.PayPal.HasValue && request.PayPal != store.PayPal)
            {
                store.PayPal = request.PayPal.Value;
            }
            if (request.BitCoin.HasValue && request.BitCoin != store.BitCoin)
            {
                store.BitCoin = request.BitCoin.Value;
            }
            if (request.IRIS.HasValue && request.IRIS != store.IRIS)
            {
                store.IRIS = request.IRIS.Value;
            }
            if (request.IsActive.HasValue && request.IsActive != store.IsActive)
            {
                store.IsActive = request.IsActive.Value;
            }
            if (request.Languages != null && request.Languages.Count > 0)
            {
                
                if (storeLanguages != null)
                    _context.StoreLanguages.RemoveRange(storeLanguages);
                var newLanguage = languages.Select(t => new StoreLanguage
                {
                    StoreId = store.Id,
                    LanguageId = t.Id
                }).ToList();
                await _context.StoreLanguages.AddRangeAsync(newLanguage);
            }
            



            if (request.ScheduleModel != null)
            {
                var existingSchedules = _context.Schedules.Where(s => s.StoreId == store.Id);
                if(existingSchedules != null)
                    _context.Schedules.RemoveRange(existingSchedules);
                var newSchedules = request.ScheduleModel.Select(s => new Schedule
                {
                    StoreId = store.Id,
                    DayOfWeek = (DayOfWeek)s.DayOfWeek,
                    OpeningTime = TimeSpan.Parse(s.OpeningTime),
                    ClosingTime = TimeSpan.Parse(s.ClosingTime),
                    IsClosed = s.IsClosed
                }).ToList();
                await _context.Schedules.AddRangeAsync(newSchedules);
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

        public async Task<StoreModel> GetStoreByStoreId(int id)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.Id == id);
            if (store is null)
                return null;

            var storeModel = new StoreModel
            {
                Id = store.Id,
                Name = store.Name,
                AFM = store.AFM,
                ImageURL = store.ImageURL
                
               
            };
            return storeModel;
        }
        //public async Task<Store> UpdateStorePhotos(UpdateStorePhotos store, Guid userId)
        //{
        //    var user = await _context.Users.FindAsync(userId);
        //    if (user is null)
        //        return null;
        //    var storeToUpdate = await _context.Stores
        //        .Include(s => s.Users)
        //        .FirstOrDefaultAsync(s => s.Id == store.StoreId && s.Users.Any(u => u.Id == userId));
        //    if (storeToUpdate is null)
        //        return null;
        //    if (store.Logo != null)
        //    {
        //        storeToUpdate.ImageURL = store.Logo;
        //    }
        //    if (store.BgPhoto != null)
        //    {
        //        storeToUpdate.Design.BgURL = store.BgPhoto;
        //    }
        //    _context.Stores.Update(storeToUpdate);
        //    await _context.SaveChangesAsync();
        //    return storeToUpdate;
        //}
        public async Task<List<PromoModel>> AddPromos(List<PromoModel> promos, int storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                return null; // or throw an exception based on your use case
            }

            var newPromos = new List<Promo>();
            foreach (var promo in promos)
            {
                var newPromo = new Promo
                {
                    ImageUrl = promo.ImageUrl,
                    StoreId = storeId
                };
                newPromos.Add(newPromo);
                _context.Promos.Add(newPromo);
            }

            await _context.SaveChangesAsync();

            // Map the newly created Promo entities to PromoModel
            var promoModels = newPromos.Select(p => new PromoModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl
            }).ToList();

            return promoModels;
        }

        public async Task<Store> UpdateStoreQrCodeUrl(int storeId, string qrCodeUrl)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                return null; // ή ρίξτε μια εξαίρεση ανάλογα με την περίπτωση χρήσης σας
            }
            store.QRCodeURL = qrCodeUrl;
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            return store;
        }
        public async Task<List<PromoModel>> GetPromosByStoreId(int id)
        {
            var promos = await _context.Promos
                .Where(p => p.StoreId == id)
                .ToListAsync();
            var promoModels = promos.Select(p => new PromoModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                
            }).ToList();
            return promoModels;
        }
    }
}
