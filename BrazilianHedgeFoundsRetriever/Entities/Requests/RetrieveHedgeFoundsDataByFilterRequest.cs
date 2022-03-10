using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrazilianHedgeFoundsRetriever.Entities.Requests
{
    public class RetrieveHedgeFoundsDataByFilterRequest
    {
        public string CNPJ { get; set; }
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate { get; set; }
    }
}
