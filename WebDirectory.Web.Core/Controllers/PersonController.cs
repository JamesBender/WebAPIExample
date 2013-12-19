using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDirectory.Web.Core.Models;
using WebDirectory.Web.Core.Models.ViewModels;

namespace WebDirectory.Web.Core.Controllers
{
    public class PersonController : ApiController
    {
        IPersonModel _personModel;

        public PersonController(IPersonModel personModel)
        {
            AutomapperConfiguration.Configure();
            _personModel = personModel;
        }

        public HttpResponseMessage DeleteExistingPerson(int personId)
        {
            _personModel.DeletePerson(personId);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public HttpResponseMessage PutExistingPerson(int userId, Person person)
        {
            _personModel.SavePerson(userId, person);
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }

        public HttpResponseMessage PostPerson(Person person)
        {
            var personId = _personModel.SavePerson(person);
            person.UserId = personId;
            var response = Request.CreateResponse<Person>(HttpStatusCode.Created, person);
            return response;
        }

        public Person GetSpecificPerson(int personId)
        {
            return _personModel.GetPerson(personId);
        }

        public IEnumerable<Person> GetListOfAllPerson()
        {
            return _personModel.GetAllPersonnel();
        }
    }
}
