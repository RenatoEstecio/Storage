using Library.DTO;
using SharpCompress.Compressors.Xz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Library.BLL
{
    public class ProductBLL
    {
        private readonly DataBaseBLL _db;
        private readonly GeminiBLL _gemini;
        private readonly S3ServiceBLL _s3;

        public ProductBLL(DataBaseBLL db, GeminiBLL gemini, S3ServiceBLL s3)
        {
            _db = db;
            _gemini = gemini;
            _s3 = s3;
        }

        public async Task<List<ProductStore>?> GetByQuery(string? query)
        {
            DataBaseBLL db = _db;
            List<ProductStore>? list;

            if (query is null || query.Trim().Length == 0)
                list = await db.Get(query);
            else
            {
                var terms = query.Trim().Replace("  "," ").Split(' ').ToList();

                string queryRgx = string.Empty;

                queryRgx += string.Join("|", terms);

                list = await db.Get(query);             
            }

            return list;
        }

        public async Task<ProductStore?> Verify(Stream stream, string fileName, string contentType)
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            var bytes = ms.ToArray();

            Product product = new Product();

            // upload
            using var uploadStream = new MemoryStream(bytes);
            product.ImgLink = await Upload(uploadStream, fileName, contentType);

            // base64
            product.Img64 = Convert.ToBase64String(bytes);

            return await Verify(product);
        }

        public async Task<string> Upload(Stream stream, string? fileName = null, string? contentType = null)
        {            
            return await _s3.UploadArquivoAsync(stream, fileName, contentType);
        }

        public async Task<ProductStore?> Verify(Product product)
        {
            string cmd_verify = GetPrombt();

            if (product.ImgLink == null)
                product.ImgLink = await Upload(FileBLL.Base64ToStream(product.Img64));

            AIBLL ai = new AIBLL(_gemini);
           
            var json = await ai.Execute(cmd_verify, product.Img64);
            
            json = json.Replace("```json","").Replace("```","");

            var element = JsonDocument.Parse(json.Trim()).RootElement.Clone();

            ProductStore store = new ProductStore();

            store.Name = product.Name;
            store.Price = product.Price;

            store.Colors = element.GetProperty("cores")
            .EnumerateArray()
            .Select(x => x.GetString())
            .Where(x => x != null)
            .Select(x => x!)
            .ToList();

            store.Tags = element.GetProperty("textos")
            .EnumerateArray()
            .Select(x => x.GetString())
            .Where(x => x != null)
            .Select(x => x!)
            .ToList();

            store.Observation = element.GetProperty("destaque").GetString();
            store.Type = element.GetProperty("definicao").GetString();
            store.ImgUrl = product.ImgLink;

            try { store.Price = Convert.ToDecimal(element.GetProperty("preco").GetString()); } catch { }

            if(store.Name is null || store.Name.Length == 0)
            {
                store.Name = store.Type ?? string.Empty;

                foreach (var s in store.Tags)
                    if ((store.Name??string.Empty).Length + s.Length > 80)
                        break;
                    else
                        store.Name += " " + s;
            }

            store.Tags = store.Tags//Remover acentos / normalizar
                .Select(t => RemoverAcentos(t).ToLower())
                .ToList(); 
         
            return await Insert(store);
        }

        async Task<ProductStore?> Insert(ProductStore ps)
        {
            DataBaseBLL db = _db;

            await db.Insert(ps, "Product");

            return ps;
        }

        public async Task<bool> DeleteByName(string name)
        {
            DataBaseBLL db = _db;

            return await db.DeleteByName<ProductStore>("Product",name);
        }

        public static string RemoverAcentos(string texto)
        {
            var normalized = texto.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new System.Text.StringBuilder();

            foreach (var c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c)
                    != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

        string GetPrombt()
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
}
