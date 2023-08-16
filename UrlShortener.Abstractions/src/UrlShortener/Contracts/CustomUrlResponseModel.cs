

namespace UrlShortener.Abstractions.src.UrlShortener.Contracts
{
    public class CustomUrlResponseModel
    {
        public string Path { get; set; }

        public bool IsValid { get; set; } = true;
    }
}
