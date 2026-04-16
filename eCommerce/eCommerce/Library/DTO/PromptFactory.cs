public static class PromptFactory
{
    public static string GetProductPrompt()
    {
        return @"Analise a imagem (se fornecida) e o texto do prompt.

            Extraia as seguintes informações:
            1. Cores predominantes (ex: ""Azul"", ""Vermelho"")
            2. Definição do objeto principal (ex: ""Placa de Vídeo"")
            3. Destaque (Ex: descrição de alguma imagem ilustrativa na embalagem do produto)
            4. Todos os textos visíveis, em formato de lista, começando por texto maiores e em regiões mais altas da imagem
            5. preço Sugerido, busque referencias ou médias de valores

            Regras:
            - Retorne apenas JSON válido
            - Não inclua explicações fora do JSON
            - Caso não encontre alguma informação, retorne null ou lista vazia

            Formato da resposta:
            {
              ""cores"": [""Azul"", ""Preto""],
              ""definicao"": ""Placa de Vídeo"",
              ""destaque"": ""Um homem de capuz"",
              ""textos"": [""16GB"", ""DLSS""]
              ""preco"": ""3500,00""
            }";
    }
}