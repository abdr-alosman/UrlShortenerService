using Microsoft.AspNetCore.Mvc;
using System.Net;
using UrlShortener.Application.src.UrlShortener;
using UrlShortener.Abstractions.src.UrlShortener.Contracts;
using UrlShortener.API.Models;

namespace UrlShortener.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUrlShortenerService _urlShortenerService;
        private readonly IHttpContextAccessor _ctx;
        private readonly ResponseModel response = new();
        public HomeController(IHttpContextAccessor httpContext, IUrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
            _ctx = httpContext;
        }

        [HttpPost("short-url")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromForm] UrlRequestModel request)
        {
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out Uri? inputUrl))
                return BadRequest(response.Error("Invalid Url has been provided!", (int)HttpStatusCode.BadRequest));

            string path = await _urlShortenerService.SaveAsync(request);

            var result = $"{_ctx.HttpContext?.Request.Scheme}://{_ctx.HttpContext?.Request.Host}/{path}";

            return Ok(response.Success(result));
        }


        [HttpPost("custome-url")]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCustomeUrl([FromForm] CustomUrlRequestModel request)
        {
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out Uri? inputUrl))
                return BadRequest(response.Error("Invalid Url has been provided!", (int)HttpStatusCode.BadRequest));


            var result = await _urlShortenerService.SaveCustomeAsync(request);

            if (!result.IsValid)
                return BadRequest(response.Error("You can't use this path please try another one!", (int)HttpStatusCode.BadRequest));


            var shortUrl = $"{_ctx.HttpContext?.Request.Scheme}://{_ctx.HttpContext?.Request.Host}/{result.Path}";

            return Ok(response.Success(shortUrl));
        }

        [HttpGet("{path:required}", Name = "ShortUrls_RedirectTo")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RedirectTo(string path)
        {
            if (path == null)
            {
                return BadRequest(response.Error("Bad Request!", (int)HttpStatusCode.BadRequest));
            }

            var originalUrl = await _urlShortenerService.GetByPath(path);

            if (originalUrl == null)
            {
                return BadRequest(response.Error("Not Found!", (int)HttpStatusCode.NotFound));
            }

            return Redirect(originalUrl);
        }

    }
}
