using Amazon.S3.Model;
using Library.DTO;

public interface IOrderRepository
{
    Task InsertAsync(Order product);
    Task<List<Order>> GetByQueryAsync(string? query);
    Task<bool> DeleteByNameAsync(string name);
}