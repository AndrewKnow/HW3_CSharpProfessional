using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Abstractions
{
    public interface IEFRepository<Customer>
    {
        Task CreateCustomerAsync(Customer customer);
        Task <Customer> GetCustomerAsync(long entity);
        
    }
}
