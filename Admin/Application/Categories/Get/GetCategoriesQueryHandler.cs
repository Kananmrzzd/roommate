using System.Linq.Expressions;
using Application.Data;
using Application.Categories.GetById;
using Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.Categories.Get;

internal sealed class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, PagedList<CategoryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Category> categoriesQuery = _context.Categories;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            categoriesQuery = categoriesQuery.Where(p =>
                p.Name.Contains(request.SearchTerm));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            categoriesQuery = categoriesQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            categoriesQuery = categoriesQuery.OrderBy(GetSortProperty(request));
        }

        var categoryResponsesQuery = categoriesQuery
            .Select(p => new CategoryResponse(
                p.Id.Value,
                p.Name,
                p.Slug,
                p.Icon));

        var categories = await PagedList<CategoryResponse>.CreateAsync(
            categoryResponsesQuery,
            request.Page,
            request.PageSize);

        return categories;
    }

    private static Expression<Func<Category, object>> GetSortProperty(GetCategoriesQuery request) =>
        request.SortColumn?.ToLower() switch
        {
            "name" => category => category.Name,
            "slug" => category => category.Slug,
            "icon" => category => category.Icon,
            _ => category => category.Id
        };
}
