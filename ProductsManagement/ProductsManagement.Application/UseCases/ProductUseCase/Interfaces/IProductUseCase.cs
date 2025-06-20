using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Base;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Response;

namespace ProductsManagement.Application.UseCases.ProductUseCase.Interfaces
{
    public interface IProductUseCase
    {
        Task<BaseResponse<int>> CreateAsync(CreateProductRequest request);
        Task<BaseResponse<bool>> UpdateAsync(int id, UpdateProductRequest request);
        Task<BaseResponse<bool>> DeleteAsync(int id);
        Task<BaseResponse<ProductResponse?>> GetByIdAsync(int id);
        Task<BaseResponse<IEnumerable<ProductResponse>>> GetAllAsync();
    }
}
