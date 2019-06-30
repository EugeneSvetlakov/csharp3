using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MailSenderTest
{
    internal static class ThreadSyncronizationTest
    {
        public static void Start()
        {
            for(var i = 0; i < 5; i++)
            {
                new Thread(PrintNumber).Start();
            }
        }

        private static readonly object _SyncRoot = new object();

        private static void PrintNumber()
        {
            lock (_SyncRoot)
            {
                Console.Write($"Thread id: {Thread.CurrentThread.ManagedThreadId} - ");
                for (var i = 0; i < 10; i++)
                {
                    Console.Write($"{i}, ");
                    Thread.Sleep(10);
                }
                Console.WriteLine("10");
            }
        }

        private static void PrintNumber2()
        {
            Monitor.Enter(_SyncRoot);
            try
            {
                Console.Write($"Thread id: {Thread.CurrentThread.ManagedThreadId} - ");
                for (var i = 0; i < 10; i++)
                {
                    Console.Write($"{i}, ");
                    Thread.Sleep(10);
                }
                Console.WriteLine("10");
            }
            finally
            {
                Monitor.Exit(_SyncRoot);
            }
        }
    }
}
