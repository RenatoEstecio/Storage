using Library.DTO;

public interface IProductAIService
{
    Task<ProductAIResult> AnalyzeAsync(string base64Image);
}