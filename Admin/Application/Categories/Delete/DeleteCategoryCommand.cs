using Domain.Categories;
using MediatR;

namespace Application.Categories.Delete;

public record DeleteCategoryCommand(CategoryId CategoryId) : IRequest;
