using Domain.Categories;

namespace Application.Categories;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(Category category);
}
