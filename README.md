**Product Vision API (Storage)**
<br><br>
Uma aplicação inteligente que recebe uma imagem de produto e extrai automaticamente informações relevantes usando visão computacional e IA.
<br><br>
🚀 Sobre o projeto

O Product Vision API é uma API que analisa imagens de produtos e retorna dados estruturados úteis para e-commerce, catálogos e sistemas de recomendação.
<br>
A partir de uma simples foto, a aplicação é capaz de:
<br><br>
🏷️ Gerar tags automáticas com base no conteúdo da imagem<br><br>
🎨 Identificar cores predominantes<br><br>
🧠 Classificar o tipo do item (ex: computador, console, celular)<br><br>
✨ Gerar descrições inteligentes (ex: "mulher anjo segurando uma espada")<br><br>
💰 Sugerir um valor estimado do produto<br><br>
📝 Extrair e organizar informações em formato estruturado<br><br>
**Como funciona**<br>
<br>
O usuário envia uma imagem via endpoint (multipart/form-data)<br>
A API processa a imagem utilizando serviços de IA<br>
São extraídas informações como:<br><br>
Objetos detectados<br>
Texto (OCR)<br>
Cores dominantes<br>
Contexto da imagem<br>
Os dados são enriquecidos e retornados como JSON<br><br>
📡 Endpoint principal<br><br>
Upload de imagem
<br>
POST /api/Product/File

Exemplo usando cURL

curl -X 'POST' \
  '/api/Product/File' \
  -H 'accept: */*' \
  -H 'Content-Type: multipart/form-data' \
  -F 'file=@produto.jpg;type=image/jpeg'
<br><br><br>
![PS5](https://github.com/user-attachments/assets/ee58771e-631b-4f20-b8e2-8a39684b5104)
  <br>
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

<br>
🎯 Funcionalidades
<br><br>

✔️ Upload de imagens via API REST

✔️ Extração de texto (OCR)

✔️ Detecção de objetos e contexto

✔️ Geração automática de tags

✔️ Identificação de cores predominantes

✔️ Classificação do produto

✔️ Geração de descrição inteligente

✔️ Sugestão de preço baseada no item



<br><br>
🛠️ Tecnologias utilizadas<br>
<br>
.NET 10
ASP.NET Core Web API
Serviços de IA (Computer Vision / NLP)
Armazenamento em nuvem (ex: AWS S3)

<br><br>
💡 Casos de uso<br>
<br>
E-commerce (cadastro automático de produtos)
Marketplaces
Apps de revenda (tipo OLX / Mercado Livre)
Organização de catálogos
Automação de inventário

<br><br>
📈 Diferenciais<br>
<br>
Reduz esforço manual no cadastro de produtos
Melhora SEO com tags automáticas
Enriquece dados com descrição inteligente
Escalável e fácil de integrar

<br><br>
👨‍💻 Autor<br>
<br>
Renato Estecio
