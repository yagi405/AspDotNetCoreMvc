using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWithModel.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsMarried { get; set; }

        public Person(string name, int age, DateTime birthDay, bool isMarried) =>
            (Name, Age, BirthDay, IsMarried) = (name, age, birthDay, isMarried);
    }
}
