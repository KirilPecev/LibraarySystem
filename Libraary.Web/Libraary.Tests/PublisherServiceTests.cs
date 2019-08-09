namespace Libraary.Tests
{
    using Common;
    using Services;
    using Services.DTOs.Publisher;
    using System.Linq;
    using Xunit;

    public class PublisherServiceTests
    {
        private IPublisherService publisherService;


        [Fact]
        public void AddPublisher_WithCorrectData_ShouldSuccessfullyAddPublisher()
        {
            string errorMessagePrefix = "PublisherService Add() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);

            AddPublisherDTO publisherDto = new AddPublisherDTO()
            {
                Name = "Testt1",
                URLAddress = "https/facebook.com/"
            };

            bool actualResult = this.publisherService.Add(publisherDto);
            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public void AddPublisher_WithPublisherAlreadyInDb_ShouldNotAddPublisher()
        {
            string errorMessagePrefix = "PublisherService Add() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);
            LibraaryDbContextInMemoryFactory.SeedDb(context);

            AddPublisherDTO publisherDto = new AddPublisherDTO()
            {
                Name = "Test",
                URLAddress = "https/facebook.com/"
            };

            bool actualResult = this.publisherService.Add(publisherDto);
            Assert.True(!actualResult, errorMessagePrefix);
        }

        [Fact]
        public void GetAllByLibraryId_WithCorrectData_ShouldReturnAllAuthors()
        {
            string errorMessagePrefix = "PublisherService GetAllByLibraryId() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);

            LibraaryDbContextInMemoryFactory.SeedDb(context);

            var id = LibraaryDbContextInMemoryFactory.GetLibraryId(context);

            var publisher = this.publisherService.GetAllByLibraryId(id);

            Assert.True(publisher != null, errorMessagePrefix);
        }

        [Fact]
        public void GetAllByLibraryId_WithInCorrectData_ShouldReturnNull()
        {
            string errorMessagePrefix = "PublisherService GetAllByLibraryId() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);

            var publishers = this.publisherService.GetAllByLibraryId("teest");

            Assert.True(publishers.Count() == 0, errorMessagePrefix);
        }

        [Fact]
        public void GetAllPublishersName_ShouldReturnAllPublishers()
        {
            string errorMessagePrefix = "PublisherService GetAllPublishersName() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);
            LibraaryDbContextInMemoryFactory.SeedDb(context);
            var publishers = this.publisherService.GetAllPublishersName();

            Assert.True(publishers.Count() != 0, errorMessagePrefix);
        }


        [Fact]
        public void GetAllPublishersName_ShouldReturnNull()
        {
            string errorMessagePrefix = "PublisherService GetAllPublishersName() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);

            var publishers = this.publisherService.GetAllPublishersName();

            Assert.True(publishers.Count() == 0, errorMessagePrefix);
        }

        [Fact]
        public void GetPublisher_WithCorrectData_ShouldReturnPublisher()
        {
            string errorMessagePrefix = "PublisherService GetAllPublishersName() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);
            LibraaryDbContextInMemoryFactory.SeedDb(context);
            var publisher = this.publisherService.GetPublisher("Test");

            Assert.True(publisher != null, errorMessagePrefix);
        }

        [Fact]
        public void GetPublisher_WithIncorrectData_ShouldReturnNull()
        {
            string errorMessagePrefix = "PublisherService GetAllPublishersName() method does not work properly.";

            var context = LibraaryDbContextInMemoryFactory.InitializeContext();
            this.publisherService = new PublisherService(context);

            var publisher = this.publisherService.GetPublisher("Test");

            Assert.True(publisher == null, errorMessagePrefix);
        }
    }
}
