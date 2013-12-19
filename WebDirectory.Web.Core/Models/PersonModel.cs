using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebDirectory.Core;
using Person = WebDirectory.Web.Core.Models.ViewModels.Person;

namespace WebDirectory.Web.Core.Models
{
    public interface IPersonModel
    {
        IEnumerable<Person> GetAllPersonnel();
        Person GetPerson(int id);
        int SavePerson(Person person);
        int SavePerson(int id, Person person);
        void DeletePerson(int id);
    }

    public class PersonModel : IPersonModel
    {
        private readonly IPersonRepository _personRepository;

        public PersonModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> GetAllPersonnel()
        {
            return
                _personRepository.GetListOfPeople()
                                 .Select(Mapper.Map<WebDirectory.Core.Person, Person>)
                                 .ToList();
        }

        public Person GetPerson(int id)
        {
            var person = _personRepository.GetPerson(id);

            return Mapper.Map<WebDirectory.Core.Person, Person>(person);
        }

        public int SavePerson(Person person)
        {
            return _personRepository.SavePerson(Mapper.Map<Person, WebDirectory.Core.Person>(person));
        }

        public int SavePerson(int id, Person person)
        {
            return _personRepository.SavePerson(id, Mapper.Map<Person, WebDirectory.Core.Person>(person));
        }

        public void DeletePerson(int id)
        {
            _personRepository.DeletePerson(id);
        }
    }
}