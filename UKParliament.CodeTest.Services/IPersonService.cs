using System;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public interface IPersonService
    {
        List<Person> GetAllPeople();

        List<Person> FilterPeopleByName(string personName);

        Person GetPeopleById(int personID);

        Person CreatePeople(Person person);

        Person UpdatePeople(Person person);

        void DeletePeople(int personID);
    }
}