using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet("GetAll")]
        public List<Person> GetPeople()
        {
            var dataSet = Data.Serialization.LoadFromXML(@"d:\Data\SkoleniICTpro\PersonDataset\dataset.xml");

            return dataSet;
        }

        [HttpGet("ByEmail/{email}")]
        public Person GetByEmail(string email)
        {
            var dataSet = Data.Serialization.LoadFromXML(@"d:\Data\SkoleniICTpro\PersonDataset\dataset.xml");

            return dataSet.Where(p => p.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }
    }
}
