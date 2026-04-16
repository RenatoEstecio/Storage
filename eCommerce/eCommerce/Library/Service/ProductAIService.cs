using Library.BLL;
using Library.DTO;
using System.Text.Json;
using System.Xml.Linq;

public class ProductAIService : IProductAIService
{
    private readonly GeminiBLL _gemini;

    public ProductAIService(GeminiBLL gemini)
    {
        _gemini = gemini;
    }

    public async Task<ProductAIResult> AnalyzeAsync(string base64Image)
    {
        var prompt = PromptFactory.GetProductPrompt();

        var ai = new AIBLL(_gemini);

        var json = await ai.Execute(prompt, base64Image);

        json = json.Replace("```json", "").Replace("```", "");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<ProductAIResult>(json, options)!;
     
        return result;
    }
}