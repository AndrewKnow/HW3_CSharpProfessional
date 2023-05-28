using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Ввод в таблицу:\n 0 - Выход из программы \n 1 - Выбрать клиента по ID (ввод целого числа)\n 2 - Сгенерировать случайного клиента");

                var num = Console.ReadLine();

                bool tryParseNum;
                tryParseNum = int.TryParse(num, out _);

                if (tryParseNum)
                {
                    int intNum = int.Parse(num);

                    if (intNum == 0) Environment.Exit(0);

                    if (intNum == 1)
                    {
                        Console.WriteLine("Укажите Id");
                        var IdVar = Console.ReadLine();
                        bool IntParseID = int.TryParse((string)IdVar, out _);
                        if (IntParseID)
                        {
                            int IdInt = int.Parse(IdVar);
                            var findCustomer = await  GetCustomer.GetFromDB(IdInt);
                            Console.WriteLine($"Найден: Id:{findCustomer.Id}, FirstName:{findCustomer.Firstname}, LastName:{findCustomer.Lastname}");
                        }
                        else
                        {
                            Console.WriteLine("Введено не число");
                        }
                    }
                    if (intNum == 2)
                    {
           
                    }
                }
            }
        }

        /// <summary>
        /// создание рандомного клиента
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>генерирует firstName и lastName </returns>
        private static CustomerCreateRequest RandomCustomer()
        {
            var rndCustomer = new Customer();

            Random rnd = new Random();

            int rndNameLenth = rnd.Next(5, 10);

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string firstName = new string(Enumerable.Repeat(chars, rndNameLenth)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());

            int rndLastNameLenth = rnd.Next(5, 10);

            string lastName = new string(Enumerable.Repeat(chars, rndLastNameLenth)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());

            return new CustomerCreateRequest(firstName, lastName);
        }
    }
}