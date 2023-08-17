using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Abstractions.src.UrlShortener.Contracts
{
    public class UrlRequestModel
    {
        [Required]
        [Display(Name = "Enter valid url to short")]
        public string Url { get; set; }
    }
}
