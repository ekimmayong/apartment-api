using Microsoft.AspNetCore.Mvc;
using MountHebronAppApi.Contracts;
using MountHebronAppApi.Services;

namespace MountHebronAppApi.Controllers
{
    [ApiController]
    [Route("api/blog")]
    public class BlogController : ControllerBase
    {
        private const string ApartmentStorageName = "apartment-images";
        private const string BlogStorageName = "blog-images";
        private readonly ILogger<BlogController> _logger;
        private readonly IStorageServices _service;

        public BlogController(ILogger<BlogController> logger, IStorageServices service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var response = await _service.GetBlogs(category);

            return Ok(response);
        }

        [HttpGet]
        [Route("{category}/{id}")]
        public async Task<IActionResult> GetBligById(string category, string id)
        {
            var response = await _service.GetBlogsById(category, id);

            return Ok(response);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> NewBlog([FromForm]BlogRequest request)
        {
            if (request.File != null)
            {
                Stream stream = request.File.OpenReadStream();
                var imageUri = await _service.UploadDocument(request.File.FileName, stream, BlogStorageName);

                if (imageUri != "")
                {
                    var createEnity = await _service.InsetNewBlog(request, imageUri);

                    return Ok(createEnity);
                }
            }


            return Ok("Invalid Data");
        }

        [HttpDelete]
        [Route("{category}/{id}")]
        public async Task<IActionResult> DeleteBlog(string category, string id)
        {
             await _service.DeleteBlog(category, id, BlogStorageName);

            return Ok();
        }
    }
}


