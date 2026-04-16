using Library.Util;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerceApi.Controllers
{
    public class AuthController : ControllerBase
    {
        Guid TOKEN = new Guid("0790A233-6B43-43EE-BD60-63538029A819");

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool Authorize()
        {
            var token = Request.Headers["Token"].FirstOrDefault();

            if (!string.IsNullOrEmpty(token) && TOKEN.ToString().ToUpper() == token)
                return true;
            else
                throw new CustomException("Falha ao autenticar", HttpStatusCode.Unauthorized);
        }
    }
}
