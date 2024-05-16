using Domain.Categories;
using MediatR;

namespace Application.Categories.Delete;

internal sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            throw new CategoryNotFoundException(request.CategoryId);
        }

        _categoryRepository.Remove(category);
    }
}
