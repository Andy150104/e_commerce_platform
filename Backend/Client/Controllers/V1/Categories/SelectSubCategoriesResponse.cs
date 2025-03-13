namespace Client.Controllers.V1.Categories;

public class SelectSubCategoriesResponse : AbstractApiResponse<List<SelectSubCategoriesEntity>>
{
    public override List<SelectSubCategoriesEntity> Response { get; set; }
}

public class SelectSubCategoriesEntity
{
    public Guid CategoryId { get; set; }
    
    public string CategoryName { get; set; }
}