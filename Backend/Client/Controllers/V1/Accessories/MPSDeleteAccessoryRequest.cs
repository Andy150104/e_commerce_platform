namespace Client.Controllers.V1.MPS;

public class MPSDeleteAccessoryRequest : AbstractApiRequest
{
    public List<string> CodeAccessory { get; set; }
}