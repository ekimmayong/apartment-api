using Microsoft.AspNetCore.Mvc;
using MountHebronAppApi.Services;
using MountHebronAppApi.Contracts;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MountHebronAppApi.Controllers
{
    [Route("api/apartment")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private const string ApartmentStorageName = "apartment-images";
        private const string BlogStorageName = "blog-images";
        private readonly IStorageServices _service;
        private readonly IPropertyService _apartmentService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IStorageServices service, ILogger<PropertyController> logger, IPropertyService apartmentService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger;
            _apartmentService = apartmentService;
        }

        //Azure Data tables Sections
        [HttpGet]
        [Route("items/{category}")]
        public async Task<IActionResult> ApartmentLists(string category)
        {
            try
            {
                var result = await _service.GetEntityAsync(category);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);

            }
            
        }

        [HttpGet]
        [Route("item/{category}/{id}")]
        public async Task<IActionResult> ApartmentList(string category, string id)
        {
            var result = await _service.GetEntityByIdAsync(category, id);

            return Ok(result);
        }

        [HttpPost]
        [Route("item")]
        public async Task<IActionResult> NewApartment([FromForm] ApartmentsRequest entity)
        {
            if (entity.File != null)
            {
                Stream stream = entity.File.OpenReadStream();
                var imageUri = await _service.UploadDocument(entity.File.FileName, stream, ApartmentStorageName);

                if(imageUri != "")
                {
                    var createEnity = await _service.UpsertEntityAsync(entity, imageUri);

                    return Ok(createEnity);
                }
            }
            

            return Ok("Invalid Data");
        }

        [HttpDelete]
        [Route("item/{category}/{id}")]
        public async Task<IActionResult> DeleteItem(string category, string id)
        {
            await _service.DeleteEntityAsync(category, id, ApartmentStorageName);

            return Ok();
        }


        //Azure Storage blobs Section
        [HttpGet]
        [Route("documents")]
        public async Task<IActionResult> GetAllDocuments()
        {
            var response = await _service.GetAllDocuments();

            return Ok(response);
        }

        [HttpGet]
        [Route("document/{filename}")]
        public async Task<IActionResult> GetDocument(string filename)
        {
            var response = await _service.GetDocument(filename);

            return Ok(response);
        }

        //SQL

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> AddNewApartment([FromForm] ApartmentRequest request)
        {
            try 
            {
                await _apartmentService.AddNewApartment(request);

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