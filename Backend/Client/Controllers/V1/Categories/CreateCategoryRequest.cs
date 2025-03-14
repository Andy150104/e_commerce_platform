using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Categories;

public class CreateCategoryRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Please enter category name")]
    public string CategoryName { get; set; }

    public Guid? ParentId { get; set; }
}