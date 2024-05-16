using System.Net.Http.Headers;

namespace Domain.Categories;
public class Category
{
    public Category(CategoryId id, string name, string slug, string icon)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Icon = icon;
    }
    private Category()
    {
    }
    public CategoryId Id { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Icon { get; private set; }

    public void Update(string name, string slug, string icon)
    {
        Name = name;
        Slug = slug;
        Icon = icon;
    }
}
