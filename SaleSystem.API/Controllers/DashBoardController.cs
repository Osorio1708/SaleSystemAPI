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
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpGet]
        [Route("Resume")]
        public async Task<IActionResult> Resume()
        {
            var response = new Response<DashboardDTO>();
            try
            {
                response.status = true;
                response.value = await _dashBoardService.Resume();
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
