using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ProductsManagement.API.Controllers;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Base;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Response;
using ProductsManagement.Application.UseCases.ProductUseCase.Interfaces;

namespace ProductControllerTests
{
    public class Product_Controller_Tests
    {
        private readonly IProductUseCase _useCase = Substitute.For<IProductUseCase>();
        private readonly ProductController _controller;

        public Product_Controller_Tests()
        {
            _controller = new ProductController(_useCase);
        }

        [Fact]
        public async Task Create_Should_Return_CreatedAtAction_When_Successful()
        {
            // Arrange
            var request = new CreateProductRequest { Name = "Product",
                                                     Price = 10,
                                                     Stock = 5,
                                                     CategoryId = 1 };

            _useCase.CreateAsync(request).Returns(BaseResponse<int>.Ok(1));

            // Act
            var result = await _controller.Create(request);

            // Assert
            var createdAt = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(1, ((BaseResponse<int>)createdAt.Value!).Data);
        }

        [Fact]
        public async Task Create_Should_Return_BadRequest_When_Failure()
        {
            // Arrange
            var request = new CreateProductRequest();
            _useCase.CreateAsync(request).Returns(BaseResponse<int>.Fail("Validation error"));

            // Act
            var result = await _controller.Create(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task Update_Should_Return_NoContent_When_Successful()
        {
            // Arrange
            var request = new UpdateProductRequest { Name = "Updated",
                                                     Price = 20,
                                                     Stock = 5,
                                                     CategoryId = 1 };

            _useCase.UpdateAsync(1, request).Returns(BaseResponse<bool>.Ok(true));

            // Act
            var result = await _controller.Update(1, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_Should_Return_BadRequest_When_Failure()
        {
            // Arrange
            var request = new UpdateProductRequest {  };
            _useCase.UpdateAsync(1, request).Returns(BaseResponse<bool>.Fail("Error"));

            // Act
            var result = await _controller.Update(1, request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task Delete_Should_Return_NoContent_When_Successful()
        {
            // Arrange
            _useCase.DeleteAsync(1).Returns(BaseResponse<bool>.Ok(true));

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_Should_Return_NotFound_When_Failure()
        {
            // Arrange
            _useCase.DeleteAsync(1).Returns(BaseResponse<bool>.Fail("Not found"));

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public async Task GetById_Should_Return_Ok_When_Found()
        {
            // Arrange
            var response = new ProductResponse { Id = 1, Name = "Product" };
            _useCase.GetByIdAsync(1).Returns(BaseResponse<ProductResponse>.Ok(response));

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task GetById_Should_Return_NotFound_When_Not_Found()
        {
            // Arrange
            _useCase.GetByIdAsync(1).Returns(BaseResponse<ProductResponse>.Fail("Not found"));

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFound.StatusCode);
        }

        [Fact]
        public async Task GetAll_Should_Return_Ok()
        {
            // Arrange
            var list = new List<ProductResponse> { new ProductResponse { Id = 1, Name = "P1" } };
            _useCase.GetAllAsync().Returns(BaseResponse<IEnumerable<ProductResponse>>.Ok(list));

            // Act
            var result = await _controller.GetAll();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ok.StatusCode);
        }
    }
}
