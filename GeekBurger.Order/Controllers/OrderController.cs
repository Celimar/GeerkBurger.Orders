using System;
using System.Threading.Tasks;
using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Service;
using GeekBurger.Order.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Order.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("orders")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var result = await _orderService.GetList();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("orders/changed")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<ActionResult> GetOrdersChanged()
        {
            try
            {
                var result = await _orderService.GetOrderChangedEventList();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{storeName}", Name = "orders/by-store-name")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<ActionResult> GetOrdersByStoreName(string storeName)
        {
            try
            {
                var result = await _orderService.GetOrderChangedEventList();
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
        public async Task<ActionResult> Pay([FromBody] Payment payment)
        {
            if (payment == null)
                return StatusCode(500);

            if (payment.OrderId <= 0)
                return StatusCode(500);

            Model.Payment newPayment = _mapper.Map<Model.Payment>(payment);

            if (newPayment.StoreId == Guid.Empty)
                return new Helper.UnprocessableEntityResult(ModelState);

            await _orderService.AddPayment(payment);

            return StatusCode(200);
        }

        [HttpPost("neworder")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<ActionResult> NewOrderForTest([FromBody] NewOrder newOrder)
        {
            if (newOrder == null)
                return StatusCode(500);

            if (newOrder.OrderId <= 0)
                return StatusCode(500);

            if (String.IsNullOrEmpty(newOrder.StoreName))
                return StatusCode(500);

            Model.Order order = _mapper.Map<Model.Order>(newOrder);

            if (order.Store == null)
                return new Helper.UnprocessableEntityResult(ModelState);

            if (order.Store.StoreId == Guid.Empty)
                return new Helper.UnprocessableEntityResult(ModelState);

            await _orderService.Insert(order);

            return StatusCode(200);
        }
    }
}
