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
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> GetAllRoles()
        {
            try
            {
                var roleList = await _roleRepository.GetAll();
                return _mapper.Map<List<RoleDTO>>(roleList.ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
