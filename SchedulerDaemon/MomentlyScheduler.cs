using SchedulerDaemon.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender;

namespace SchedulerDaemon
{
    class MomentlyScheduler
    {
        public static void IntervalInSeconds(double interval, Action task)
        {
            interval = interval / 60;
            SchedulerService.Instance.ScheduleTask(interval, task);
        }

        public static void IntervalInMinutes(double interval, Action task)
        {
            //interval = interval;
            SchedulerService.Instance.ScheduleTask(interval, task);
        }

        public static void SearchMailTasksToExecute()
        {
            throw new NotImplementedException();
        }
    }

}
