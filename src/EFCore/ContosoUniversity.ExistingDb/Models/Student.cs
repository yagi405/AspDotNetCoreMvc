using System;
using System.Collections.Generic;

namespace ContosoUniversity.ExistingDb.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
