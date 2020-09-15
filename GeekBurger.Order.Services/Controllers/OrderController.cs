﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using GeekBurger.Order.Contracts;
using GeekBurger.Order.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Order.Services.Controllers
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

        [HttpGet("orders/changed")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetOrdersChanged()
        {
            try
            {
                var result = await Task.FromResult(_orderService.GetOrderChangedEventList());
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
        public async Task<IActionResult> GetOrdersByStoreName(string storeName)
        {
            try
            {
                var result = await Task.FromResult(_orderService.GetOrderChangedEventList());
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
        public async Task<IActionResult> Pay([FromBody] Payment payment)
        {
            if (payment == null)
                return StatusCode(500);

            if (payment.OrderId <= 0)
                return StatusCode(500);

            Model.Payment newPayment = _mapper.Map<Model.Payment>(payment);

            if (newPayment.StoreId == Guid.Empty)
                return new Helper.UnprocessableEntityResult(ModelState);

            _orderService.AddPayment(payment);

            return StatusCode(200);

        }
    }
}
