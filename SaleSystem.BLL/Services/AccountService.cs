using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IGenericRepository<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountDTO> Create(AccountDTO model)
        {
            try
            {
                var createdAccount = await _accountRepository.Create(_mapper.Map<Account>(model));
                if(createdAccount.IdUser == 0) {
                    throw new TaskCanceledException("Account wasn't create");
                }
                var query = await _accountRepository.GetAll(a=>a.IdUser==createdAccount.IdUser);
                createdAccount = query.Include(role=>role.IdRoleNavigation).First();
                return _mapper.Map<AccountDTO>(createdAccount);
            }
            catch (Exception ex) {
                throw;
            }
        }

        public async Task<SessionDTO> CredentialValidation(string email, string password)
        {
            try
            {
                var queryAccount = await _accountRepository.GetAll(a=> a.Email == email && a.Password == password);
                if (queryAccount.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("Account doesnt exist");
                }
                Account account = queryAccount.Include(role => role.IdRoleNavigation).First();
                return _mapper.Map<SessionDTO>(account);
            }
            catch (Exception ex) {
                throw;
            } 
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foundAccount = await _accountRepository.Get(a => a.IdUser == id);
                if (foundAccount == null)
                {
                    throw new TaskCanceledException("Account doesnt exist");
                }
                bool response = await _accountRepository.Delete(foundAccount);
                if (response)
                {
                    throw new TaskCanceledException("It cannt be deleted");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<AccountDTO>> List()
        {
            try
            {
                var queryAccount = await _accountRepository.GetAll();
                var accountList = queryAccount.Include(x => x.IdRoleNavigation).ToList();
                return _mapper.Map<List<AccountDTO>>(accountList);
            }catch (Exception ex) {
                throw;
            }
        }

        public async Task<bool> Update(AccountDTO model)
        {
            try
            {
                var modelAccount = _mapper.Map<Account>(model);
                var foundAccount = await _accountRepository.Get(a=> a.IdUser == model.IdUser);
                if (foundAccount == null) {
                    throw new TaskCanceledException("Account doesnt exist");
                }
                foundAccount.FullName = modelAccount.FullName;
                foundAccount.Email = modelAccount.Email;
                foundAccount.IdRole = modelAccount.IdRole;
                foundAccount.Password = modelAccount.Password;
                foundAccount.IsActive = modelAccount.IsActive;
                bool response = await _accountRepository.Update(foundAccount);
                if (response)
                {
                    throw new TaskCanceledException("It cannt be updated");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
