using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using ProductsManagement.Application.Persistence;
using ProductsManagement.Application.UseCases.ProductUseCase;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Domain.Entities;

namespace UseCaseTests
{
    public class Update_Product_Tests
    {
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly IValidator<UpdateProductRequest> _validator = Substitute.For<IValidator<UpdateProductRequest>>();
        private readonly ProductUseCase _useCase;

        public Update_Product_Tests()
        {
            _useCase = new ProductUseCase(_repository, 
                                          Substitute.For<IValidator<CreateProductRequest>>(),
                                          _validator);
        }

        [Fact]
        public async Task Should_Update_Product_When_Request_Is_Valid()
        {
            // Arrange
            var request = new UpdateProductRequest
            {
                Name = "Updated Product",
                Price = 20.0m,
                Description = "Updated description",
                CategoryId = 2,
                Stock = 10
            };

            _validator.ValidateAsync(request).Returns(new ValidationResult());

            var product = new Product();
            product.Create(request.Name,
                           request.Description,
                           request.Price,
                           request.CategoryId,
                           request.Stock);

            _repository.GetByIdAsync(1).Returns(product);

            // Act
            var result = await _useCase.UpdateAsync(1, request);

            // Assert
            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Updated Product", product.Name);
            Assert.Equal(20.0m, product.Price);
            Assert.Equal("Updated description", product.Description);
            Assert.Equal(2, product.CategoryId);
            Assert.Equal(10, product.Stock);
        }

        [Fact]
        public async Task Should_Return_Validation_Errors()
        {
            // Arrange
            var request = new UpdateProductRequest
            {
                Name = "",
                Price = -5,
                Description = "",
                CategoryId = 0,
                Stock = -1
            };

            var validationResult = new ValidationResult(new[]
            {
            new ValidationFailure("Name", "Name is required"),
            new ValidationFailure("Price", "Invalid price"),
            new ValidationFailure("Stock", "Invalid stock")
        });

            _validator.ValidateAsync(request).Returns(validationResult);

            // Act
            var result = await _useCase.UpdateAsync(1, request);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Name is required", result.Errors);
            Assert.Contains("Invalid price", result.Errors);
            Assert.Contains("Invalid stock", result.Errors);
        }

        [Fact]
        public async Task Should_Return_Error_If_Product_Not_Found()
        {
            // Arrange
            var request = new UpdateProductRequest
            {
                Name = "Test",
                Price = 10,
                Description = "",
                CategoryId = 1,
                Stock = 5
            };

            _validator.ValidateAsync(request).Returns(new ValidationResult());
            _repository.GetByIdAsync(1).Returns((Product?)null);

            // Act
            var result = await _useCase.UpdateAsync(1, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Product with id 1 not found.", result.Message);
        }
    }
}
