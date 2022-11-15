using Microsoft.AspNetCore.Mvc;
using MountHebronAppApi.Services;
using MountHebronAppApi.Contracts;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MountHebronAppApi.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private const string ApartmentStorageName = "apartment-images";
        private const string BlogStorageName = "blog-images";
        private readonly IStorageServices _storageService;
        private readonly IPropertyService _propertyService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IStorageServices storageService, ILogger<PropertyController> logger, IPropertyService propertyService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _logger = logger;
            _propertyService = propertyService;
        }

        //Apartment Section
        [HttpGet]
        [Route("apartments")]
        public async Task<IActionResult> ApartmentLists()
        {
            try
            {
                var result = await _propertyService.GetApartments();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);

            }
        }

        [HttpGet]
        [Route("apartment/{uid}")]
        public async Task<IActionResult> ApartmentList(Guid uid)
        {
            try
            {
                var result = await _propertyService.GetApartment(uid);

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
            
        }

        [HttpPost]
        [Route("apartment")]
        public async Task<IActionResult> NewApartment([FromForm] ApartmentRequest request)
        {
            try
            {
                await _propertyService.AddNewApartment(request);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            } 
        }

        [HttpDelete]
        [Route("apartment/{uid}")]
        public async Task<IActionResult> DeleteItem(Guid uid)
        {
            try
            {
                await _propertyService.DeleteApartment(uid);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }


        //Categories
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var response = await _propertyService.GetCategories();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
            
        }

        [HttpGet]
        [Route("category/{uid}")]
        public async Task<IActionResult> GetDocument(Guid uid)
        {
            try
            {
                var response = await _propertyService.GetCategory(uid);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> AddNewApartment([FromBody] CategoryRequest request)
        {
            try 
            {
                await _propertyService.AddNewCategory(request);

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}