using MongoDB.Driver;

namespace MongoDB_ürün_katalog_Projesi.Utils
{
    public class MongoDb
    {
        private readonly string connectionString = "mongodb://localhost:27017";
        private readonly string databaseName = "Urun_Katalogu";
        public readonly IMongoDatabase _database;

        public MongoDb()
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}