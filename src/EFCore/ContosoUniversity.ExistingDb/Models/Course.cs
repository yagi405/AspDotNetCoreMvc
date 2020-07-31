using System;
using System.Collections.Generic;

namespace ContosoUniversity.ExistingDb.Models
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
