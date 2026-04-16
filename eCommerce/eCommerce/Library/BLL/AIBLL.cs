using Library.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL
{
    public class AIBLL
    {
        AI ai;
        public AIBLL(AI ai) 
        { 
            this.ai = ai;
        }

        public async Task<string> Execute(string prompt, string? img)
        {
            return await ai.Execute(prompt, img);
        }
    }
}
