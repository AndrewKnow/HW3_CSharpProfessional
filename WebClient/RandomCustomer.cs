using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    public class RandomCustomer
    {


        public static Customer CreateRndCustomer()
        {
            var rndCustomer = new Customer();

            Random rnd = new Random();
            int id = rnd.Next(1, 100);

            int rndNameLenth = rnd.Next(5, 10);

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string firstName = new string(Enumerable.Repeat(chars, rndNameLenth)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());


            int rndLastNameLenth = rnd.Next(5, 10);

            string lastName = new string(Enumerable.Repeat(chars, rndLastNameLenth)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());


            return rndCustomer;

        }

    }
}
