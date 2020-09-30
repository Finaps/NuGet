using System;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Database.Mongo.Connection
{
  public class MongoConnection
  {
    public MongoClient Client { get; private set; }
    public IMongoDatabase Database { get; private set; }
    public MongoConnection(string connectionString, string databaseName, Action<ClusterBuilder> clusterConfigurator = null)
    {
      var settings = MongoClientSettings.FromConnectionString(connectionString);
      if (clusterConfigurator != null)
      {
        settings.ClusterConfigurator = clusterConfigurator;
      }

      Client = new MongoClient(settings);
      Database = Client.GetDatabase(databaseName);
    }
  }
}
