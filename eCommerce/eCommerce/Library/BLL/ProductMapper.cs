using Library.DTO;
using Library.Util;
using System.Globalization;
using System.Text;

public static class ProductMapper
{
    public static ProductStore Map(string imgUrl, ProductAIResult ai)
    {
        var store = new ProductStore
        {
            Name = BuildName(ai),
            ImgUrl = imgUrl,
            Type = ai.Definicao,
            Observation = ai.Destaque,
            Colors = ai.Cores ?? new List<string>(),
            Tags = Normalize(ai.Textos),
            Price = ParsePrice(ai.Preco)
        };   

        return store;
    }

    private static List<string> Normalize(List<string>? tags)
        => tags?.Select(t => RemoveAccents(t).ToLower()).ToList() ?? new();

    private static decimal ParsePrice(string? price)
        => decimal.TryParse(price, out var p) ? p : 0;

    private static string BuildName(ProductAIResult aiResult)
    {
        if (aiResult.Definicao == null)
            throw new CustomException("Imagem incompatível", System.Net.HttpStatusCode.BadRequest);
        else
        {
            var name = aiResult.Definicao;

            foreach (var tag in aiResult.Textos)
            {
                if ((name + tag).Length > 80) break;
                name += " " + tag;
            }

            return name.Trim().ToUpper();
        }
    }

    private static string RemoveAccents(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}