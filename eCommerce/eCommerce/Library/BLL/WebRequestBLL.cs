using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Library.BLL
{
    public class WebRequestBLL
    {
        public HttpRequestMessage request;
        public WebRequestBLL() 
        { 
            request = new HttpRequestMessage(); 
        }

        public void AddHeader(string name, string value)
        {
            request.Headers.Add(name, value); 
        }

        public async Task<string?> Request(string url, HttpMethod httpMethod, string? body = null) 
        {
            string? result = null;         
            HttpResponseMessage? response = null;       

            using var client = new HttpClient();

            if (body is not  null)
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");

            request.RequestUri = new Uri(url, dontEscape: true);

            request.Method = httpMethod;
            
            response = await client.SendAsync(request);

            if (response is not null)
                result = await response.Content.ReadAsStringAsync();

            return result;
        }

    }
}
