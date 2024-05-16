using MediatR;

namespace Application.Categories.Create;

public record CreateCategoryCommand(
    string Name,
    string Slug,
    string Icon) : IRequest;