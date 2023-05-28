using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Abstractions;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        // ���������� ��� ��������� �� ���������
        //1.�������� ����������
        private readonly IEFCustomerRepository _customerRepository;
        public CustomerController(IEFCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        //2. ���������� ������� ����������
        [HttpGet("{id:long}")]   
        public async Task<Customer> GetCustomerAsync([FromRoute] long id)
        {
            return await _customerRepository.GetCustomerAsync(id);
        }

        [HttpPost("")]   
        public async Task<long> CreateCustomerAsync([FromBody] Customer customer)
        {
            await _customerRepository.CreateCustomerAsync(customer);
            return customer.Id;
        }
    }
}