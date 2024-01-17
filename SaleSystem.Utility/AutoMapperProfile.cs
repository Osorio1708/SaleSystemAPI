using AutoMapper;
using SaleSystem.DTO;
using SaleSystem.Model;
using System.Globalization;

namespace SaleSystem.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Role
            CreateMap<Role, RoleDTO>().ReverseMap();
            #endregion Role

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Account
            CreateMap<Account, AccountDTO>()
                .ForMember(
                destiny => destiny.RoleDescription, 
                opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name))
                .ForMember(
                destiny => destiny.IsActive, 
                opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );
            CreateMap<Account, SessionDTO>()
                .ForMember(
                destiny => destiny.RoleDescription, 
                opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name)
                );
            CreateMap<AccountDTO, Account>()
                .ForMember(
                destiny => destiny.IdRoleNavigation, 
                opt => opt.Ignore()
                )
                .ForMember(
                destiny => destiny.IsActive, 
                opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );
            #endregion Account

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion Category

            #region Product
            CreateMap<Product, ProductDTO>()
                .ForMember(
                destiny => destiny.CategoryDescription, 
                opt => opt.MapFrom(origin => origin.IdCategoryNavigation.Name)
                )
                .ForMember(
                destiny => destiny.Price, 
                opt => opt.MapFrom(origin => Convert.ToString(origin.Price, new CultureInfo("en-US")))
                )
                .ForMember(
                destiny => destiny.IsActive, 
                opt => opt.MapFrom(origin => origin.IsActive == true ? 1:0)
                );
            CreateMap<ProductDTO, Product>()
                .ForMember(
                destiny => destiny.IdCategoryNavigation, 
                opt => opt.Ignore()
                )
                .ForMember(
                destiny => destiny.Price, 
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("en-US")))
                )
                .ForMember(
                destiny => destiny.IsActive, 
                opt => opt.MapFrom(origin => origin.IsActive == 1 ? true: false)
                );
            #endregion Product

            #region Sale
            CreateMap<Sale, SaleDTO>()
                .ForMember(
                destiny => destiny.Total, 
                opt => opt.MapFrom(origin => Convert.ToString(origin.Total, new CultureInfo("en-us")))
                )
                .ForMember(
                destiny => destiny.RegistrationDate, 
                opt => opt.MapFrom(origin => origin.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<SaleDTO, Sale>()
                .ForMember(
                destiny => destiny.Total, 
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Total, new CultureInfo("en-US")))
                );
            #endregion Sale

            #region SaleDetail
            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(
                destiny => destiny.ProductDescription,
                opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
                )
                .ForMember(
                destiny => destiny.Price,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Price, new CultureInfo("en-US")))
                )
                .ForMember(
                destiny => destiny.Total,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Total, new CultureInfo("en-US")))
                );
            CreateMap<SaleDetailDTO, SaleDetail>()
                .ForMember(
                destiny => destiny.Price,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("en-US")))
                )
                .ForMember(
                destiny => destiny.Total,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Total, new CultureInfo("en-US")))
                );
            #endregion SaleDetail

            #region Report
            CreateMap<SaleDetail, ReportDTO>()
                .ForMember(
                destiny => destiny.RegistrationDate,
                opt => opt.MapFrom(origin => origin.IdSaleNavigation.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(
                destiny => destiny.DocumentNumber,
                opt => opt.MapFrom(origin => origin.IdSaleNavigation.DocumentNumber)
                )
                .ForMember(
                destiny => destiny.PaymentType,
                opt => opt.MapFrom(origin => origin.IdSaleNavigation.PaymentType)
                )
                .ForMember(
                destiny => destiny.TotalSale,
                opt => opt.MapFrom(origin => Convert.ToString(origin.IdSaleNavigation.Total.Value, new CultureInfo("en-US")))
                )
                .ForMember(
                destiny => destiny.Product,
                opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
                )
                .ForMember(
                destiny => destiny.Price,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("en-US")))
                )
                .ForMember(
                destiny => destiny.Total,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("en-US")))
                );
            #endregion Report

        }
    }
}
