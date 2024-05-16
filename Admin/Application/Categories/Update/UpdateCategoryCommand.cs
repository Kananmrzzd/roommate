using MediatR;
using Domain.Categories;

namespace Application.Categories.Update;


public record UpdateCategoryCommand(
    CategoryId CategoryId,
    string Name,
    string Slug,
    string Icon) : IRequest;
public record UpdateCategoryRequest(
    string Name,
    string Slug,
    string Icon);