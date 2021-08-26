using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebYouToken {
    public class Program {
        public static string ClientId;
        public static string Scope = "account-info operation-history operation-details incoming-transfers";
        
        public static void Main(string[] args) {
            Console.WriteLine("Запихнуть в hosts: 127.0.0.1 youcode.local");

            Console.WriteLine("Данные для формы: https://yoomoney.ru/myservices/new");
            Console.WriteLine("Назваине: любое");
            Console.WriteLine("Адрес сайта: http://youcode.local");
            Console.WriteLine("Почта: любая");
            Console.WriteLine("Redirect URI: http://youcode.local/code");
            Console.WriteLine("====================================================");
            
            Console.WriteLine("Введите ClientID");
            ClientId = Console.ReadLine();
            if (string.IsNullOrEmpty(ClientId)) {
                Console.WriteLine("Invalid, retry");
                return;
            }
            ClientId = ClientId.Trim(' ', '\n', '\r');

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseKestrel(options => {
                        options.Listen(IPAddress.Parse("127.0.0.1"), 80);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
