﻿using Client.Controllers;
using server.Models;

namespace server.Controllers.V1.AddExchangeProductScreen
{
    public class AEPSAddExchangeProductResponse : AbstractApiResponse<string>
    {
        public override string Response { get; set; }
    }
}
