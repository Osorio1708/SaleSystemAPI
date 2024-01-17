using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaleSystem.BLL.Services.Contract;
using SaleSystem.DAL.Repository.Contract;
using SaleSystem.DTO;
using SaleSystem.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IGenericRepository<SaleDetail> saleDetailRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleDTO>> History(string findFor, string saleNumber, string initDate, string endDate)
        {
            IQueryable<Sale> query = await _saleRepository.GetAll();
            var resultList = new List<Sale>();
            try
            {
                if(findFor == "date")
                {
                    DateTime formatedInitDate = DateTime.ParseExact(initDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                    DateTime formatedEndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                    resultList = await query
                        .Where(
                            s =>
                                s.RegistrationDate.Value.Date >= formatedInitDate &&
                                s.RegistrationDate.Value.Date <= formatedEndDate
                        )
                        .Include(
                            ds => 
                                ds.SaleDetails)
                        .ThenInclude(
                            p =>
                                p.IdProductNavigation
                        ).ToListAsync();
                }
                else
                {
                    resultList = await query
                        .Where(
                            s =>
                                s.DocumentNumber == saleNumber
                        )
                        .Include(
                            ds =>
                                ds.SaleDetails)
                        .ThenInclude(
                            p =>
                                p.IdProductNavigation
                        ).ToListAsync();
                }

            }catch (Exception ex)
            {
                throw;
            }

            return _mapper.Map<List<SaleDTO>>(resultList); 
        }

        public async Task<SaleDTO> Record(SaleDTO model)
        {
            try
            {
                var generatedSale = await _saleRepository.Record(_mapper.Map<Sale>(model));
                if (generatedSale.IdSale == 0) {
                    throw new TaskCanceledException("Cannt be created");
                }
                return _mapper.Map<SaleDTO>(generatedSale);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ReportDTO>> Report(string initDate, string endDate)
        {
            IQueryable<SaleDetail> query = await _saleDetailRepository.GetAll();
            var resultList = new List<SaleDetail>();
            try
            {
                DateTime formatedInitDate = DateTime.ParseExact(initDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                DateTime formatedEndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                resultList = await query
                    .Include(p=>p.IdProductNavigation)
                    .Include(s=>s.IdSaleNavigation)
                    .Where(ds=>ds.IdSaleNavigation.RegistrationDate.Value.Date>= formatedInitDate&& ds.IdSaleNavigation.RegistrationDate.Value.Date<= formatedEndDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return _mapper.Map<List<ReportDTO>>(resultList);
        }
    }
}
