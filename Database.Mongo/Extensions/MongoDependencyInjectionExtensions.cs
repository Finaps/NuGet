using System;
using Database.Mongo.Connection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Database.Mongo.Extensions
{
  public static class MongoDependencyInjectionExtensions
  {
    public static IServiceCollection AddMongoDBConnection(
      this IServiceCollection services,
      MongoOptions options = null,
      Action<ClusterBuilder> clusterConfigurator = null)
    {
      options = options ?? new MongoOptions()
      {
        ConnectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING"),
        Database = Environment.GetEnvironmentVariable("DATABASE_NAME")
      };

      var connection = new MongoConnection(options.ConnectionString, options.Database, clusterConfigurator);
      return services.AddSingleton<MongoConnection>(connection);
    }

    public static IServiceCollection UseMongoDBCollection<T>(this IServiceCollection services)
    {
      return services.AddScoped<IMongoCollection<T>>((ctx) =>
      {
        MongoConnection connection = ctx.GetRequiredService<MongoConnection>();
        return connection.Database.GetCollection<T>(typeof(T).Name);
      });
    }
  }

}
