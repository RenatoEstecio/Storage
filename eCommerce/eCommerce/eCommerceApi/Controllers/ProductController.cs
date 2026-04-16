using Library.BLL;
using Library.DTO;
using Library.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerceApi.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : AuthController
    {       
        private readonly ProductService _productService;
       
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }       

        [HttpDelete]
        public async Task<IActionResult> DeleteItem([FromHeader] Guid Token, string? query)
        {
            try
            {
                Authorize();

                bool result = await _productService.DeleteByName(query);

                if (result)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (CustomException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }         
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? query)
        {
            try
            {                              
                List<ProductStore>? prods = await _productService.GetByQuery(query);
                
                if (prods == null)
                    return BadRequest("Nenhum Resultado Encontrado");

                return Ok(prods);
            }
            catch (CustomException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromHeader] Guid Token, IFormFile file)
        {
            try
            {
                Authorize();

                using var stream = file.OpenReadStream();

                ProductStore? prod = await _productService.AnalyzeProductImageAsync(stream, file.FileName, file.ContentType);
               
                if (prod is not null)
                    return Ok(prod);
                else
                    return BadRequest("Imagem não compatível");
            }
            catch (CustomException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }          
        }
    }
}
