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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List(int accountId)
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                response.status = true;
                response.value = await _menuService.List(accountId);
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
