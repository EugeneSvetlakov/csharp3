using System;
using System.Threading;

namespace MailSenderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTest.Start();

            ThreadSyncronizationTest.Start();

            Console.ReadLine();
        }

        
    }
}
