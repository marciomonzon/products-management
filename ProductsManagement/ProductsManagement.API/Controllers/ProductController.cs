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
            var result = await _productUseCase.CreateAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var result = await _productUseCase.UpdateAsync(id, request);

            if (!result.Success)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productUseCase.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productUseCase.GetByIdAsync(id);

            if (!result.Success || result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productUseCase.GetAllAsync();
            return Ok(result);
        }
    }
}
