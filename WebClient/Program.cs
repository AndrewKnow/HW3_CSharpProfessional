using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
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
                        Console.WriteLine("Укажите Id:");
                        var IdVar = Console.ReadLine();
                        bool IntParseID = int.TryParse((string)IdVar, out _);
                        if (IntParseID)
                        {
                            int IdInt = int.Parse(IdVar);
                            var findCustomer = await CustomerRepository.GetFromDB(IdInt);
                            if(findCustomer != null)
                            {
                                Console.WriteLine($"Найден: Id:{IdInt}, FirstName:{findCustomer.Firstname}, LastName:{findCustomer.Lastname}");
                            }    
                            else
                            {
                                Console.WriteLine($"{IdInt} не существует");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Введено не число");
                        }
                    }
                    if (intNum == 2)
                    {
                        CustomerCreateRequest randomCustomer = RandomCustomer();
                        int adddCustomerId = await CustomerRepository.AddToDB(randomCustomer);
                        var findCustomer = await CustomerRepository.GetFromDB(adddCustomerId);

                        if (findCustomer != null)
                        {
                            Console.WriteLine($"Создан: Id:{adddCustomerId}, FirstName:{findCustomer.Firstname}, LastName:{findCustomer.Lastname}");
                            Console.WriteLine("Проверка на сервере:");

                        }
                        else
                        {
                            Console.WriteLine("Ошибка. Запись не создана");
                        }
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