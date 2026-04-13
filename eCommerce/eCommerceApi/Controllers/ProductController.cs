using Library.BLL;
using Library.DTO;
using Library.Util;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerceApi.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase  
    {
        private readonly ProductBLL _productBLL;
        Guid TOKEN = new Guid("0790A233-6B43-43EE-BD60-63538029A819");

        public ProductController(ProductBLL productBLL)
        {
            _productBLL = productBLL;
        }

        [HttpPost]
        public async Task<IActionResult> Item([FromHeader] Guid Token, Product product)
        {
            try
            {
                if (Token != TOKEN)
                    return Unauthorized("Falha ao autenticar");
               
               

                ProductStore? prod = await _productBLL.Verify(product);

                if (prod == null)
                    return BadRequest("Imagem não compatível");

                return Ok(prod);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteItem([FromHeader] Guid Token, string? query)
        {
            try
            {
                if (Token != TOKEN)
                    return Unauthorized("Falha ao autenticar");
             
                bool result = await _productBLL.DeleteByName(query);

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
        public async Task<IActionResult> Item(string? query)
        {
            try
            {                
                
                List<ProductStore>? prods = await _productBLL.GetByQuery(query);
                
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

        [HttpPost("File")]
        public async Task<IActionResult> Upload([FromHeader] Guid Token, IFormFile file)
        {
            try
            {
                if (Token != TOKEN)
                    return Unauthorized("Falha ao autenticar");

                using var stream = file.OpenReadStream();
               
                ProductStore? prod = await _productBLL.Verify(stream, file.FileName, file.ContentType);
           
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
