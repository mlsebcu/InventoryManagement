
using AutoMapper;
using InventoryManagement.Business.Interfaces;
using InventoryManagement.Data.Entities;
using InventoryManagement.Data.Interfaces;
using InventoryManagement.Models.DTOs.Category;

namespace InventoryManagement.Business.Services
{
    public class CategoryService: ICategoryService
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync() {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return null;
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            category.IsActive = true;

            var createdCategory = await _categoryRepository.CreateAsync(category);

            return _mapper.Map<CategoryDto>(createdCategory);
        }
        public async Task UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }
            _mapper.Map(dto, category);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }
            await _categoryRepository.DeleteAsync(category);
        }
    }
}
