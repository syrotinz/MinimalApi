namespace TestProject.Domain;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Product() { }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
