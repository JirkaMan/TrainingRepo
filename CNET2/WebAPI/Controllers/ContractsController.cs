using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data;
using Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly PeopleContext _db;
        public ContractsController(PeopleContext db)
        {
            _db = db;
        }

        [HttpGet("GetAllContracts")]
        public IEnumerable<Contract> GetContracts()
        {
            var dataSet = Data.Serialization.LoadFromXML(@"d:\Data\SkoleniICTpro\PersonDataset\dataset.xml");

            return dataSet.SelectMany(p => p.Contracts);
        }
    }
}
