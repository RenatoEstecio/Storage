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
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ISqsSendService _sqsService;

        public OrderController(OrderService orderService, ISqsSendService sqsService)
        {
            _orderService = orderService;
            _sqsService = sqsService;
        }

      
        [HttpGet]
        public async Task<IActionResult> Get(string? query)
        {
            try
            {
                List<Order>? orders = await _orderService.GetByQuery(query);

                if (orders == null)
                    return BadRequest("Nenhum Resultado Encontrado");

                return Ok(orders);
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Order post)
        {
            try
            {
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
