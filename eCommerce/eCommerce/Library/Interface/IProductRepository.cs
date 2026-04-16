using Library.DTO;

public interface IProductRepository
{
    Task InsertAsync(ProductStore product);
    Task<List<ProductStore>> GetByQueryAsync(string? query);
    Task<bool> DeleteByNameAsync(string name);
}