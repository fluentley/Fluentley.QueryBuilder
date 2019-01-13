using System;
using System.Collections.Generic;
using System.Linq;
using Fluentley.QueryBuilder;
using Fluentley.QueryBuilder.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Fluentley.QueryBuilder
{
    [TestClass]
    public class QueryBuilderTests
    {
        [TestMethod]
        public void SimpleIntegrationTest()
        {
            var list = new List<Person>
            {
                new Person("Alice", 20),
                new Person("Bob", 30),
                new Person("Patricia", 40),
                new Person("Bill", 50),
                new Person("Gabriealla", 50)
            };

            Action<IQueryOption<Person>> queryOption = option => option
                    .QueryBy(query => query.Where(x => x.Name.ToLower().Contains("a")))
                    .Paging(0, 1)
                    .DynamicWhere("x=> x.Name != \"Gabriealla\"")
                ;


            var queryResult = list.QueryOn(queryOption);
            Assert.AreEqual(1, queryResult.Data.Count());
        }

        public class Person
        {
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}