using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using recruit_dotnetframework.ApiClients;
using recruit_dotnetframework.Controllers;
using recruit_dotnetframework.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Services.Description;

namespace recruit_dotnetframework_test
{
    [TestClass]
    public class CreditCardControllerTests
    {
        private static CreditCardController CreateMockController(Mock<IExternalApiClient> mockApiClient)
        {
    

            var config = new HttpConfiguration(); // Create a new HttpConfiguration
            var request = new HttpRequestMessage();
            var controller = new CreditCardController(mockApiClient.Object)
            {
                Configuration = config,
                Request = request
            };
            return controller;
        }
        [TestMethod]
        public async Task PostCardInformationTest_Success()
        {
            //arrange
            var creditCard = new CreditCard
            {
                CardNumber = "123",
                CVC = "123",
                ExpiryMonth = 12,
                ExpiryYear = 24,
            };
            var MockValidationResponse = new ValidationResponse { IsValid = true, Message = "Card validated successfully" };

            var mockApiClient = new Mock<IExternalApiClient>();
            mockApiClient.Setup(x => x.ValidateCreditCardInformation(It.IsAny<CreditCard>()))
                .ReturnsAsync(MockValidationResponse);
            var controller = CreateMockController(mockApiClient);
            //act
            var result = await controller.PostCardInformation(creditCard);
            var response  = await result.ExecuteAsync(CancellationToken.None);
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.TryGetContentValue(out ValidationResponse validationResponse));
            Assert.AreEqual(validationResponse,MockValidationResponse);
        }

        [TestMethod]
        public async Task PostCardInformationTest_Fail()
        {
            //arrange
            var creditCard = new CreditCard
            {
                CardNumber = "123",
                CVC = "123",
                ExpiryMonth = 12,
                ExpiryYear = 24,
            };
            var MockValidationResponse = new ValidationResponse { IsValid = true, Message = "Card validated successfully" };

            var mockApiClient = new Mock<IExternalApiClient>();
            mockApiClient.Setup(x => x.ValidateCreditCardInformation(It.IsAny<CreditCard>()))
                .ThrowsAsync(new ArgumentException("Invalid Date"));
            var controller = CreateMockController(mockApiClient);
            //act
            var result = await controller.PostCardInformation(creditCard);
            var response = await result.ExecuteAsync(CancellationToken.None);
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
