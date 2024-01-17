using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaleSystem.BLL.Services.Contract;
using SaleSystem.DAL.Repository.Contract;
using SaleSystem.DTO;
using SaleSystem.Model;

namespace SaleSystem.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO model)
        {
            try
            {
                var createdProduct = await _productRepository.Create(_mapper.Map<Product>(model));
                if (createdProduct.IdProduct == 0) {
                throw new TaskCanceledException("It cannt be created");
                }
                return _mapper.Map<ProductDTO>(createdProduct);
            }catch(Exception ex) { throw; }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foundProduct = await _productRepository.Get(p => p.IdProduct == id);
                if (foundProduct == null) {
                    throw new TaskCanceledException("Product doesnt exist");
                }
                bool response = await _productRepository.Delete(foundProduct);
                if(!response) {
                    throw new TaskCanceledException("It cannt be deleted");
                }
                return response;

            }
            catch (Exception ex) { throw; }
        }

        public async Task<List<ProductDTO>> List()
        {
            try
            {
                var queryProduct = await _productRepository.GetAll();
                var productList = queryProduct.Include(cat => cat.IdCategoryNavigation).ToList();
                return _mapper.Map<List<ProductDTO>>(productList.ToList());  
            }
            catch (Exception ex) { throw; }
        }

        public async Task<bool> Update(ProductDTO model)
        {
            try
            {
                var productModel = _mapper.Map<Product>(model);
                var foundProduct = await _productRepository.Get(p=>p.IdProduct == productModel.IdProduct);
                if (foundProduct == null) {
                    throw new TaskCanceledException("Product doesnt exist");
                }
                foundProduct.Name = productModel.Name;
                foundProduct.IdCategory = productModel.IdCategory;
                foundProduct.Stock = productModel.Stock;
                foundProduct.Price = productModel.Price;
                foundProduct.IsActive = productModel.IsActive;
                bool response = await _productRepository.Update(foundProduct);
                if (!response) {
                    throw new TaskCanceledException("Cannt be updated");
                }
                return response;    
            }
            catch (Exception ex) { throw; }
        }
    }
}
