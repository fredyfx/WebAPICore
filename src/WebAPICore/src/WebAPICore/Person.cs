using System;
using System.Collections.Generic;
using System.Linq;

namespace TestCoreAPI.Controllers
{
  public class People
  {
    List<Person> _people = new List<Person>()
      {
        new Person() { Id = 1, Name = "Shawn", Birthday = DateTime.Parse("04/24/1969"), Present = true },
        new Person() { Id = 2, Name = "Bill", Birthday = DateTime.Parse("03/15/1941"), Present = false },
        new Person() { Id = 3, Name = "Resa", Birthday = DateTime.Parse("08/27/1981"), Present = false },
        new Person() { Id = 4, Name = "Chris", Birthday = DateTime.Parse("06/02/1969"), Present = true },
      };

    public IEnumerable<Person> Get()
    {
      return _people;
    }

    public void Add(Person p)
    {
      _people.Add(p);
    }

    public void Remove(Person p)
    {
      _people.Remove(p);
    }
  }

  public class Person
  {
    public int Id { get; set; }
    public DateTime Birthday { get; set; }
    public string Name { get; set; }
    public bool Present { get; set; }
  }

}