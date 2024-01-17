using AutoMapper;
using SaleSystem.BLL.Services.Contract;
using SaleSystem.DAL.Repository.Contract;
using SaleSystem.DTO;
using SaleSystem.Model;
using System.Globalization;

namespace SaleSystem.BLL.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public DashBoardService(ISaleRepository saleRepository, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        private IQueryable<Sale> ReturnSales(IQueryable<Sale> saleTable, int subtractNumberDays) {
            DateTime? lastDate = saleTable.OrderByDescending(s=>s.RegistrationDate).Select(s=> s.RegistrationDate).First();
            lastDate = lastDate.Value.AddDays(subtractNumberDays);
            return saleTable.Where(s => s.RegistrationDate >= lastDate.Value.Date);
        }

        private async Task<int> LastWeekTotalSales()
        {
            int total = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAll();
            if (_saleQuery.Count() > 0) {
                var saleTable = ReturnSales(_saleQuery, -7);
                total = saleTable.Count();
            }
            return total;
        }

        private async Task<string> TotalRevenueLastWeek()
        {
            decimal result = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAll();
            if( _saleQuery.Count() > 0)
            {
                var saleTable = ReturnSales(_saleQuery, -7);
                result = saleTable.Select(s=>s.Total).Sum(s=>s.Value);
            } 
            return Convert.ToString(result, new CultureInfo("es-CO"));
        }

        private async Task<int> TotalProducts()
        {
            IQueryable <Product> _productsQuery = await _productRepository.GetAll();
            int total = _productsQuery.Count();
            return total;   
        }

        private async Task<Dictionary<string,int>> LastWeekSales()
        {
            Dictionary<string,int> result = new Dictionary<string,int>();
            IQueryable<Sale> _saleQuery = await _saleRepository.GetAll();
            if (_saleQuery.Count()>0)
            {
                var saleTable = ReturnSales(_saleQuery, -7);
                result = saleTable.GroupBy(s => s.RegistrationDate.Value.Date).OrderBy(g => g.Key).Select(ds => new { date = ds.Key.ToString("dd/MM/yyyy"), total = ds.Count() }).ToDictionary(keySelector: r => r.date, elementSelector: r => r.total);

            }
            return result;
        }

        public async Task<DashboardDTO> Resume()
        {
            DashboardDTO vmDashBoard = new DashboardDTO();
            try
            {
                vmDashBoard.TotalSales = await LastWeekTotalSales();
                vmDashBoard.TotalRevenue = await TotalRevenueLastWeek();
                vmDashBoard.TotalProducts = await TotalProducts();
                List<WeekSalesDTO> weekSaleList = new List<WeekSalesDTO>(); 
                foreach(KeyValuePair<string,int> item in await LastWeekSales())
                {
                    weekSaleList.Add(new WeekSalesDTO()
                    {
                        Date = item.Key,
                        Total = item.Value,
                    });

                }
                vmDashBoard.LastWeekSales = weekSaleList;
            }catch (Exception ex) { throw; }
            return vmDashBoard; 
        }
    }
}
