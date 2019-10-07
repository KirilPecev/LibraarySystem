namespace Libraary.Scheduler.ScheduledTasks
{
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Services;
    using Services.DTOs.Book;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BooksTask : ScheduledProcessor
    {
        public BooksTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory) { }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var books = GetAllBooks(serviceProvider);

            UpdateCache(serviceProvider, books);

            return Task.CompletedTask;
        }

        private IEnumerable<BookDTO> GetAllBooks(IServiceProvider serviceProvider)
        {
            var bookService = serviceProvider.GetService(typeof(IBookService)) as IBookService;
            return bookService.GetAll();
        }

        private void UpdateCache(IServiceProvider serviceProvider, IEnumerable<BookDTO> events)
        {
            var cache = serviceProvider.GetService(typeof(IDistributedCache)) as IDistributedCache;

            var serializedObject = JsonConvert.SerializeObject(events);
            cache.SetString("books", serializedObject);

            Console.WriteLine($"Books saved to Redis.({DateTime.Now})");
        }
    }
}
