using Hangfire;
using HangFire.Application.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace HangFire.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        [HttpPost("createBackgroundJob")]
        public IActionResult CreateBackgroundJob()
        {
            BackgroundJob.Enqueue<IEmailService>(x=>x.EmailSend("2k20it01@kiot.ac.in"));
            return Ok();
        }

        [HttpPost("scheduleJob")]
        public IActionResult ScheduleJob()
        {
            BackgroundJob.Schedule(() => Console.WriteLine($"Schedule Job triggered Current Time:{DateTime.Now}"), TimeSpan.FromMilliseconds(5000));
            return Ok();
        }

        [HttpPost("continousJob")]
        public IActionResult ContinousJob()
        {
            string jobId = BackgroundJob.Schedule(() => Console.WriteLine($"continous Job 1 Triggered :{DateTime.Now}"), TimeSpan.FromSeconds(1));
            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine($"continous Job 2 Triggered :{DateTime.Now}"));
            return Ok();
        }

        [HttpPost("recurringJob")]
        public IActionResult CreateRecurringJob()
        {
            RecurringJob.AddOrUpdate<IEmailService>("recurring-job", x => x.EmailSend("kesavan.saravanan@concertidc.com"), "*/5 * * * * *");
            return Ok();
        }
    }
}
