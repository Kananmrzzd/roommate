using Application.Data;
using Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.GetById;

internal sealed class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCategoryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _context
            .Categories
            .Where(p => p.Id == request.CategoryId)
            .Select(p => new CategoryResponse(
                p.Id.Value,
                p.Name,
                p.Slug,
                p.Icon))
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            throw new CategoryNotFoundException(request.CategoryId);
        }

        return category;
    }
}
