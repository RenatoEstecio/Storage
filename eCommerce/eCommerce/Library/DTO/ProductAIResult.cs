using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    public class ProductAIResult
    {
        public List<string>? Cores { get; set; }
        public string? Definicao { get; set; }
        public string? Destaque { get; set; }
        public List<string>? Textos { get; set; }
        public string? Preco { get; set; }
    }
}
