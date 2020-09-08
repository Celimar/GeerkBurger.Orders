using System.Collections.Generic;
using System.Threading.Tasks;
using GeekBurger.Order.Contracts;
using GeekBurger.OrderApi.Services;
using Microsoft.AspNetCore.Mvc;
using Models = GeekBurger.OrderApi.Models;

namespace GeekBurger.OrderApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var result = await Task.FromResult(_orderService.GetList());
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("pay")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<IActionResult> Pay(Payment payment)
        {
            try
            {
                _orderService.ReceivePayment(payment);
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
