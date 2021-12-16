using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICWR.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static ICollection<Person> GetPersons()
        {
            IList<Person> list = new List<Person>();
            for (int i = 0; i < 630; i++)
            {
                list.Add(new Person() { Id = i, Name = "name" + i });
            }
            return list;
        }
    }
}