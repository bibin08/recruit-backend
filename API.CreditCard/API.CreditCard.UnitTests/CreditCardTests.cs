using API.CreditCard.CreditCard;
using API.CreditCard.CreditCard.Models;
using API.CreditCard.Database;
using API.CreditCard.Helper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CreditCard.UnitTests
{
    public class Tests
    {
        private Mock<ICreditCardRepository> _creditCardRepositoryMock { get; set; }
        private Mock<ITokenisationService> _tokenisationServiceMock { get; set; }


        [SetUp]
        public void Setup()
        {
            _creditCardRepositoryMock = new Mock<ICreditCardRepository>();
            _tokenisationServiceMock = new Mock<ITokenisationService>();

            var creditCardDtos = new List<CreditCardDto>
            {
                new CreditCardDto()
                {
                    Name = "Test1",
                    CardNumber = "4124255442254",
                    CVC = "425",
                    CardId = Guid.NewGuid(),
                    ExpiryDate = DateTime.Now.AddDays(10)
                },
                new CreditCardDto()
                {
                    Name = "Test2",
                    CardNumber = "4124245541552524",
                    CVC = "423",
                    CardId = Guid.NewGuid(),
                    ExpiryDate = DateTime.Now.AddDays(15)
                }
            };

            _creditCardRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(creditCardDtos);
            _tokenisationServiceMock.Setup(x => x.Decrypt(It.IsAny<string>())).Returns<string>(x => x);

            _creditCardRepositoryMock.Setup(x => x.GetByQuery(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(creditCardDtos.First());
        }

        [Test]
        public async Task GetAllMethodReturnsCreditCardDetails()
        {
            var creditCardService = new CreditCardService(_creditCardRepositoryMock.Object, _tokenisationServiceMock.Object);
            var creditCards = await creditCardService.GetAll();
            Assert.AreEqual(creditCards.Count(), 2);
        }

        [Test]
        public async Task GetMethodByNameAndCardNumberReturnsCreditCardDetails()
        {
            var creditCardService = new CreditCardService(_creditCardRepositoryMock.Object, _tokenisationServiceMock.Object);
            var creditCard = await creditCardService.GetByQuery("Test1", "4124255442254");
            Assert.AreEqual(creditCard.Name, "Test1");
            Assert.AreEqual(creditCard.CardNumber, "4124255442254");
        }

        //TODO: Add more unit tests
    }
}