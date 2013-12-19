using System;
using System.Collections.Generic;

namespace WebDirectory.Core
{
    public class PersonRepository : IPersonRepository
    {
        private static readonly IDictionary<int, Person> _personList = new Dictionary<int, Person>();
        private readonly Person _samplePerson = new Person { FirstName = "Fred", LastName = "Flinstone", UserId = 1 };
        private static Object _hold = new Object();

        public PersonRepository()
        {
            if (!_personList.ContainsKey(_samplePerson.UserId))
            {
                _personList.Add(_samplePerson.UserId, _samplePerson);
            }

        }

        public Person GetPerson(int userId)
        {
            if (_personList.ContainsKey(userId))
            {
                return _personList[userId];
            }
            return null;
        }

        public int SavePerson(Person addedPerson)
        {
            if (addedPerson.UserId != 0)
            {
                return SavePerson(addedPerson.UserId, addedPerson);
            }

            lock (_hold)
            {
                var newUserId = _personList.Count + 1;
                addedPerson.UserId = newUserId;
                _personList.Add(newUserId, addedPerson);
            }

            return addedPerson.UserId;
        }

        public int SavePerson(int userId, Person updatedPerson)
        {
            if (_personList.ContainsKey(userId))
            {
                _personList[userId] = updatedPerson;
            }
            else
            {
                throw new ArgumentException("Person does not exist");
            }

            return userId;
        }

        public void DeletePerson(int userId)
        {
            if (_personList.ContainsKey(userId))
            {
                _personList.Remove(userId);
            }
        }

        public ICollection<Person> GetListOfPeople()
        {
            return _personList.Values;
        }
    }
}