namespace Libraary.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Scheduler.ScheduledTasks;
    using System;
    using System.Threading.Tasks;

    public abstract class BaseController : Controller
    {
        private readonly IServiceProvider provider;
        private readonly IServiceScopeFactory factory;

        protected BaseController() { }

        protected BaseController(IServiceProvider provider, IServiceScopeFactory factory)
        {
            this.provider = provider;
            this.factory = factory;
        }

        protected async Task UpdateBooksInCache()
        {
            var task = new BooksTask(factory);
            await task.ProcessInScope(provider);
        }
    }
}
