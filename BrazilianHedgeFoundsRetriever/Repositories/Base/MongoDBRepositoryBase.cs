using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrazilianHedgeFoundsRetriever.Repositories.Base
{
    public abstract class MongoDBRepositoryBase<T> where T : class
    {
        public virtual void InsertMany(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual IList<T> GetByFilter(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }
    }
}
