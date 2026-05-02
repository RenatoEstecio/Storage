using eCommerceApi.Controllers;
using Library.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests
{
    [Fact]
    public async Task Delete_DeveRetornar401_QuandoTokenInvalido()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        var storageMock = new Mock<IImageStorageService>();
        var aiMock = new Mock<IProductAIService>();

        var serviceMock = new Mock<ProductService>(
            repoMock.Object,
            storageMock.Object,
            aiMock.Object
        );

        var controller = new ProductController(serviceMock.Object);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Token"] = "TOKEN_ERRADO";

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        // Act
        var result = await controller.DeleteItem(Guid.NewGuid(), "teste");

        // Assert
        var obj = Assert.IsType<ObjectResult>(result);
       
        Assert.Equal(401, obj.StatusCode);       
    }

    [Fact]
    public async Task Delete_DeveRetornar400_QuandoTokenValido_E_DeleteFalha()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        var storageMock = new Mock<IImageStorageService>();
        var aiMock = new Mock<IProductAIService>();

        var serviceMock = new Mock<ProductService>(
            repoMock.Object,
            storageMock.Object,
            aiMock.Object
        );

        

        var controller = new ProductController(serviceMock.Object);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Token"] = Auth.TOKEN().ToString().ToUpper();

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        // Act
        var result = await controller.DeleteItem(Guid.NewGuid(), "teste");

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}