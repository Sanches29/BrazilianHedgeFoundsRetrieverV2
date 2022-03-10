using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrazilianHedgeFoundsRetriever.Entities.Responses
{
    public class RetrieveHedgeFoundsDataByFilterResponse
    {
        public IList<HedgeFoundData> HedgeFoundDatas { get; set; }
    }
}
