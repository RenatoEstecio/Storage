using Library.BLL;
using Library.DTO;

public class ProductRepository : IProductRepository
{
    private readonly DataBaseBLL _db;

    public ProductRepository(DataBaseBLL db)
    {
        _db = db;
    }

    public async Task InsertAsync(ProductStore product)
    {
        await _db.Insert(product, "Product");
    }

    public async Task<List<ProductStore>> GetByQueryAsync(string? query)
    {
        return await _db.Get(query);
    }

    public async Task<bool> DeleteByNameAsync(string name)
    {
        return await _db.DeleteByName<ProductStore>("Product", name);
    }
}