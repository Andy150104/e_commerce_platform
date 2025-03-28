﻿using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.AEPS;

public class AEPSAddExchangeAccessoryRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Images is required")]
    public List<IFormFile> Images { get; set; }
}