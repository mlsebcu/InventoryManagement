using AutoMapper;
using InventoryManagement.Business.Interfaces;
using InventoryManagement.Data.Entities;
using InventoryManagement.Data.Interfaces;
using InventoryManagement.Models.DTOs.Product;

namespace InventoryManagement.Business.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product == null)
            {
                return null;
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var product = _mapper.Map<Product>(dto);
            product.CreatedDate = DateTime.UtcNow;
            product.IsActive = true;

            var created = await _productRepository.CreateAsync(product);

            return _mapper.Map<ProductDto>(created);
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product == null)
            {
                throw new Exception("Product not found");
            }

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            
            if(category == null)
            {
                throw new Exception("Category not found");
            }

            _mapper.Map(dto, product);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.DeleteAsync(product);
        }
    }
}
