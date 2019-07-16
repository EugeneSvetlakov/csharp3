using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSenderTest
{
    internal static class ThreadTest
    {
        public static void Start()
        {
            Thread.CurrentThread.Name = "Main thread";
            //Thread.CurrentThread.ManagedThreadId

            //var thread = new Thread(new ThreadStart(FirstThreadMethod));
            var thread1 = new Thread(FirstThreadMethod);
            //var thread2 = new Thread(new ParameterizedThreadStart(FirstParametricThreadMethod));
            var thread2 = new Thread(FirstParametricThreadMethod);

            thread1.Start();
            thread2.Start("Hello world!");

            var clock_thread = new Thread(UpdateClockMethod);
            clock_thread.Name = "Поток обновления часов";
            clock_thread.Priority = ThreadPriority.Lowest;
            clock_thread.IsBackground = true;

            var msg = "Hellow world!";
            var count = 100;
            var timeout = 100;

            var printer_thread1 = new Thread(() => PrintMethod(msg, count, timeout));
            //printer_thread1.Start();

            var printer_task = new ThreadPrinterTask
            {
                Message = "qwe",
                Count = 100,
                Timeout = 10
            };
            var printer_thread2 = new Thread(printer_task.PrintMethod);
            printer_thread2.Start();

            clock_thread.Start();
            //clock_thread.ThreadState == ThreadState.Background

            Console.WriteLine($"Главный поток программы id {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();

            Console.WriteLine($"Остановка потока часов");
            _IsClockUpdating = false;

            if (!clock_thread.Join(100))
            {
                //Щедящее прерывание потока
                clock_thread.Interrupt();
                //Жесткое прерывание потока
                clock_thread.Abort();
            }
        }

        private static void FirstThreadMethod()
        {
            Console.WriteLine($"Выполнение в потоке с id {Thread.CurrentThread.ManagedThreadId}");
        }

        private static void FirstParametricThreadMethod(object parameter)
        {
            Console.WriteLine($"Выполнение в потоке с id {Thread.CurrentThread.ManagedThreadId} с параметром {parameter}");

        }

        private static bool _IsClockUpdating = true;

        private static void UpdateClockMethod()
        {
            try
            {
                while (_IsClockUpdating)
                {
                    Thread.Sleep(1000);
                    Console.Title = DateTime.Now.ToString("HH:mm:ss:fff");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine($"Часы завершили свою работу");
        }

        private static void PrintMethod(string Message, int Count, int Timeout)
        {
            for (var i = 0; i < Count; i++)
            {
                Console.WriteLine($"Сообщение из потока id {Thread.CurrentThread.ManagedThreadId} - {Message} :: {i}");
                Thread.Sleep(Timeout);
            }
        }
    }

   
}


