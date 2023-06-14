using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UKParliament.CodeTest.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PersonManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<PersonManagerContext>>()))
            {
                // Look for any people data already in database.
                if (context.People.Any())
                {
                    return;   // Database has been seeded
                }

                var people = new List<Person>
                {
                    new Person
                    {
                        Title = "Mr",
                        FirstName ="Dainel",
                        LastName ="Craig",
                        DateOfBirth = DateOnly.Parse("12/5/2008"),
                        Gender = "Male"
                    },
                    new Person
                    {
                        Title = "Mr",
                        FirstName = "William",
                        LastName = "Firth",
                        DateOfBirth = DateOnly.Parse("12/7/2009"),
                        Gender = "Male"
                    },
                    new Person
                    {
                        Title = "Mr",
                        FirstName = "Hugh",
                        LastName = "Wilkinson",
                        DateOfBirth = DateOnly.Parse("12/11/2007"),
                        Gender = "Male"
                    },
                    new Person
                    {
                        Title = "Ms",
                        FirstName = "Rachel",
                        LastName = "Ellis",
                        DateOfBirth = DateOnly.Parse("2/3/2006"),
                        Gender = "Female"
                    },
                    new Person
                    {
                        Title = "Mr",
                        FirstName = "Roger",
                        LastName = "Federer",
                        DateOfBirth = DateOnly.Parse("25/12/2005"),
                        Gender = "Female"
                    },
                    new Person
                    {
                        Title = "Ms",
                        FirstName ="Lilly",
                        LastName ="Townsend",
                        DateOfBirth = DateOnly.Parse("14/7/2018"),
                        Gender = "Male"
                    },
                    new Person
                    {
                        Title = "Mr",
                        FirstName = "Lewis",
                        LastName = "Gallore",
                        DateOfBirth = DateOnly.Parse("12/7/1987"),
                        Gender = "Male"
                    },
                    new Person
                    {
                        Title = "Mr",
                        FirstName = "Nathan",
                        LastName = "Sueppage",
                        DateOfBirth = DateOnly.Parse("12/11/1995"),
                        Gender = "Male"
                    },
                    new Person
                    {
                        Title = "Mr",
                        FirstName = "Eddie",
                        LastName = "Corbett",
                        DateOfBirth = DateOnly.Parse("09/3/2008"),
                        Gender = "Female"
                    },
                    new Person
                    {
                        Title = "Ms",
                        FirstName = "Rita",
                        LastName = "Mal",
                        DateOfBirth = DateOnly.Parse("25/09/2005"),
                        Gender = "Female"
                    }
                };
                context.People.AddRange(people);
                context.SaveChanges();
            }
        }
    }

}

