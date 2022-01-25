using Xunit;
using Moq;
using System.Collections.Generic;
using System;
using SogetiAssessment.Models;
using Microsoft.AspNetCore.Mvc;
using SogetiAssessment.DataServices;
using SogetiAssessment.Controllers;

namespace SogetiAssessmentTests
{
    public class ControllerUnitTests
    {
        [Fact]
        public async void TestCreateOrder()
        {
            // Arrange
            Order order = getDummyOrder();

            var mock = new Mock<OrderDataService>(MockBehavior.Loose, null);
            mock.Setup(x => x.Create(order))
                .ReturnsAsync(order);
            OrderController controller = new OrderController(mock.Object);

            // Act
            var createResult = await controller.CreateOrder(order);

            // Assert
            Assert.IsType<Order>(createResult.Value);
        }

        [Fact]
        public async void TestUpdateOrderShouldReturnNoContent()
        {
            // Arrange
            Order order = getDummyOrder();

            var mock = new Mock<OrderDataService>(MockBehavior.Loose, null);
            mock.Setup(x => x.Update(order))
                .ReturnsAsync(order);
            OrderController controller = new OrderController(mock.Object);

            // Act
            var updateResult = await controller.UpdateOrder(order.OrderId, order);

            // Assert
            Assert.IsType<NoContentResult>(updateResult);
        }

        [Fact]
        public async void TestUpdateOrderShouldReturnBadRequest()
        {
            // Arrange
            Order order = getDummyOrder();

            var mock = new Mock<OrderDataService>(MockBehavior.Loose, null);
            mock.Setup(x => x.Update(order))
                .ReturnsAsync(order);
            OrderController controller = new OrderController(mock.Object);

            // Act
            var updateResult = await controller.UpdateOrder(order.OrderId + 1, order);

            // Assert
            Assert.IsType<BadRequestResult>(updateResult);
        }

        [Fact]
        public async void TestUpdateOrderShouldReturnNotFound()
        {
            // Arrange
            Order order = getDummyOrder();

            var mock = new Mock<OrderDataService>(MockBehavior.Loose, null);
            mock.Setup(x => x.Update(order))
                .ReturnsAsync(() => null);
            OrderController controller = new OrderController(mock.Object);

            // Act
            var updateResult = await controller.UpdateOrder(order.OrderId, order);

            // Assert
            Assert.IsType<NotFoundResult>(updateResult);
        }

        [Fact]
        public async void TestCancelOrderShouldReturnNoContent()
        {
            // Arrange
            Order order = getDummyOrder();

            var mock = new Mock<OrderDataService>(MockBehavior.Loose, null);
            mock.Setup(x => x.Delete(order.OrderId))
                .ReturnsAsync(order);
            OrderController controller = new OrderController(mock.Object);

            // Act
            var cancelResult = await controller.CancelOrder(order.OrderId);

            // Assert
            Assert.IsType<NoContentResult>(cancelResult);
        }

        [Fact]
        public async void TestCancelOrderShouldReturnNotFound()
        {
            // Arrange
            Order order = getDummyOrder();

            var mock = new Mock<OrderDataService>(MockBehavior.Loose, null);
            mock.Setup(x => x.Update(order))
                .ReturnsAsync(() => null);
            OrderController controller = new OrderController(mock.Object);

            // Act
            var cancelResult = await controller.CancelOrder(order.OrderId);

            // Assert
            Assert.IsType<NotFoundResult>(cancelResult);
        }

        [Fact]
        public async void TestGetOrderByCustomerShouldReturnList()
        {
            // Arrange
            int customerId = 2;

            var x = new ActionResult<List<Order>>(getDummyOrderList());

            var mock = new Mock<OrderDataService>(MockBehavior.Loose, null);
            mock.Setup(x => x.GetByCustomer(customerId))
                .ReturnsAsync(getDummyOrderList());
            OrderController controller = new OrderController(mock.Object);

            // Act
            var getResult = await controller.GetOrdersByCustomer(customerId);

            // Assert
            Assert.IsType<ActionResult<List<Order>>>(getResult);
        }

        [Fact]
        public void TestConstructorShouldThrowNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderController(null));
        }

        private Order getDummyOrder()
        {
            return new Order
            {
                OrderId = 1,
                CustomerId = 1,
                Price = 2.00m
            };
        }

        private List<Order> getDummyOrderList()
        {
            List<Order> orderList = new List<Order>();
            orderList.Add(new Order { OrderId = 1, CustomerId = 1, Price = 2.00m });
            orderList.Add(new Order { OrderId = 5, CustomerId = 2, Price = 3.00m });
            orderList.Add(new Order { OrderId = 2, CustomerId = 2, Price = 5.00m });
            orderList.Add(new Order { OrderId = 3, CustomerId = 2, Price = 7.00m });
            orderList.Add(new Order { OrderId = 4, CustomerId = 2, Price = 880.00m });

            return orderList;
        }
    }
}