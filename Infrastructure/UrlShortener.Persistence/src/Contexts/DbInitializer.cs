using UrlShortener.Domain.src;

namespace UrlShortener.Persistence.src.Contexts
{
    public static class DbInitializer
    {
        public static void Initialize(MainDbContext context)
        {
            if (context.UrlManagement.Any()) return;

            // Test Example
            var product = new List<UrlManagement>
            {
                new UrlManagement
                {
                   Id=Guid.NewGuid(),
                   ShortUrl="hacker",
                   Url="https://www.hackerrank.com/"
                },
            };

            context.UrlManagement.AddRange(product);

            context.SaveChanges();
        }
    }
}
