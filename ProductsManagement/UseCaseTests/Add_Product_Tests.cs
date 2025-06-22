using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using ProductsManagement.Application.Persistence;
using ProductsManagement.Application.Services.ExternalServices;
using ProductsManagement.Application.UseCases.ProductUseCase;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Domain.Entities;

namespace UseCaseTests
{
    public class Add_Product_Tests
    {
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly IPublishEventService _publishEvent = Substitute.For<IPublishEventService>();
        private readonly IValidator<CreateProductRequest> _validator = Substitute.For<IValidator<CreateProductRequest>>();
        private readonly ProductUseCase _useCase;

        public Add_Product_Tests()
        {
            _useCase = new ProductUseCase(_repository,
                                          _validator,
                                          Substitute.For<IValidator<UpdateProductRequest>>(),
                                          _publishEvent);
        }

        [Fact]
        public async Task Should_Add_Product_When_Request_Is_Valid()
        {
            // Arrange
            var request = new CreateProductRequest
            {
                Name = "Test Product",
                Price = 10.0m,
                Description = "Sample description",
                CategoryId = 1,
                Stock = 5
            };

            _validator.ValidateAsync(request).Returns(Task.FromResult(new ValidationResult()));

            Product addedProduct = null!;
            await _repository.AddAsync(Arg.Do<Product>(p => addedProduct = p));

            // Act
            var result = await _useCase.CreateAsync(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Product created successfully", result.Message);

            Assert.Equal("Test Product", addedProduct.Name);
            Assert.Equal(10.0m, addedProduct.Price);
            Assert.Equal("Sample description", addedProduct.Description);
            Assert.Equal(1, addedProduct.CategoryId);
            Assert.Equal(5, addedProduct.Stock);
        }

        [Fact]
        public async Task Should_Return_Errors_When_Request_Is_Invalid()
        {
            // Arrange
            var request = new CreateProductRequest();

            var validationErrors = new ValidationResult(new[]
            {
                new ValidationFailure("Name", "Name is required"),
                new ValidationFailure("Price", "Price must be greater than zero")
            });

            _validator.ValidateAsync(request).Returns(Task.FromResult(validationErrors));

            // Act
            var result = await _useCase.CreateAsync(request);

            // Assert
            Assert.False(result.Success);
            Assert.True(result.Errors.Contains("Name is required"));
            Assert.True(result.Errors.Contains("Price must be greater than zero"));
        }
    }
}
