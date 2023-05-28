using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Abstractions;
using WebApi.Models;

namespace WebApi.Repository
{
    public class EFCustomerRepository : IEFCustomerRepository
    {

        private readonly CustomerDataContext _dataContext;
        public EFCustomerRepository(CustomerDataContext customerDataContext)
        {
            _dataContext = customerDataContext;
        }

        public async Task<long> CreateCustomerAsync(Customer customer)
        {
            await _dataContext.customers!.AddAsync(customer);
            await _dataContext.SaveChangesAsync();
            return customer.id;
        }

        public async Task<Customer> GetCustomerAsync(long id)
        {
            var entity = await _dataContext.Set<Customer>().FirstOrDefaultAsync(x => x.id == id);
            return entity;
        }
    }
}
