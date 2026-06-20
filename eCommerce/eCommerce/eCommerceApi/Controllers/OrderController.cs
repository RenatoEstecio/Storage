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
    public class OrderController : AuthController
    {
        private readonly OrderService _orderService;
        private readonly ISqsSendService _sqsService;

        public OrderController(OrderService orderService, ISqsSendService sqsService)
        {
            _orderService = orderService;
            _sqsService = sqsService;
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteItem([FromHeader] Guid Token, string? query)
        {
            try
            {
                Authorize();

                bool result = await _orderService.DeleteByName(query);

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
              
                return Ok();
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
        public async Task<IActionResult> Create([FromHeader] Guid Token, Order post)
        {
            try
            {
                Authorize();

                await _sqsService.SendMessageAsync(post);

                return Ok();
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
