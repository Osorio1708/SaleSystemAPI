using SaleSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services.Contract
{
    public interface IProductService
    {
        Task<List<ProductDTO>> List();
        Task<ProductDTO> Create(ProductDTO model);
        Task<bool> Update(ProductDTO model);
        Task<bool> Delete(int id);
    }
}
