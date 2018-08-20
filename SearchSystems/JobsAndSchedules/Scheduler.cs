using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Quartz;
using Quartz.Core;
using Quartz.Impl;

namespace SearchSystems.JobsAndSchedules
{
    public class Scheduler
    {
        public static void Start()
        {
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler scheduler = sf.GetScheduler().Result;
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<ProvidentFundActivatorJob>().Build();

            // Check every 24 hours if it is 1st of the month.
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithInterval(new TimeSpan(1,0,0,0))
            .WithRepeatCount(6))
            .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}