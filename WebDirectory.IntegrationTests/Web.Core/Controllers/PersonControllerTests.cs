using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using NUnit.Framework;
using Ninject;
using WebDirectory.Web.Core;
using WebDirectory.Web.Core.Controllers;
using WebDirectory.Web.Core.Models.ViewModels;

namespace WebDirectory.IntegrationTests.Web.Core.Controllers
{
    [TestFixture]
    public class WhenUsingThePersonController
    {
        private PersonController _personController;

        [SetUp]
        public void SetupTests()
        {
            var kernel = new StandardKernel(new WebModule());
            _personController = kernel.Get<PersonController>();

            var request = new HttpRequestMessage();
            var config = new HttpConfiguration();
            _personController.Request = request;
            _personController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [Test]
        public void ShouldBeAbleToGetListOfAllThePersons()
        {
            var expectedNumberOfPersons = 1;

            var result = _personController.GetListOfAllPerson() as IList<Person>;

            Assert.IsNotNull(result);            
            Assert.AreEqual(expectedNumberOfPersons, result.Count);
        }

        [Test]
        public void ShouldBeAbleToGetASpecificPerson()
        {
            var expectedFirstName = "Fred";
            var expectedLastName = "Flinstone";
            var expectedUserId = 1;

            var result = _personController.GetSpecificPerson(expectedUserId);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFirstName, result.FirstName);
            Assert.AreEqual(expectedLastName, result.LastName);
            Assert.AreEqual(expectedUserId, result.UserId);
        }

        [Test]
        public void ShouldBeAbleToSaveANewPerson()
        {
            var firstName = "Barney";
            var lastName = "Rubble";
            
            var oldCount = ((IList<Person>)_personController.GetListOfAllPerson()).Count;

            var result = _personController.PostPerson(new Person { FirstName = firstName, LastName = lastName });

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            var newCount = ((IList<Person>)_personController.GetListOfAllPerson()).Count;

            Assert.AreEqual(++oldCount, newCount);
           //todo add code to pull json out or response and validate
        }
    }
}