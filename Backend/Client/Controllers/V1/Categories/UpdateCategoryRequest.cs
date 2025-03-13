namespace Client.Controllers.V1.Categories;

public class UpdateCategoryRequest : AbstractApiRequest
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public Guid? ParentId { get; set; }
}