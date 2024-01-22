using APIproject.DTOs;
using APIproject.Entittes;
using APIproject.Repositories.Interface;
using APIproject.Services.Interface;
using AutoMapper;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IQueryable<Category>> GetAll()
    {
        return await _categoryRepository.GetAll();
    }

    public async Task<Category> GetById(int id)
    {
        if (id <= 0) throw new ArgumentException("Invalid ID");

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null) throw new ArgumentException("Category not found");

        return category;
    }

    public async Task Create(CreateCategoryDTO createCategoryDto)
    {
        if (createCategoryDto == null) throw new ArgumentException("Invalid input");

     
        Category category=_mapper.Map<Category>(createCategoryDto);

        await _categoryRepository.Create(category);
    }

    public async Task Update(int id, UpdateCategoryDTO updateCategoryDto)
    {
        if (id <= 0 || updateCategoryDto == null)
        {
            throw new ArgumentException("Invalid parameters");
        }

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            throw new ArgumentException("Category not found");
        }

        _mapper.Map(updateCategoryDto, category);

        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();
    }


    public async Task Delete(int id)
    {
        if (id <= 0) throw new ArgumentException("Invalid ID");

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null) throw new ArgumentException("Category not found");

         _categoryRepository.Delete(category);
    }
}
