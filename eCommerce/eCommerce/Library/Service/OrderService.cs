using Library.DTO;
using Library.Interface;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
       
    }

  

    public async Task Insert(Order order)
    {
        await _repo.InsertAsync(order);
    }

    public async Task<bool> DeleteByName(string name)
    {
        return await _repo.DeleteByNameAsync(name);
    }

    public async Task<List<Order>> GetByQuery(string query)
    {
        return await _repo.GetByQueryAsync(query);
    }
}