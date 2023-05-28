using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Abstractions;
using WebApi.Models;

namespace WebApi.Repository
{
    public class EFRepository : IEFRepository<Customer>
    {

        private readonly DbContext _dataContext;
        public EFRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _dataContext.AddAsync(customer);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerAsync(long id)
        {
            var entity = await _dataContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
