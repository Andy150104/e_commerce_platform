namespace Client.Controllers.V1.Categories;

public class SelectCategoriesResponse : AbstractApiResponse<List<SelectCategoriesEntity>>
{
    public override List<SelectCategoriesEntity> Response { get; set; }
}

public class SelectCategoriesEntity
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public Guid? ParentId { get; set; }
}