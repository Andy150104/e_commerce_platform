﻿using Client.Controllers;
using server.Models;

namespace server.Controllers.V1.DisplayProductScreenExchange
{
    public class DPSESAddToQueueResponse : AbstractApiResponse<string>
    {
        public override string Response { get; set; }
    }
}
