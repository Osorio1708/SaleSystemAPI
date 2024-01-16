using SaleSystem.DAL.DBContext;
using SaleSystem.DAL.Repository.Contract;
using SaleSystem.Model;

namespace SaleSystem.DAL.Repository
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsaleContext _dbContext;

        public SaleRepository(DbsaleContext dbContext): base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Sale> Record(Sale model)
        {
            Sale generatedSale = new Sale();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (SaleDetail sd in model.SaleDetails)
                    {
                        Product? foundProduct = _dbContext.Products.Where(p => p.IdProduct == sd.IdProduct).FirstOrDefault();
                        if (foundProduct != null)
                        {
                            foundProduct.Stock = foundProduct.Stock - sd.Quantity;
                            _dbContext.Products.Update(foundProduct);
                        }
                    }
                    await _dbContext.SaveChangesAsync();
                    DocumentNumber correlative = _dbContext.DocumentNumbers.First();
                    correlative.LastNumber = correlative.LastNumber + 1;
                    correlative.RegistrationDate = DateTime.Now;
                    _dbContext.DocumentNumbers.Update(correlative);
                    await _dbContext.SaveChangesAsync();

                    int DigitCount = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", DigitCount));
                    string saleNumber = ceros + correlative.LastNumber.ToString();
                    saleNumber = saleNumber.Substring(saleNumber.Length - DigitCount, DigitCount);
                    model.DocumentNumber = saleNumber;
                    await _dbContext.Sales.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    generatedSale = model;
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                return generatedSale;
            }

        }
    }
}
