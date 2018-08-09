[![CodeFactor](https://www.codefactor.io/repository/github/fluentley/fluentley.querybuilder/badge)](https://www.codefactor.io/repository/github/fluentley/fluentley.querybuilder) 
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/77a34113980d487d9aaade0bad5e4e9e)](https://www.codacy.com/project/emre_3/Fluentley.QueryBuilder/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=fluentley/Fluentley.QueryBuilder&amp;utm_campaign=Badge_Grade_Dashboard)
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Fluentley.QueryBuilder)

# Fluentley.QueryBuilder
Fluentley QueryBuilder focuses on advanced query abstraction for queryable objects.


Installation
------------

[Fluentley.QueryBuilder is available on NuGet](https://www.nuget.org/packages/Fluentley.QueryBuilder).

Either open the package console and type:

```
PM> Install-Package Fluentley.QueryBuilder
```

Quick Start
-----------

**Sample Model**

------------


```csharp
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
```

**Sample Data**

------------
```csharp
     var list = new List<Person>
                {
                    new Person("Alice", 20),
                    new Person("Bob", 30),
                    new Person("Patricia", 40),
                    new Person("Bill", 50),
                    new Person("Gabriealla", 50)
                };
```

**Query Sample**

------------
```csharp
//Use complex query as variable.
      Action<IQueryOption<Person>> queryOption = option => option
                        .QueryBy(query => query.Where(x => x.Name.ToLower().Contains("a")))
                        .Paging(0, 1)
                        .DynamicWhere("x=> x.Name != \"Gabriealla\"");
    
       IQueryResult<Person> queryResult = list.QueryOn(queryOption);
```
 `IQueryResult<T>` interface contains properties such as `Data` property which is basically always returns type of ```IQueryable<T>```, which means we can still apply filters before the execution.

** Eagerloads**

------------
 `EagerLoads`  as name implies used for eagerloading entities.
 ```csharp
      foreach (var eagerLoad in queryResult.EagerLoads)
                    queryResult.Data.Include(eagerLoad);
```


** Paging**

------------


 `QueryPaging` class can be used for any type of in memory or in case of EntityFramework `DataSet<T>` paging.
 
 ```csharp
     public class QueryPaging
        {
            public int TotalPages { get; set; }
            public int TotalRecords { get; set; }
            public int CurrentPageIndex { get; set; }
            public bool IsPaged { get; set; }
        }
```
