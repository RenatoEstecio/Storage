using Library.DTO;

public interface IOrderService
{
    Task Insert(Order order);
    Task<bool> DeleteByName(string name);
    Task<List<Order>> GetByQuery(string query);
}

