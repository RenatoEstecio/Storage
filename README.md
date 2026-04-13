Product Vision API (Storage)

Uma aplicação inteligente que recebe uma imagem de produto e extrai automaticamente informações relevantes usando visão computacional e IA.

🚀 Sobre o projeto

O Product Vision API é uma API que analisa imagens de produtos e retorna dados estruturados úteis para e-commerce, catálogos e sistemas de recomendação.

A partir de uma simples foto, a aplicação é capaz de:

🏷️ Gerar tags automáticas com base no conteúdo da imagem
🎨 Identificar cores predominantes
🧠 Classificar o tipo do item (ex: computador, console, celular)
✨ Gerar descrições inteligentes (ex: "mulher anjo segurando uma espada")
💰 Sugerir um valor estimado do produto
📝 Extrair e organizar informações em formato estruturado
🧠 Como funciona
O usuário envia uma imagem via endpoint (multipart/form-data)
A API processa a imagem utilizando serviços de IA
São extraídas informações como:
Objetos detectados
Texto (OCR)
Cores dominantes
Contexto da imagem
Os dados são enriquecidos e retornados como JSON
📡 Endpoint principal
Upload de imagem
POST /api/Product/File
Exemplo usando cURL
curl -X 'POST' \
  'https://localhost:44327/api/Product/File' \
  -H 'accept: */*' \
  -H 'Content-Type: multipart/form-data' \
  -F 'file=@produto.jpg;type=image/jpeg'

![PS5](https://github.com/user-attachments/assets/ee58771e-631b-4f20-b8e2-8a39684b5104)
  
📦 Exemplo de resposta
{
  "name": "Console PlayStation 5 com jogos",
  "price": 4800,
  "tags": [
    "sony",
    "ps5",
    "4k",
    "hdr",
    "console",
    "gran turismo",
    "astro bot"
  ],
  "colors": [
    "Branco",
    "Preto",
    "Azul"
  ],
  "observation": "Console com controle e jogos inclusos",
  "imgUrl": "https://.../image.jpg",
  "type": "Console PlayStation 5"
}
🎯 Funcionalidades
✔️ Upload de imagens via API REST
✔️ Extração de texto (OCR)
✔️ Detecção de objetos e contexto
✔️ Geração automática de tags
✔️ Identificação de cores predominantes
✔️ Classificação do produto
✔️ Geração de descrição inteligente
✔️ Sugestão de preço baseada no item

🛠️ Tecnologias utilizadas

.NET 8
ASP.NET Core Web API
Serviços de IA (Computer Vision / NLP)
Armazenamento em nuvem (ex: AWS S3)


💡 Casos de uso

E-commerce (cadastro automático de produtos)
Marketplaces
Apps de revenda (tipo OLX / Mercado Livre)
Organização de catálogos
Automação de inventário


📈 Diferenciais

Reduz esforço manual no cadastro de produtos
Melhora SEO com tags automáticas
Enriquece dados com descrição inteligente
Escalável e fácil de integrar


👨‍💻 Autor

Renato Estecio
