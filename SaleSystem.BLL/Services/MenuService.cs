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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IGenericRepository<MenuRole> _menuRoleRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(
            IGenericRepository<Account> accountRepository, 
            IGenericRepository<MenuRole> menuRoleRepository, 
            IGenericRepository<Menu> menuRepository, 
            IMapper mapper
            )
        {
            _accountRepository = accountRepository;
            _menuRoleRepository = menuRoleRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> List(int idAccount)
        {
            IQueryable<Account> tbAccount = await _accountRepository.GetAll(a => a.IdUser == idAccount);
            IQueryable<MenuRole> tbMenuRole = await _menuRoleRepository.GetAll();
            IQueryable<Menu> tbMenu = await _menuRepository.GetAll();

            try { 
                IQueryable<Menu> tbResult = (from a in tbAccount
                                             join mr in tbMenuRole on a.IdRole equals mr.IdRole
                                             join m in tbMenu on mr.IdMenu equals m.IdMenu
                                             select m).AsQueryable();
                var menuList = tbResult.ToList();
                return _mapper.Map<List<MenuDTO>>( menuList );
            }
            catch {
                throw;
            }   
        }
    }
}
