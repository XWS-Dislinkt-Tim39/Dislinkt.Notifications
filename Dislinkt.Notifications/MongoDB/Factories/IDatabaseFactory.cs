using MongoDB.Driver;

namespace Dislinkt.Notifications.MongoDB.Factories
{
    public interface IDatabaseFactory
    {
        IMongoDatabase Create();
    }
}
