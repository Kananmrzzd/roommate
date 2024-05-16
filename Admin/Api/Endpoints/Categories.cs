using Application.Categories.Create;
using Application.Categories.Get;
using Application.Categories.GetById;
using Application.Categories.Update;
using Application.Categories.Delete;
using Carter;
using Domain.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Endpoints;

public class Categories : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", async (CreateCategoryCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        app.MapGet("categories", async (
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender) =>
        {
            var query = new GetCategoriesQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

            var categories = await sender.Send(query);

            return Results.Ok(categories);
        });

        app.MapGet("categories/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetCategoryQuery(new CategoryId(id))));
            }
            catch (CategoryNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapDelete("categories/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteCategoryCommand(new CategoryId(id)));

                return Results.NoContent();
            }
            catch (CategoryNotFoundException e)
            {
                return Results.NotFound(e.Message);
            }
        });

        app.MapPut("categories/{id:guid}", async (Guid id, [FromBody] UpdateCategoryRequest request, ISender sender) =>
        {
            var command = new UpdateCategoryCommand(
                new CategoryId(id),
                request.Name,
                request.Slug,
                request.Icon);
            
            await sender.Send(command);

            return Results.NoContent();
        });
    }
}
