using Library.BLL;
using Library.DTO;

public class OrderRepository : IOrderRepository
{
    private readonly DataBaseBLL _db;

    public OrderRepository(DataBaseBLL db)
    {
        _db = db;
    }

    public async Task InsertAsync(Order order)
    {
        await _db.Insert(order, "Order");
    }

    public async Task<List<Order>> GetByQueryAsync(string? query)
    {
        return await _db.GetOrder(query);
    }

    public async Task<bool> DeleteByNameAsync(string name)
    {
        return await _db.DeleteByName<Order>("Order", name);
    }
}