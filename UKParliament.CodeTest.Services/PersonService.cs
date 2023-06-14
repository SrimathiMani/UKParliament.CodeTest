using UKParliament.CodeTest.Data;
using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {
        private PersonManagerContext dbContext;

        public PersonService(PersonManagerContext context)
        {
            dbContext = context;
        }

        public List<Person> GetAllPeople()
        {
            var peopleList = dbContext.People.ToList();
            return peopleList;
        }

        public List<Person> FilterPeopleByName(string personName)
        {
            //match with first name and last name
            return dbContext.People.Where(_ => _.FirstName.ToLower().Contains(personName.ToLower()) || _.LastName.ToLower().Contains(personName.ToLower())).OrderBy(a => a.FirstName).ToList();
        }

        public Person GetPeopleById(int personID)
        {
            return dbContext.People.Where(_ => _.Id == personID).FirstOrDefault();
        }

        public Person CreatePeople(Person person)
        {
            var result = dbContext.People.Add(person);
            dbContext.SaveChanges();

            return result.Entity;
        }

        public Person UpdatePeople(Person person)
        {
            var result = dbContext.People.Update(person);
            dbContext.SaveChanges();

            return result.Entity;
        }

        public void DeletePeople(int personID)
        {
            var person = dbContext.People.FirstOrDefault(bo => bo.Id == personID);
            if (person != null)
            {
                dbContext.People.Remove(person);
                dbContext.SaveChanges();
            }
        }
    }
}