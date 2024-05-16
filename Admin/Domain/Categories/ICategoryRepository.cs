namespace Domain.Categories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(CategoryId id);

    void Add(Category category);

    void Update(Category category);

    void Remove(Category category);
}