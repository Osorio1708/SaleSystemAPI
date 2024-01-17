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
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Route("Record")]
        public async Task<IActionResult> Record([FromBody] SaleDTO sale)
        {
            var response = new Response<SaleDTO>();
            try
            {
                response.status = true;
                response.value = await _saleService.Record(sale);

            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("History")]
        public async Task<IActionResult> History(string findFor, string? saleNumber, string? initDate, string? endDate)
        {
            var response = new Response<List<SaleDTO>>();
            saleNumber = saleNumber is null ? "" : saleNumber;
            initDate = initDate is null ? "" : initDate;
            endDate = endDate is null ? "" : endDate;
            try
            {
                response.status = true;
                response.value = await _saleService.History(findFor,saleNumber,initDate,endDate);
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> Report(string? initDate, string? endDate)
        {
            var response = new Response<List<ReportDTO>>();
            initDate = initDate is null ? "" : initDate;
            endDate = endDate is null ? "" : endDate;
            try
            {
                response.status = true;
                response.value = await _saleService.Report(initDate, endDate);
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
