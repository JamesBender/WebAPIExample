using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using NUnit.Framework;
using Telerik.JustMock;
using WebDirectory.Web.Core.Controllers;
using WebDirectory.Web.Core.Models;
using WebDirectory.Web.Core.Models.ViewModels;
using Person = WebDirectory.Web.Core.Models.ViewModels.Person;

namespace WebDirectory.UnitTests.Web.Core.Controllers
{
    [TestFixture]
    public class WhenUsingThePersonController
    {
        private PersonController _personController;
        private IPersonModel _personModel;
        private Person _personOne;
        private Person _personTwo;
        private IList<Person> _personList;
        private string _firstNameOne = "first name 1";
        private string _lastNameOne = "last name 1";
        private int _userIdOne = 1;

        [SetUp]
        public void SetupForTests()
        {
            _personModel = Mock.Create<IPersonModel>(Behavior.Strict);
            _personController = new PersonController(_personModel);

            _personOne = new Person { FirstName = _firstNameOne, LastName = _lastNameOne, UserId = _userIdOne };
            _personTwo = new Person { FirstName = "FirstName", LastName = "LastName", UserId = 2 };
            _personList = new List<Person> { _personOne, _personTwo };

            var request = new HttpRequestMessage();
            var config = new HttpConfiguration();
            _personController.Request = request;
            _personController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [Test]
        public void ShouldBeAbleToGetAListOfPerson()
        {
            Mock.Arrange(() => _personModel.GetAllPersonnel()).Returns(_personList).OccursOnce();
         
            var result = _personController.GetListOfAllPerson() as IList<Person>;

            Assert.IsNotNull(result);
            Assert.AreEqual(_personList.Count, result.Count);
            Mock.Assert(_personModel);
        }

        [Test]
        public void ShouldBeAbleToGetASpecificPerson()
        {
            var expectedPersonId = 1;
            Mock.Arrange(() => _personModel.GetPerson(1)).Returns(_personOne).OccursOnce();

            var result = _personController.GetSpecificPerson(expectedPersonId) as Person;

            Assert.IsNotNull(result);
            Assert.AreEqual(_firstNameOne, result.FirstName);
            Assert.AreEqual(_lastNameOne, result.LastName);
            Assert.AreEqual(_userIdOne, result.UserId);
            Mock.Assert(_personModel);
        }

        [Test]
        public void ShouldBeAbleToAddANewPerson()
        {
            var person = new Person { FirstName = "New", LastName = "Person" };
            var expectedPersonId = 3;
            //var request = new HttpRequestMessage();
            //var config = new HttpConfiguration();
            //_personController.Request = request;
            //_personController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            Mock.Arrange(() => _personModel.SavePerson(Arg.IsAny<Person>())).Returns(expectedPersonId).OccursOnce();

            var result = _personController.PostPerson(person);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsNotNull(result.Content);
            Mock.Assert(_personModel);
        }

        [Test]
        public void ShouldBeAbleToSaveAExistingPersonViaPut()
        {
            var newFirstName = "updated";
            _personOne.FirstName = newFirstName;
            Mock.Arrange(() => _personModel.SavePerson(_personOne.UserId, Arg.IsAny<Person>())).Returns(_personOne.UserId).OccursOnce();

            var result = _personController.PutExistingPerson(_personOne.UserId, _personOne);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            Assert.IsNull(result.Content);
            Mock.Assert(_personModel);
        }

        [Test]
        public void ShouldBeAbleToDeleteAPerson()
        {
            Mock.Arrange(() => _personModel.DeletePerson(1)).OccursOnce();

            var result = _personController.DeleteExistingPerson(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Mock.Assert(_personModel);
        }
        
    }
}