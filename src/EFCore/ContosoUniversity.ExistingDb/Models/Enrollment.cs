﻿using System;
using System.Collections.Generic;

namespace ContosoUniversity.ExistingDb.Models
{
    public partial class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int? Grade { get; set; }
    }
}
