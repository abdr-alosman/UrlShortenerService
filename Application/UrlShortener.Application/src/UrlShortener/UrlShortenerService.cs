
using Microsoft.EntityFrameworkCore;
using UrlShortener.Abstractions.src.UrlShortener;
using UrlShortener.Abstractions.src.UrlShortener.Contracts;
using UrlShortener.Domain.src;
using UrlShortener.Persistence.src.Contexts;

namespace UrlShortener.Application.src.UrlShortener
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";
        private readonly MainDbContext _db;

        public UrlShortenerService(MainDbContext db)
        {
            _db = db;
        }

        public async Task<string> GetByPath(string path)
        {
            var usrlMatch = await _db.UrlManagement.FirstOrDefaultAsync(x => x.ShortUrl.Trim() == path.Trim());

            if (usrlMatch == null)
                return String.Empty;

            return usrlMatch.Url;
        }

        public async Task<string> SaveAsync(UrlRequestModel request)
        {
            var random = new Random();

            string randomStr = new string(Enumerable.Repeat(Alphabet, 6)
               .Select(x => x[random.Next(x.Length)]).ToArray());

            var sUrl = new UrlManagement()
            {
                Url = request.Url,
                ShortUrl = randomStr
            };

            await _db.UrlManagement.AddAsync(sUrl);
            await _db.SaveChangesAsync();

            return randomStr;
        }

        public async Task<CustomUrlResponseModel> SaveCustomeAsync(CustomUrlRequestModel request)
        {
            bool usrlMatch = await _db.UrlManagement.AnyAsync(x => x.ShortUrl.Trim() == request.Path.Trim());

            CustomUrlResponseModel response = new();
            if (usrlMatch)
            {
                response.IsValid = false;
                return response;
            }

            var sUrl = new UrlManagement()
            {
                Url = request.Url,
                ShortUrl = request.Path
            };

            await _db.UrlManagement.AddAsync(sUrl);
            await _db.SaveChangesAsync();

            response.Path = sUrl.ShortUrl;

            return response;
        }
    }
}
