using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Telerik.JustMock;
using WebDirectory.Core;
using WebDirectory.Web.Core.Models;

namespace WebDirectory.UnitTests.Web.Core.Models
{
    [TestFixture]
    public class WhenUsingThePersonModel
    {
        private IPersonRepository _personRepository;
        private PersonModel _personModel;

        [SetUp]
        public void SetupPersonModelForTesting()
        {
            AutomapperConfiguration.Configure();
            _personRepository = Mock.Create<IPersonRepository>();

            _personModel = new PersonModel(_personRepository);
        }

        [Test]
        public void ShouldBeAbleToGetAPerson()
        {
            var expectedFirstName = "Barney";
            var expectedLastName = "Rubble";
            var expectedUserId = 2;

            Mock.Arrange(() => _personRepository.GetPerson(expectedUserId))
                .Returns(new WebDirectory.Core.Person { FirstName = expectedFirstName, LastName = expectedLastName, UserId = expectedUserId });

            var result = _personModel.GetPerson(expectedUserId);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFirstName, result.FirstName);
            Assert.AreEqual(expectedLastName, result.LastName);
            Assert.AreEqual(expectedUserId, result.UserId);
        }

        [Test]
        public void ShouldBeAbleToGetAListOfAllPeople()
        {
            Mock.Arrange(() => _personRepository.GetListOfPeople())
                .Returns(new List<WebDirectory.Core.Person> { new WebDirectory.Core.Person() });
            var result = _personModel.GetAllPersonnel();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToList<WebDirectory.Web.Core.Models.ViewModels.Person>().Count);
        }

        [Test]
        public void ShouldBeAbleToAddAPerson()
        {
            var firstName = "Wilma";
            var lastName = "Filnstone";
            var userId = 3;

            Mock.Arrange(() => _personRepository
                .SavePerson(Arg.Matches<WebDirectory.Core.Person>(x => x.FirstName == firstName && x.LastName == lastName)))
                .Returns(userId);

            var savedPerson = new WebDirectory.Web.Core.Models.ViewModels.Person { FirstName = firstName, LastName = lastName, UserId = userId };

            var result = _personModel.SavePerson(savedPerson);

            Assert.AreEqual(userId, result);
        }

        [Test]
        public void ShouldBeAbleToUpdateAPerson()
        {
            var userId = 3;
            var firstName = "Wilma";
            var lastName = "Flinstone";

            Mock.Arrange(() => _personRepository
                .SavePerson(userId, Arg.Matches<WebDirectory.Core.Person>(x => x.FirstName == firstName && x.LastName == lastName)))
                .Returns(userId);
            var savedPerson = new WebDirectory.Web.Core.Models.ViewModels.Person { FirstName = firstName, LastName = lastName, UserId = userId };

            var result = _personModel.SavePerson(userId, savedPerson);

            Assert.AreEqual(userId, result);
        }

        [Test]
        public void ShouldBeAbleToDeleteAPerson()
        {
            var userId = 3;

            _personModel.DeletePerson(userId);

            Mock.Assert(() => _personRepository.DeletePerson(userId), Occurs.Once());
        }
    }
}