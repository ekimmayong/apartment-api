using Microsoft.AspNetCore.Mvc;
using MountHebronAppApi.Services;
using MountHebronAppApi.Contracts;

namespace MountHebronAppApi.Controllers
{
    [Route("api/apartment")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private const string ApartmentStorageName = "apartment-images";
        private const string BlogStorageName = "blog-images";

        private readonly IStorageServices _service;
        private readonly ILogger<ApartmentController> _logger;
        public ApartmentController(IStorageServices service, ILogger<ApartmentController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger;
        }

        //Azure Data tables Sections
        [HttpGet]
        [Route("items/{category}")]
        public async Task<IActionResult> ApartmentLists(string category)
        {
            var result = await _service.GetEntityAsync(category);

            return Ok(result);
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
        public async Task<IActionResult> NewApartment([FromForm] ApartmentRequest entity)
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
    }
}