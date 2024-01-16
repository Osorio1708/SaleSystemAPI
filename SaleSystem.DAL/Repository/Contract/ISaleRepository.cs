using SaleSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DAL.Repository.Contract
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> Record(Sale model);
    }
}
