namespace Client.Controllers.V1;

public class HelloWordResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}