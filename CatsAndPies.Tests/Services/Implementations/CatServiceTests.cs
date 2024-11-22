using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.DTO.Response.Cat;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Factories;
using CatsAndPies.Domain.Models.Cat;
using CatsAndPies.Services.Implementations;
using Moq; // Для создания моков
using Xunit; // xUnit framework

namespace CatsAndPies.Tests.Services.Implementations
{
    public class CatServiceTests
    {
        [Fact]
        public async Task CreateCat_ShouldReturnSuccessResult_WhenCatDoesNotExist()
        {
            // Arrange
            var userId = 1;
            var name = "Fluffy";
            var colorId = 2;
            var personalityId = 3;

            var mockRepository = new Mock<ICatRepository>();
            mockRepository.SetupSequence(r => r.GetOneByUserId(userId))
                          .ReturnsAsync((CatEntity?)null); // Проверка, что кот отсутствует
                          //.ReturnsAsync(new CatEntity { Name = name, ColorId = colorId, PersonalityId = personalityId }); // После добавления

            mockRepository.Setup(r => r.GetRandomColorAndPersonality())
                          .ReturnsAsync((colorId, personalityId));
            mockRepository.Setup(r => r.Add(It.IsAny<CatEntity>()))
                          .Returns(Task.CompletedTask);

            var mockFactory = new Mock<CatFactory>();
            var mockCat = new Mock<Cat>();
            //mockCat.Setup(c => c.SayHelloToNewOwner()).Returns("Meow!");
            //mockFactory.Setup(f => f.CreateCatWithCertainBehavior(personalityId))
            //           .Returns(mockCat.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<CatResponseDTO>(It.IsAny<CatEntity>()))
                      .Returns(new CatResponseDTO { Name = name, Phrase = "Meow!" });

            var service = new CatService(mockRepository.Object, mockFactory.Object, mockMapper.Object);

            // Act
            var result = await service.TryCreateCat(name, userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            //Assert.Equal(name, result.Data.Name);
            //Assert.Equal("Meow!", result.Data.Phrase);

            mockRepository.Verify(r => r.GetOneByUserId(userId), Times.Exactly(2)); // Два вызова: до и после добавления
            mockRepository.Verify(r => r.GetRandomColorAndPersonality(), Times.Once);
            mockRepository.Verify(r => r.Add(It.IsAny<CatEntity>()), Times.Once);
            mockFactory.Verify(f => f.CreateCatWithCertainBehavior(personalityId), Times.Once);
            mockMapper.Verify(m => m.Map<CatResponseDTO>(It.IsAny<CatEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateCat_ShouldReturnError_WhenUserAlreadyHasCat()
        {
            // Arrange
            var userId = 1;
            var existingCatEntity = new CatEntity { Id = 1, UserId = userId };

            var mockRepository = new Mock<ICatRepository>();
            mockRepository.Setup(r => r.GetOneByUserId(userId))
                          .ReturnsAsync(existingCatEntity);

            var mockFactory = new Mock<CatFactory>();
            var mockMapper = new Mock<IMapper>();

            var service = new CatService(mockRepository.Object, mockFactory.Object, mockMapper.Object);

            // Act
            var result = await service.TryCreateCat("NewCat", userId);

            // Assert
            Assert.False(result.IsSuccess);
            mockRepository.Verify(r => r.GetOneByUserId(userId), Times.Once);
            mockRepository.Verify(r => r.Add(It.IsAny<CatEntity>()), Times.Never);
        }
       

        [Fact]
        public async Task SaySomething_ShouldReturnMessage_WhenCatExists()
        {
            // Arrange
            var userId = 1;
            var personalityId = 3;
            var expectedMessage = "Meow!";

            var mockRepository = new Mock<ICatRepository>();
            mockRepository.Setup(r => r.GetCatBehaviorByUserId(userId))
                          .ReturnsAsync(personalityId);

            var mockFactory = new Mock<CatFactory>();
            var mockCat = new Mock<Cat>(MockBehavior.Strict, new Mock<PersonalityBehavior>().Object);
            mockCat.Setup(c => c.SaySomething())
                   .Returns(expectedMessage);

            mockFactory.Setup(f => f.CreateCatWithCertainBehavior(personalityId))
                       .Returns(mockCat.Object);

            var service = new CatService(mockRepository.Object, mockFactory.Object, Mock.Of<IMapper>());

            // Act
            var result = await service.TrySaySomething(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Data);

            mockRepository.Verify(r => r.GetCatBehaviorByUserId(userId), Times.Once);
            mockFactory.Verify(f => f.CreateCatWithCertainBehavior(personalityId), Times.Once);
        }

    }
}
