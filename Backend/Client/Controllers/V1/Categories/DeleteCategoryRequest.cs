namespace Client.Controllers.V1.Categories;

public class DeleteCategoryRequest : AbstractApiRequest
{
    public Guid? CategoryId { get; set; }
}