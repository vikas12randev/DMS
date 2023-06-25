namespace TT.Deliveries.Tests.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Moq;
    using NUnit.Framework;
    using TT.Deliveries.Application.Features.Delivery.Queries;
    using TT.Deliveries.Application.Handlers;
    using TT.Deliveries.Persistence.DataStore;
    using TT.Deliveries.Web.Api.Controllers;

    [TestFixture]
    public class DeliveryControllerTests
    {
        private FakeDataStore _fakeDataStore;
        private Mock<IMediator> _mockMediator;
        private DeliveriesController _deliveriesConttoller;

        public DeliveryControllerTests()
        {
            _fakeDataStore = new FakeDataStore();
            _mockMediator = new Mock<IMediator>();
            _deliveriesConttoller = new DeliveriesController( _mockMediator.Object);
        }

        [Test]
        public async Task GetByOrderId_Should_Return_404_If_Delivery_Doesnt_Exist()
        {
            //Arrange
            var handler = new GetDeliveryByOrderIdQueryHandler(_fakeDataStore);
            GetDeliveryByOrderIdQuery getDeliveryByOrderNumberQuery = new GetDeliveryByOrderIdQuery(100, CancellationToken.None);

            // Act
            try
            {
                await handler.Handle(getDeliveryByOrderNumberQuery, CancellationToken.None);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.That(ex.Message, Is.EqualTo($"Order Id 100 not found"));
            }
        }

        [Test]
        public async Task GetByOrderId_Should_Return_Delivery_Details()
        {
            //Arrange
            var handler = new GetDeliveryByOrderIdQueryHandler(_fakeDataStore);
            GetDeliveryByOrderIdQuery getDeliveryByOrderNumberQuery = new GetDeliveryByOrderIdQuery(1, CancellationToken.None);

            //Act
            var result = await handler.Handle(getDeliveryByOrderNumberQuery, CancellationToken.None);

            //Assert
            Assert.AreEqual(1, result.Order.OrderNumber);
        }
    }
}
