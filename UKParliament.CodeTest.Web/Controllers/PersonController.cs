using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Web.ViewModels;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Controllers.Extensions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger, IPersonService service)
        {
            _logger = logger;
            _service = service;
        }


        // GET: api/Person
        [HttpGet]
        public IActionResult GetAllPeople()
        {
            return new ExceptionHandler().Execute<IActionResult>(MethodBase.GetCurrentMethod(),
               () => Ok(_service.GetAllPeople()),
               () => NotFound());

        }

        // GET: api/Person/5
        [HttpGet("{personID:int}")]
        public IActionResult GetPeopleById(int personID)
        {
            return new ExceptionHandler().Execute<IActionResult>(MethodBase.GetCurrentMethod(),
               () => Ok(_service.GetPeopleById(personID)),
               () => NotFound());
        }

        // GET: api/Person/SearchPeople?personName=roger
        [HttpGet]
        [Route("SearchPeople")]
        public IActionResult SearchPeople(string personName)
        {
            return Ok(_service.FilterPeopleByName(personName));
        }

        // POST: api/Players
        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            return new ExceptionHandler().Execute<IActionResult>(MethodBase.GetCurrentMethod(),
                () =>
                {
                    var validationResultList = new List<ValidationResult>();
                    Validator.TryValidateObject(person, new ValidationContext(person), validationResultList);
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    return Ok(_service.CreatePeople(person));
                },
                () => NotFound());
        }

        // PUT: api/Players
        [HttpPut]
        public IActionResult ModifyPerson(Person person)
        {
            return new ExceptionHandler().Execute<IActionResult>(MethodBase.GetCurrentMethod(),
                () =>
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    return Ok(_service.UpdatePeople(person));
                },
                () => NotFound());
        }

        // DELETE: api/Players/5
        [HttpDelete("{id:int}")]
        public IActionResult DeletePerson(int id)
        {
            return new ExceptionHandler().Execute<IActionResult>(MethodBase.GetCurrentMethod(),
                () =>
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    _service.DeletePeople(id);

                    return Ok();
                },
                () => NotFound());
        }
    }
}