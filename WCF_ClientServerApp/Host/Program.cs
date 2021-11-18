using System;
using System.ServiceModel;
using Server;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(MyContractServer)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал!");
                Console.ReadLine();
            }
        }
    }
}
