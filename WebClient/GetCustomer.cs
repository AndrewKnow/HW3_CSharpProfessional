using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    public static class GetCustomer
    {
        private static RestClient restClient;
        public static async Task<Customer> GetFromDB(int id)
        {
            Customer customer = new Customer();

            var url = "https://localhost:5001/customers/" + id;


            using (restClient = new RestClient(url))
            {
                var response = await restClient.ExecuteAsync<Customer>(new RestRequest());

                Customer myDeserializedClass = JsonConvert.DeserializeObject<Customer>(response.Content);

                customer = myDeserializedClass;   

            }


            return customer;
        }
    }
}
