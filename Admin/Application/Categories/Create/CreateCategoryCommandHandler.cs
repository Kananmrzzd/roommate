using Application.Data;
using Domain.Categories;
using MediatR;

namespace Application.Categories.Create;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(
            new CategoryId(Guid.NewGuid()),
            request.Name,
            request.Slug,
            request.Icon);

        _categoryRepository.Add(category);

        return Task.CompletedTask;
    }
}
