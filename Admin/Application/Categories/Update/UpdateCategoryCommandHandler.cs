using Domain.Categories;
using MediatR;

namespace Application.Categories.Update;

internal sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            throw new CategoryNotFoundException(request.CategoryId);
        }

        category.Update(
            request.Name,
            request.Slug,
            request.Icon);

        _categoryRepository.Update(category);
    }
}
