using System.Net;

namespace UrlShortener.API.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string? ShortUrl { get; set; }

        public ResponseModel Success(string shortUrl)
        {
            ResponseModel model = new ResponseModel()
            {
                IsSuccessful=true,
                ShortUrl = shortUrl,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Successful"
            };
            return model;
        } 
        public ResponseModel Error(string message,int StatusCode)
        {
            ResponseModel model = new ResponseModel()
            {
                IsSuccessful=false,
                ShortUrl = null,
                StatusCode = StatusCode,
                Message = message
            };
            return model;
        }
    }
}
