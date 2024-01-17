using SaleSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services.Contract
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetAllRoles();
    }
}
