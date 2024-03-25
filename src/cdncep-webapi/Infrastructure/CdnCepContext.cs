using cdncep_webapi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace cdncep_webapi.Infrastructure
{
    public class CdnCepContext
    {
        private readonly IMongoDatabase _database = null;

        public CdnCepContext(IOptions<CdnCepSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase("cepdb");
                // _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<CepModel> Ceps
        {
            get
            {
                return _database.GetCollection<CepModel>("ceps");
            }
        }
    }
}
