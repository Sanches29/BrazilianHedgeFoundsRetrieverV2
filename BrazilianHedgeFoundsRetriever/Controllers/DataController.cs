using BrazilianHedgeFoundsRetriever.BusinessRules.Interfaces;
using BrazilianHedgeFoundsRetriever.Entities.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BrazilianHedgeFoundsRetriever.Controllers
{
    public class DataController : ApiControllerBase
    {
        protected readonly IDataInterface dataInterface;

        public DataController(IDataInterface dataInterface)
        {
            this.dataInterface = dataInterface;
        }

        [HttpPost("load")]
        public IActionResult LoadData()
        {
            this.dataInterface.LoadData();
            return ResponseDefault();
        }

        [HttpGet]
        public IActionResult RetrieveHedgeFoundsDataByFilter([FromQuery]RetrieveHedgeFoundsDataByFilterRequest filter)
        {
            return ResponseDefault(this.dataInterface.RetrieveHedgeFoundsDataByFilter(filter));
        }
    }
}
