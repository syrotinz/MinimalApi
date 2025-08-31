using Microsoft.EntityFrameworkCore;
using Npgsql;
using TestProject.Data;
using TestProject.Domain;
using TestProject.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

static bool IsUniqueViolation(DbUpdateException ex)
{
    if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505") 
        return true;
    return false;
}

app.MapPost("/api/products", async (ProductCreateDto dto, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name))
        return Results.BadRequest(new { errors = "required name" });

    if (dto.Name.Trim().Length < 2)
        return Results.BadRequest(new { errors = "required name >= 2" });

    if (dto.Name.Trim().Length > 100)
        return Results.BadRequest(new { errors = "required name < 100" });

    if (dto.Price <= 0)
        return Results.BadRequest(new { errors = "required price > 0" });

    var product = new Product(dto.Name.Trim(), dto.Price);
    db.Products.Add(product);

    try
    {
        await db.SaveChangesAsync();
    }
    catch (DbUpdateException ex) when (IsUniqueViolation(ex))
    {
        return Results.BadRequest(new { error = "name exists" });
    }

    var response = new ProductReadDto(product.Id, product.Name, product.Price, product.CreatedAt);
    return Results.Created($"/products/{product.Id}", response);
});

app.MapGet("/products/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var p = await db.Products.FindAsync(id);
    if (p is null) 
        return Results.NotFound();
    return Results.Ok(new ProductReadDto(p.Id, p.Name, p.Price, p.CreatedAt));
});

app.Run();

public partial class Program { }