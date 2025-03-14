namespace Client.Controllers.V1.Categories;

public class CreateCategoryResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}