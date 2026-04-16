using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Abstract
{
    public abstract class AI
    {
        public string Key { get; set; }
        public string Name { get; set; }

        public AI() { }

        public abstract Task<string> Execute(string prompt, string? img); 

    }
}
