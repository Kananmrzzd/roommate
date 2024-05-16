using Domain.Categories;
using MediatR;

namespace Application.Categories.GetById;

public record GetCategoryQuery(CategoryId CategoryId) : IRequest<CategoryResponse>;

public record CategoryResponse(
    Guid Id,
    string Name,
    string Slug,
    string Icon);
