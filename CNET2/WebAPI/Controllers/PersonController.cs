using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PeopleContext _db;
        public PersonController(PeopleContext db)
        {
            _db = db;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Person> GetPeople()
        {
            //var dataSet = Data.Serialization.LoadFromXML(@"d:\Data\SkoleniICTpro\PersonDataset\dataset.xml");
            return _db.Persons
                .Include(x=>x.Contracts)
                .Include(x=>x.HomeAddress);
        }

        [HttpGet("ByEmail/{email}")]
        public Person GetByEmail(string email)
        {
            //var dataSet = Data.Serialization.LoadFromXML(@"d:\Data\SkoleniICTpro\PersonDataset\dataset.xml");

            return _db.Persons
                .Include(x => x.Contracts)
                .Include(x => x.HomeAddress)
                .Where(p => p.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        [HttpGet("ById/{Id}")]
        public Person GetById(int Id)
        {
            return _db.Persons.Include(x => x.Contracts)
                .Include(x => x.HomeAddress)
                .FirstOrDefault(p => p.Id == Id);
        }
    }
}
