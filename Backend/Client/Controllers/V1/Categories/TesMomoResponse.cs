using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.V1.Categories;

public class TesMomoResponse : AbstractApiResponse<IActionResult>
{
    public override IActionResult Response { get; set; }
}