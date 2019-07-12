namespace Libraary.Services
{
    using Libraary.Domain;

    public interface IPublisherService
    {
        Publisher GetPublisher(string name);
    }
}
