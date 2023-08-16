using UrlShortener.Abstractions.src.UrlShortener.Contracts;

namespace UrlShortener.Application.src.UrlShortener
{
    public interface IUrlShortenerService
    {
        Task<string> SaveAsync(UrlRequestModel request);

        Task<CustomUrlResponseModel> SaveCustomeAsync(CustomUrlRequestModel request);

        Task<string> GetByPath(string path);
    }
}
