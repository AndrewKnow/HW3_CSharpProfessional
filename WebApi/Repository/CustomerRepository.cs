using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Abstractions;
using WebApi.Models;

namespace WebApi.Repository
{
    public class CustomerRepository : IEFCustomerRepository
    {

        private readonly CustomerDataContext _dataContext;
        public CustomerRepository(CustomerDataContext customerDataContext)
        {
            _dataContext = customerDataContext;
        }

        public async Task<long> CreateCustomerAsync(Customer customer)
        {
            await _dataContext.AddAsync(customer);
            await _dataContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<Customer> GetCustomerAsync(long id)
        {
            var entity = await _dataContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
