using SaleSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services.Contract
{
    public interface IAccountService
    {
        Task<List<AccountDTO>> List();
        Task<SessionDTO> CredentialValidation(string email, string password);
        Task<AccountDTO> Create(AccountDTO model);
        Task<bool> Update(AccountDTO model);
        Task<bool> Delete(int id);
    }
}
