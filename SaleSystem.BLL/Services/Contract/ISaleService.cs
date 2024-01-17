using SaleSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services.Contract
{
    public interface ISaleService
    {
        Task<SaleDTO> Record(SaleDTO model);
        Task<List<SaleDTO>> History(string findFor, string saleNumber, string initDate, string endDate);
        Task<List<ReportDTO>> Report(string initDate, string endDate);
    }
}
