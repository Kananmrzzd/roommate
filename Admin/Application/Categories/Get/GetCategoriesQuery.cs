using Application.Categories.GetById;
using MediatR;

namespace Application.Categories.Get;

public record GetCategoriesQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<PagedList<CategoryResponse>>;