using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

using Quartz;
using System.Threading.Tasks;
using SearchSystems.Models;

namespace SearchSystems.JobsAndSchedules
{
    public class ProvidentFundActivatorJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // Define the job to be scheduled: Go to the db and check for if probation period
            // is over for any of the employees!
            var employeeDb = new SEARCHSYSTEMSEntities4();
            var probationOverEmployees = employeeDb.Employees.Where(e => e.DateOfJoining.Value.
                                            AddMonths(Convert.ToInt32(e.ProbationPeriod.Value)).Equals(DateTime.Today));

            if (probationOverEmployees.Count() > 0)
            {
                // Get names of the employees

                var firstNames = probationOverEmployees.Select(x => x.FirstName).ToList();
                var names = string.Join(", ", firstNames);
                using (var message = new MailMessage("searchnotify22@gmail.com", "sampadadsapre@gmail.com"))
                {
                    message.Subject = "Probation Period Over";
                    message.Body = string.Format("Probation Period Over for employees: {0}", names);
                    using (SmtpClient client = new SmtpClient
                    {
                        EnableSsl = true,
                        Host = "smtp.gmail.com",
                        Port = 587,
                        Credentials = new NetworkCredential("searchnotify22@gmail.com", "testsearch22")
                    })
                    {
                        client.Send(message);
                        await Console.Out.WriteLineAsync("HelloJob is executing.");
                    }
                }
            }

        }
    }
}