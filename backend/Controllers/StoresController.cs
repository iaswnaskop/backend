using backend.Services.StoreServices;
using backend.Services.CloudinaryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services.AuthServices;
using System.Text.Json;

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
        private readonly ILogger<StoresController> _logger;
        public StoresController(IStoreService storeService, ICloudinaryService cloudinaryService, IAuthService authService, ILogger<StoresController> logger)
        {
            _storeService = storeService;
            _cloudinaryService = cloudinaryService;
            _authService = authService;
            _logger = logger;
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
            var promo = await _storeService.GetPromosByStoreId(store.Id);
            var storeModel = new StoreModel
            {
                Id = store.Id,
                Description = store.Description,
                Name = store.Name,
                Address = store.Address,
                AFM = store.AFM,
                Phone = store.Phone,
                ImageURL = store.ImageURL,
                Email = store.Email,
                Facebook = store.Facebook,
                Instagram = store.Instagram,
                TikTok = store.TikTok,
                TripAdvisor = store.TripAdvisor,
                GoogleBusiness = store.GoogleBusiness,
                PurchasingManager = store.PurchasingManager,
                WifiName = store.WifiName,
                WifiPassword = store.WifiPassword,
                StorePhone = store.StorePhone,
                QRCodeURL = store.QRCodeURL,
                Card = store.Card,
                IRIS = store.IRIS,
                PayPal = store.PayPal,
                BitCoin = store.BitCoin,
                Cash = store.Cash,
                IsActive = store.IsActive,
                Categories = store.Categories.Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    NameEng = c.NameEng,
                    NameDu = c.NameDu,
                    NameFr = c.NameFr,
                    NameIt = c.NameIt,
                    NameEs = c.NameEs,
                    DescriptionEng = c.DescriptionEng,
                    DescriptionDu = c.DescriptionDu,
                    DescriptionFr = c.DescriptionFr,
                    DescriptionIt = c.DescriptionIt,
                    DescriptionEs = c.DescriptionEs,
                    Description = c.Description
                    //ImageURL = c.ImageURL
                }).ToList(),
                ProductDetails = store.ProductDetails.Select(pd => new ProductDetailModel
                {
                    Id = pd.Id,
                    Name = pd.Name,
                    NameEng = pd.NameEng,
                    NameEs = pd.NameEs,
                    NameDu = pd.NameDu,
                    NameFr = pd.NameFr,
                    NameIt = pd.NameIt,
                    Available = pd.Available,
                    DescriptionEng = pd.DescriptionEng,
                    DescriptionDu = pd.DescriptionDu,
                    DescriptionFr = pd.DescriptionFr,
                    DescriptionIt = pd.DescriptionIt,
                    DescriptionEs = pd.DescriptionEs,
                    Description = pd.Description,
                    Price = pd.Price,
                    CategoryId = pd.CategoryId,
                    //ImageURL = pd.ImageURL,
                }).ToList(),
                StoreType = store.StoreTypes.Select(st => new TypeModel
                {
                    Id = st.Type.Id,
                    Name = st.Type.Name
                    //Description = st.Description
                }).ToList(),
                Languages = store.StoreLanguages.Select(sl => new LanguageModel
                {
                    Id = sl.Language.Id,
                    CountryLang = sl.Language.CountryLang,
                    Abbr = sl.Language.Abbr
                    //Description = sl.Description
                }).ToList(),
                Design = store.Design.Select(d => new GetDesign
                {
                    Id = d.Id,
                    BgColor = d.BgColor,
                    BgURL = d.BgURL,
                    Font = d.Font,
                    DesignModel = new DesignModel
                    {
                        Id = d.DesignModel.Id,
                        Model = d.DesignModel.Model,
                    }
                }).ToList(),
                Schedules = store.Schedules.Select(s => new Schedule
                {
                    Id = s.Id,
                    DayOfWeek = s.DayOfWeek,
                    OpeningTime = s.OpeningTime,
                    ClosingTime = s.ClosingTime,
                    IsClosed = s.IsClosed
                }).ToList(),
                Promo = promo.Select(p => new PromoModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    
                }).ToList()

            };
            



            return Ok(storeModel);
        }

        
        [HttpPost("add-store")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Store>> AddStore([FromForm] AddStoreModel store)
        {
            if (store is null)
                return BadRequest("Invalid store data or user ID");
            var urls = new List<string>();
            if (store.Logo is null && store.DesignColor is null)
                return BadRequest("Please upload images and Backround color");

            

            if (store.BgPhoto is not null)
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
            if (store.BgPhoto is not null)
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


        [HttpPut("update-store/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<UpdateStoreModel>> UpdateStore(int id, [FromForm] UpdateStoreForm request)
        {
            try
            {
                var model = JsonSerializer.Deserialize<UpdateStoreModel>(request.JsonData);

                if (model == null)
                    return BadRequest("Invalid JSON data");

                if (request.Logo != null)
                {
                    model.ImageURL = await _cloudinaryService.UpdatePhoto(request.Logo, model.AFM);
                }

                var store = await _storeService.UpdateStore(id, model);
                if (store is null)
                    return NotFound("Store not found");

                var result = new UpdateStoreModel
                {
                    Description = store.Description,
                    Name = store.Name,
                    Address = store.Address,
                    AFM = store.AFM,
                    Phone = store.Phone,
                    ImageURL = store.ImageURL,
                    Email = store.Email,
                    Facebook = store.Facebook,
                    Instagram = store.Instagram,
                    TikTok = store.TikTok,
                    TripAdvisor = store.TripAdvisor,
                    GoogleBusiness = store.GoogleBusiness,
                    PurchasingManager = store.PurchasingManager,
                    WifiName = store.WifiName,
                    WifiPassword = store.WifiPassword,
                    StorePhone = store.StorePhone,
                    QRCodeURL = store.QRCodeURL,
                    ScheduleModel = store.Schedules.Select(s => new ScheduleModel
                    {
                        DayOfWeek = (int)s.DayOfWeek,
                        OpeningTime = s.OpeningTime.ToString(@"hh\:mm"),
                        ClosingTime = s.ClosingTime.ToString(@"hh\:mm"),
                        IsClosed = s.IsClosed
                    }).ToList(),
                    Cash = store.Cash,
                    Card = store.Card,
                    PayPal = store.PayPal,
                    BitCoin = store.BitCoin,
                    IRIS = store.IRIS,
                    IsActive = store.IsActive,
                    Languages = store.StoreLanguages.Select(sl => sl.LanguageId).ToList()
                };

                return Ok(result);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON parsing error while updating store {StoreId}", id);
                return BadRequest(new { error = "JSON parse error", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating store {StoreId}", id);
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }
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
        [HttpPost("add-promos")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<List<PromoModel>>> AddPromos([FromForm] List<PromoModel> promos, int storeId)
        {
            var store = await _storeService.GetStoreByStoreId(storeId);
            var afm = store.AFM;
            var files = promos.Select(p => p.Image).ToList();
            var urls = await _cloudinaryService.UploadPromoPhotos(files, afm);
            for (int i = 0; i < promos.Count; i++)
            {
                promos[i].ImageUrl = urls.ElementAtOrDefault(i);
            }
            var addedPromos = await _storeService.AddPromos(promos, storeId);
            return Ok(addedPromos);
        }
    }
}
