using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using MailSender.Data;
using MailSender.Data.EF;
using MailSender.Data.BaseEntityes;
using MailSender.Services;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MailSender.Services.EF;

namespace SchedulerDaemon
{
    class Program
    {
        /// <summary>
        /// Служба выполнения запланированных задач отправки почты
        /// </summary>
        /// <param name="args">параметры запуска</param>
        static void Main(string[] args)
        {
            while (true)
            {
                SearchMailTasks();
                Thread.Sleep(60000);
            }
        }

        private static void SearchMailTasks()
        {
            Console.WriteLine("ПереЗапуск планировщика.");
            ObservableCollection<MailTask> MailTasks;
            DateTime dt_now;

            using (var db = new MailSenderDB())
            {
                #region Получение списка запланированных задач
                IMailTasksDataService mailTasksEFData;
                mailTasksEFData = new MailTasksEFData(db);
                MailTasks = new ObservableCollection<MailTask>(mailTasksEFData.GetAll().Where(s => s.SendStatusEnum == SendStatusEnum.Scheduled));
                dt_now = DateTime.Now;
                if (MailTasks is null) throw new ArgumentNullException(nameof(MailTasks), "Нет подходящих данных.");
                foreach (var item in MailTasks)
                {
                    var SecondsToStart = (item.Time - dt_now).TotalSeconds;
                    if (SecondsToStart < 60)
                    {
                        mailTasksEFData.SendTaskAsync(item).Wait();
                    }
                }
                #endregion
            }
        }
    }
}
