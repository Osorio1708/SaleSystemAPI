using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleSystem.API.Utility;
using SaleSystem.BLL.Services;
using SaleSystem.BLL.Services.Contract;
using SaleSystem.DTO;

namespace SaleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var response = new Response<List<AccountDTO>>();
            try
            {
                response.status = true;
                response.value = await _accountService.List();
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("StartSession")]
        public async Task<IActionResult> StartSession([FromBody] LoginDTO login) 
        {
            var response = new Response<SessionDTO>();
            try
            {
                response.status = true;
                response.value = await _accountService.CredentialValidation(login.Email,login.Password);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] AccountDTO account)
        {
            var response = new Response<AccountDTO>();
            try
            {
                response.status = true;
                response.value = await _accountService.Create(account);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] AccountDTO account)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _accountService.Update(account);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _accountService.Delete(id);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }
    }
}
