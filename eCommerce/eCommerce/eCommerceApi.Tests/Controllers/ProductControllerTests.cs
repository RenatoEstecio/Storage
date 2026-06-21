using eCommerceApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Delete_DeveRetornarOk_QuandoDeleteFunciona()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        var storageMock = new Mock<IImageStorageService>();
        var aiMock = new Mock<IProductAIService>();

        repoMock.Setup(r => r.DeleteByNameAsync("teste")).ReturnsAsync(true);

        var serviceMock = new Mock<ProductService>(
            repoMock.Object,
            storageMock.Object,
            aiMock.Object
        );

        var controller = new ProductController(serviceMock.Object);

        // Act
        var result = await controller.DeleteItem("teste");

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Delete_DeveRetornar400_QuandoDeleteFalha()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        var storageMock = new Mock<IImageStorageService>();
        var aiMock = new Mock<IProductAIService>();

        repoMock.Setup(r => r.DeleteByNameAsync("teste")).ReturnsAsync(false);

        var serviceMock = new Mock<ProductService>(
            repoMock.Object,
            storageMock.Object,
            aiMock.Object
        );

        var controller = new ProductController(serviceMock.Object);

        // Act
        var result = await controller.DeleteItem("teste");

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}
