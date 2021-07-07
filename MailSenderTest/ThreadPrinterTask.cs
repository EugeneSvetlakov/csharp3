using System;
using System.Threading;

namespace MailSenderTest
{
    internal class ThreadPrinterTask
    {
        public string Message { get; set; }
        public int Timeout { get; set; }
        public int Count { get; set; }

        public void PrintMethod()
        {
            var count = Count;
            var timeout = Timeout;
            var msg = Message;

            for (var i = 0; i < count; i++)
            {
                Console.WriteLine($"Сообщение из потока id {Thread.CurrentThread.ManagedThreadId} - {msg} :: {i}");
                Thread.Sleep(timeout);
            }
        }
    }
}
