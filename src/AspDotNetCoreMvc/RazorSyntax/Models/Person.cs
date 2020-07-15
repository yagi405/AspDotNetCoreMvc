using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorSyntax.Models
{
    public class Person
    {
        [DisplayName("名前")]
        public string Name { get; set; }

        [DisplayName("年齢")]
        [Url]
        public int Age { get; set; }

        [DisplayName("誕生日")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [DisplayName("既婚")]
        public bool IsMarried { get; set; }

        public Person() { }

        public Person(string name, int age, DateTime birthDay, bool isMarried) =>
            (Name, Age, BirthDay, IsMarried) = (name, age, birthDay, isMarried);
    }
}
