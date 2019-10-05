namespace Libraary.Scheduler.ScheduledTasks
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public class BooksTask : ScheduledProcessor
    {
        public BooksTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        protected override string Schedule { get; }
    }
}
