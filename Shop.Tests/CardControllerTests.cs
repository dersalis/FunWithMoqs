using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shop.Controllers;
using Shop.Models;
using Shop.Services;

namespace Shop.Tests
{
    public class CardControllerTests
    {
        private CartController cartController;
        private Mock<ICartService> cardServiceMock;
        private Mock<IPaymentService> paymentServiceMock;
        private Mock<IShipmentService> shipmentServiceMock;
        private Mock<ICard> cardMock;
        private Mock<IAddressInfo> addressInfoMock;
        private Mock<ICartItem> cartItemMock;
        private IEnumerable<ICartItem> cartItems; 

        [SetUp]
        public void Setup()
        {
            cardServiceMock = new Mock<ICartService>();
            paymentServiceMock = new Mock<IPaymentService>();
            shipmentServiceMock = new Mock<IShipmentService>();
            addressInfoMock = new Mock<IAddressInfo>();
            cardMock = new Mock<ICard>();
            cartItemMock = new Mock<ICartItem>();
            

            cartItems = new List<ICartItem>()
            {
                cartItemMock.Object
            };
            cartItemMock.Setup(s => s.Price).Returns(10);
            cardServiceMock.Setup(s => s.Items()).Returns(cartItems.AsEnumerable());

            cartController = new CartController(cardServiceMock.Object, paymentServiceMock.Object, shipmentServiceMock.Object);
        }

        [Test]
        public void ShouldReturnCharged()
        {
            paymentServiceMock.Setup(s => s.Charge(It.IsAny<double>(), cardMock.Object)).Returns(true);
            var result = cartController.CheckOut(cardMock.Object, addressInfoMock.Object);

            shipmentServiceMock.Verify(v => v.Ship(addressInfoMock.Object, cartItems.AsEnumerable()), Times.Once());
            Assert.AreEqual("charged", result);
        }

        [Test]
        public void ShouldReturnNotCharged()
        {
            paymentServiceMock.Setup(s => s.Charge(It.IsAny<double>(), cardMock.Object)).Returns(false);
            var result = cartController.CheckOut(cardMock.Object, addressInfoMock.Object);

            shipmentServiceMock.Verify(v => v.Ship(addressInfoMock.Object, cartItems.AsEnumerable()), Times.Never());
            Assert.AreEqual("not charged", result);
        }
    }
}