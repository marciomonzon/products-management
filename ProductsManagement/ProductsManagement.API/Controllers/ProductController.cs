using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Application.UseCases.ProductUseCase.Interfaces;

namespace ProductsManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductUseCase _productUseCase;

        public ProductController(IProductUseCase productUseCase)
        {
            _productUseCase = productUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var id = await _productUseCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            await _productUseCase.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productUseCase.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productUseCase.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productUseCase.GetAllAsync();
            return Ok(products);
        }
    }
}
