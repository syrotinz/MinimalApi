namespace TestProject.Dtos;

public record ProductCreateDto(string Name, decimal Price);

public record ProductReadDto(Guid Id, string Name, decimal Price, DateTime CreatedAt);
