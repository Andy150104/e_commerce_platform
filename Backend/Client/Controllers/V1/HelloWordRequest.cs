using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1;

public class HelloWordRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Hello not null")]
    public string Hello { get; set; }
}