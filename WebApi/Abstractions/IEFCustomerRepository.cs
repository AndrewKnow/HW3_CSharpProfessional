using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Abstractions
{
    public interface IEFCustomerRepository
    {
        Task <long> CreateCustomerAsync(Customer customer);
        Task <Customer> GetCustomerAsync(long entity);
    }
}
