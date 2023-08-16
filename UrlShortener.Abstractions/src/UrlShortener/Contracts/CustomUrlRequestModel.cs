
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Abstractions.src.UrlShortener.Contracts
{
    public class CustomUrlRequestModel
    {
        [Required]
        [Display(Name = "Enter valid url to short")]
        public string Url { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "It must be a maximum of {1} characters long!")]
        public string Path { get; set; }
    }
}
