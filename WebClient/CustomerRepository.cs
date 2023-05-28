using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    public static class CustomerRepository
    {

        private static string _url = "https://localhost:5001/customers/";

        public static async Task<Customer> GetFromDB(int id)
        {
            Customer customer = new Customer();
            RestClient restClient = new RestClient();

            using (restClient = new RestClient(_url + id))
            {
                try
                {
                    var response = await restClient.ExecuteAsync<Customer>(new RestRequest());
                    Customer myDeserializedClass = JsonConvert.DeserializeObject<Customer>(response.Content);
                    customer = myDeserializedClass;
                    return customer;
                }
                catch
                {
                    return null;
                }

            } 
        }

        public static Task<int> AddToDB(CustomerCreateRequest randomCustomer)
        {
           
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string stringRandomCustomer = JsonConvert.SerializeObject(randomCustomer, serializerSettings);
            var content = new StringContent(stringRandomCustomer, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    using var response = client.PostAsync(_url, content).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Customer myDeserializedClass = JsonConvert.DeserializeObject<Customer>(stringRandomCustomer);
                        Customer customer = new Customer();
                        customer = myDeserializedClass;

                        var id = response.Content.ReadAsStringAsync().Result;

                        return Task.FromResult(int.Parse(id));
                    }
                    else
                    {

                    }

                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
