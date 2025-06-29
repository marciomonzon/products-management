﻿using FluentValidation;
using ProductsManagement.Application.Persistence;
using ProductsManagement.Application.Services.ExternalServices;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Base;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Response;
using ProductsManagement.Application.UseCases.ProductUseCase.Interfaces;
using ProductsManagement.Application.UseCases.ProductUseCase.Mapping;

namespace ProductsManagement.Application.UseCases.ProductUseCase
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductRequest> _createValidator;
        private readonly IValidator<UpdateProductRequest> _updateValidator;
        private readonly IPublishEventService _publishService;

        public ProductUseCase(IProductRepository productRepository,
                              IValidator<CreateProductRequest> createValidator,
                              IValidator<UpdateProductRequest> updateValidator,
                              IPublishEventService publishService)
        {
            _productRepository = productRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _publishService = publishService;
        }

        public async Task<BaseResponse<int>> CreateAsync(CreateProductRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new BaseResponse<int>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = 0,
                    Errors = errors
                };
            }

            var product = ProductMapper.ToEntity(request);
            product.SetProductStatus();

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            _publishService.PostEventProductCreated(product.Id,
                                                    product.Name,
                                                    product.Description);

            return BaseResponse<int>.Ok(product.Id, "Product created successfully");
        }

        public async Task<BaseResponse<bool>> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return BaseResponse<bool>.Fail($"Product with id {id} not found.");

            _productRepository.Remove(product);
            await _productRepository.SaveChangesAsync();

            return BaseResponse<bool>.Ok(true, "Product deleted successfully");
        }

        public async Task<BaseResponse<IEnumerable<ProductResponse>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var responses = products.Select(ProductMapper.ToResponse);
            return BaseResponse<IEnumerable<ProductResponse>>.Ok(responses);
        }

        public async Task<BaseResponse<ProductResponse?>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return BaseResponse<ProductResponse?>.Fail($"Product with id {id} not found.");

            var response = ProductMapper.ToResponse(product);
            return BaseResponse<ProductResponse?>.Ok(response);
        }

        public async Task<BaseResponse<bool>> UpdateAsync(int id, UpdateProductRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new BaseResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Errors = errors
                };
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return BaseResponse<bool>.Fail($"Product with id {id} not found.");

            ProductMapper.MapUpdate(product, request);
            product.SetProductStatus();

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return BaseResponse<bool>.Ok(true, "Product updated successfully");
        }
    }
}
