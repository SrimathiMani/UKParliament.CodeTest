using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data
{
    public class PersonManagerContext : DbContext
    {
        public PersonManagerContext(DbContextOptions<PersonManagerContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}