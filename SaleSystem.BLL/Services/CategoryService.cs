using AutoMapper;
using SaleSystem.BLL.Services.Contract;
using SaleSystem.DAL.Repository.Contract;
using SaleSystem.DTO;
using SaleSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> List()
        {
            try
            {
                var categoryList = await _categoryRepository.GetAll();
                return _mapper.Map<List<CategoryDTO>>(categoryList.ToList());
            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
