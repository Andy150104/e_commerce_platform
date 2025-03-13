namespace Client.Controllers.V1.Categories;

public class SelectCategoryResponse : AbstractApiResponse<SelectCategoryEntity>
{
    public override SelectCategoryEntity Response { get; set; }
}

public class SelectCategoryEntity
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public Guid? ParentId { get; set; }
}

