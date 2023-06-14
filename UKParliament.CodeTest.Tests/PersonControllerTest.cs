using System;
using System.Collections.Generic;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Controllers;
using UKParliament.CodeTest.Web.ViewModels;
using Xunit;
using UKParliament.CodeTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace UKParliament.CodeTest.Tests
{
    public class PersonControllerTest : IDisposable
    {
        protected readonly PersonManagerContext _context;
        public PersonControllerTest()
        {
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .Options;

            _context = new PersonManagerContext(options);

            _context.Database.EnsureCreated();
            _context.People.AddRange(getDummyPersonData());
            _context.SaveChanges();
        }

        #region GetAllPeople

        [Fact]
        public void GetAllPeople_Returns_OkResult()
        {
            //arrange
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);


            //act
            var result = _controller.GetAllPeople() as ObjectResult;


            //assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllPeople_Returns_AllResult()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);


            //act
            var result = _controller.GetAllPeople() as ObjectResult;


            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result?.Value);
            var items = Assert.IsType<List<Person>>(result?.Value);
            Assert.Equal(peopleList.Count(), items.Count());
        }

        #endregion GetAllPeople

        #region GetPeopleById

        [Fact]
        public void GetPeopleById_Returns_CorrectResult()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);

            //act
            var result = _controller.GetPeopleById(2) as OkObjectResult;

            //assert
            Assert.NotNull(result);
            var items = Assert.IsType<Person>(result?.Value);
            Assert.True(peopleList[1].Id == items.Id);
        }

        [Fact]
        public void GetPeopleById_Returns_NoResults_For_Invalid_Id()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);

            //act
            var result = _controller.GetPeopleById(100) as OkObjectResult;

            //assert
            Assert.Null(result?.Value);
            Assert.True(null == result?.Value);
        }
        #endregion GetPeopleById


        #region CreatePerson

        [Fact]
        public void CreatePerson_AddsItem()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);
            var newPerson = new Person
            {
                Title = "Mr",
                FirstName = "testFirstName",
                LastName = "testLastName",
                DateOfBirth = DateOnly.Parse("13/7/2008"),
                Gender = "Male"
            };

            //act
            var result = _controller.CreatePerson(newPerson) as ObjectResult;

            //assert
            Assert.NotNull(result);
            Person? newPers = Assert.IsType<Person>(result?.Value);

            var compareRes = _controller.GetPeopleById(newPers.Id ?? default(int)) as ObjectResult;
            var items = Assert.IsType<Person>(compareRes?.Value);

            Assert.True(newPerson.FirstName == items.FirstName);
        }

        [Fact]
        public void CreatePerson_InvalidMissingData()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);
            var newPerson = new Person
            {
                Title = "Mr",
                //FirstName = "testFirstName",
                LastName = "testLastName",
                DateOfBirth = DateOnly.Parse("13/7/2008"),
                Gender = "Male"
            };

            //act
            var result = _controller.CreatePerson(newPerson) as ObjectResult;

            //assert
            Assert.Null(result);
        }


        #endregion CreatePerson


        #region UpdatePerson

        [Fact]
        public void UpdatePerson_UpdatesItem()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);
            var modPerson = new Person
            {
                Id = 2,
                Title = "Mr",
                FirstName = "testing",
                LastName = "Firth",
                DateOfBirth = DateOnly.Parse("12/7/2009"),
                Gender = "Male"
            };

            _context.ChangeTracker.Clear();
            //act
            var result = _controller.ModifyPerson(modPerson) as ObjectResult;


            //assert
            Assert.IsType<Person>(result?.Value);
            var updatedItem = Assert.IsType<Person>(result?.Value);
            Assert.True(updatedItem.FirstName == modPerson.FirstName);
        }

        #endregion UpdatePerson


        #region DeletePerson
        [Fact]
        public void Delete_Returns_Null_InvalidId()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);

            //act
            var result = _controller.DeletePerson(100) as ObjectResult;

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_RemovesEntry()
        {
            //arrange
            var peopleList = getDummyPersonData();
            var _service = new PersonService(_context);
            var _controller = new PersonController(null, _service);

            //act
            var result = _controller.DeletePerson(1) as ObjectResult;

            //assert
            Assert.Null(result);

            var compareRes = _controller.GetPeopleById(1) as ObjectResult;

            Assert.Null(compareRes?.Value);
        }
        #endregion DeletePerson

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private List<Person> getDummyPersonData()
        {
            return new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Title = "Mr",
                    FirstName ="James",
                    LastName ="Braggs",
                    DateOfBirth = DateOnly.Parse("12/5/2008"),
                    Gender = "Male"
                },
                new Person
                {
                    Id = 2,
                    Title = "Mr",
                    FirstName = "William",
                    LastName = "Firth",
                    DateOfBirth = DateOnly.Parse("12/7/2009"),
                    Gender = "Male"
                },
                new Person
                {
                    Id = 3,
                    Title = "Mr",
                    FirstName = "Neal",
                    LastName = "Wilkinson",
                    DateOfBirth = DateOnly.Parse("12/11/2007"),
                    Gender = "Male"
                },
                new Person
                {
                        Id = 4,
                    Title = "Ms",
                    FirstName = "Sarah",
                    LastName = "Ellis",
                    DateOfBirth = DateOnly.Parse("2/3/2006"),
                    Gender = "Female"
                },
                new Person
                {
                     Id = 5,
                    Title = "Ms",
                    FirstName = "Roger",
                    LastName = "Federer",
                    DateOfBirth = DateOnly.Parse("25/12/2005"),
                    Gender = "Female"
                }
            };
        }
    }


}