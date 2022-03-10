using BrazilianHedgeFoundsRetriever.Repositories.Base;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BrazilianHedgeFoundsRetriever.Repositories.Implementor
{
    public class MongoDBRepositoryImplementor<T> : MongoDBRepositoryBase<T> where T : class
    {
        private readonly IConfiguration configuration;

        public MongoDBRepositoryImplementor(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public override void InsertMany(List<T> entities)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetValue<string>("ConnectionStrings:MongoDbLocalHost"));
            mongoClient.GetDatabase(configuration.GetValue<string>("MongoDBrepositoryConfigs:DataBaseName"))
                .GetCollection<T>(configuration.GetValue<string>("MongoDBrepositoryConfigs:CollectionName"))
                .InsertMany(entities);
        }

        public override IList<T> GetByFilter(FilterDefinition<T> filter)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetValue<string>("ConnectionStrings:MongoDbLocalHost"));
            return mongoClient.GetDatabase(configuration.GetValue<string>("MongoDBrepositoryConfigs:DataBaseName"))
                .GetCollection<T>(configuration.GetValue<string>("MongoDBrepositoryConfigs:CollectionName"))
                .Find(filter).ToList();
        }
    }
}
