using Library.DTO;

public class ProductService
{
    private readonly IProductRepository _repo;
    private readonly IImageStorageService _storage;
    private readonly IProductAIService _ai;

    public ProductService(
        IProductRepository repo,
        IImageStorageService storage,
        IProductAIService ai)
    {
        _repo = repo;
        _storage = storage;
        _ai = ai;
    }

    public async Task<ProductStore> AnalyzeProductImageAsync(Stream stream, string fileName, string contentType)
    {
        var image = await _storage.SaveAsync(stream, fileName, contentType);

        var analysis = await _ai.AnalyzeAsync(image.Base64);

        var product = ProductMapper.Map(image.Url, analysis);

        await _repo.InsertAsync(product);

        return product;
    }

    public async Task Insert(ProductStore product)
    {
        await _repo.InsertAsync(product);
    }

    public async Task<bool> DeleteByName(string name)
    {
        return await _repo.DeleteByNameAsync(name);
    }

    public async Task<List<ProductStore>> GetByQuery(string query)
    {
        return await _repo.GetByQueryAsync(query);
    }
}