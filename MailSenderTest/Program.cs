using System;
using System.Threading;

namespace MailSenderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTest.Start();

            //ThreadSyncronizationTest.Start();

            var threads = new Thread[10];
            for (var i = 0; i < threads.Length; i++)
            {
                int i0 = i;
                threads[i] = new Thread(() => Console.WriteLine($"Сообщение №{i0}"));
            }
            for (var i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            Console.ReadLine();
        }

        
    }
}
