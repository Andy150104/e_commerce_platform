using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.V1.ThirdParty;

public class MomoOrderResponse : AbstractApiResponse<IActionResult>
{
    public override IActionResult Response { get; set; }
}