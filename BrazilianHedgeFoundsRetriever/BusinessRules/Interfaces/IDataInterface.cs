using BrazilianHedgeFoundsRetriever.Entities.Requests;
using BrazilianHedgeFoundsRetriever.Entities.Responses;

namespace BrazilianHedgeFoundsRetriever.BusinessRules.Interfaces
{
    public interface IDataInterface
    {
        RetrieveHedgeFoundsDataByFilterResponse RetrieveHedgeFoundsDataByFilter(RetrieveHedgeFoundsDataByFilterRequest filter);
        public void LoadData();
    }
}
