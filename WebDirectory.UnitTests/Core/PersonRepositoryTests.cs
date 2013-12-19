using NUnit.Framework;
using WebDirectory.Core;

namespace WebDirectory.UnitTests.Core
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        IPersonRepository _personRepository;

        [SetUp]
        public void SetupPersonRepositoryForTesting()
        {
            _personRepository = new PersonRepository();
        }

        [Test]
        public void ShouldBeAbleToGetTheSeededPerson()
        {
            var expectedPerson = new Person { FirstName = "Fred", LastName = "Flinstone", UserId = 1 };

            var result = _personRepository.GetPerson(1);

            Assert.AreEqual(expectedPerson.FirstName, result.FirstName);
            Assert.AreEqual(expectedPerson.LastName, result.LastName);
            Assert.AreEqual(expectedPerson.UserId, result.UserId);
        }

        [Test]
        public void ShouldBeAbleToAddAPerson()
        {
            var expectedPerson = new Person { FirstName = "Barney", LastName = "Rubble" };
            var defaultUserId = expectedPerson.UserId;

            var resultUserId = _personRepository.SavePerson(expectedPerson);
            var resultPerson = _personRepository.GetPerson(resultUserId);

            Assert.AreNotEqual(defaultUserId, resultUserId);
            Assert.AreEqual(expectedPerson.FirstName, resultPerson.FirstName);
            Assert.AreEqual(expectedPerson.LastName, resultPerson.LastName);
            Assert.AreEqual(expectedPerson.UserId, resultPerson.UserId);
        }

        [Test]
        public void ShouldBeAbleToUpdateAnExisitngPerson()
        {
            var expectedPerson = new Person { FirstName = "Wilma", LastName = "Something" };

            var resultUserId = _personRepository.SavePerson(expectedPerson);
            var resultPerson = _personRepository.GetPerson(resultUserId);

            Assert.AreEqual(expectedPerson.FirstName, resultPerson.FirstName);
            Assert.AreEqual(expectedPerson.LastName, resultPerson.LastName);
            Assert.AreEqual(expectedPerson.UserId, resultPerson.UserId);

            var newLastName = "Flinstone";
            resultPerson.LastName = newLastName;

            _personRepository.SavePerson(resultPerson.UserId, resultPerson);

            resultPerson = _personRepository.GetPerson(resultUserId);

            Assert.AreEqual(newLastName, newLastName);
        }

        [Test]
        public void ShouldBeAbleToDeleteAPerson()
        {
            var expectedPerson = new Person { FirstName = "Wilma", LastName = "Flinstone" };

            var resultUserId = _personRepository.SavePerson(expectedPerson);
            var resultPerson = _personRepository.GetPerson(resultUserId);

            Assert.AreEqual(expectedPerson.FirstName, resultPerson.FirstName);
            Assert.AreEqual(expectedPerson.LastName, resultPerson.LastName);
            Assert.AreEqual(expectedPerson.UserId, resultPerson.UserId);

            _personRepository.DeletePerson(resultUserId);

            resultPerson = _personRepository.GetPerson(resultUserId);

            Assert.IsNull(resultPerson);
        }
    }
}