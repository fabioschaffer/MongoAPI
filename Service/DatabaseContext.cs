using MongoDB.Driver;

namespace MongoAPI.Services;
public class DataBaseContext {

    private readonly IConfiguration configuration;

    public DataBaseContext(IConfiguration configuration) {
        this.configuration = configuration;
    }

    public IMongoDatabase GetDatabase() {
        MongoClient client = new MongoClient(configuration["Database:ConnectionString"]);
        return client.GetDatabase(configuration["Database:DbName"]);
    }
}