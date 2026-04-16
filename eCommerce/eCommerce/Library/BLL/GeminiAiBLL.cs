




using GenerativeAI;
using GenerativeAI.Types;
using Library.Abstract;
using Library.Util;
using Microsoft.Extensions.Configuration;

namespace Library.BLL
{
    public class GeminiBLL : AI
    {
        public GeminiBLL(IConfiguration config)
        {
            this.Key = config["Gemini:Key"];
           
            this.Name = "Gemini";                     
        }

        override public async Task<string> Execute(string prompt, string? img)
        {
            var client = new GenerativeModel(apiKey: Key, model: "gemini-2.5-flash");
           
            try
            {
                GenerateContentResponse response;

                if (img is not null)
                {
                    var textPart = new Part 
                    { 
                        Text = prompt
                    };

                    var imagePart = new Part
                    {
                        InlineData = new Blob
                        {
                            MimeType = "image/jpeg", // Ajuste conforme o formato (image/png, etc)
                            Data = img.Replace("data:image/jpeg;base64,","")
                        }
                    };

                    var content = new Content
                    {
                        Parts = { textPart, imagePart }
                    };

                    var request = new GenerateContentRequest
                    {
                        Contents = new List<Content>
                        {
                            content
                        }
                    };

                    response = await client.GenerateContentAsync(request);                   
                }
                else
                    response = await client.GenerateContentAsync(prompt);          
              
                return response.Text;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNAVAILABLE (Code:"))
                    throw new CustomException("Serviço Indisponível", System.Net.HttpStatusCode.ServiceUnavailable);
                else
                    throw new CustomException("Erro ao processar Imagem", System.Net.HttpStatusCode.InternalServerError);
            }
        }

    }

  
}
