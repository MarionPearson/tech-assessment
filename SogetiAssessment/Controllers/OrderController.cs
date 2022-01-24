using Microsoft.AspNetCore.Mvc;
using SogetiAssessment.Models;
using SogetiAssessment.DataServices;

namespace SogetiAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IDataService<Order> _orderDataService;

        public OrderController(IDataService<Order> orderDataService)
        {
            _orderDataService = orderDataService;
        }

        /// <summary>
        /// Routes to the Create method of the data service.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CreatedAtActionResult> CreateOrder(Order order)
        {
            var createResult = await _orderDataService.Create(order);
            return CreatedAtAction(nameof(CreateOrder), new { id = order.OrderId }, order);
        }

        /// <summary>
        /// Routes to the Update method of the data service.
        /// Returns NotFound (404) if the order id doesn't match an existing order.
        /// Returns BadRequest (400) if the order id supplied doesn't match the id on the order sent in.
        /// Returns NoContent (204) on success.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var updateResult = await _orderDataService.Update(order);
            if (updateResult == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Routes to the Delete method of the data service.
        /// Returns NotFound (404) if no order matches the id sent in.
        /// Returns NoContent(204) on success.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var cancelResult = await _orderDataService.Delete(id);
            if (cancelResult == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Returns a list of orders where the customer id of the order matches the supplied id (via the GetOrdersByCustomer method of the data service).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByCustomer(int id)
        {
            return await _orderDataService.GetByCustomer(id);
        }

    }
}