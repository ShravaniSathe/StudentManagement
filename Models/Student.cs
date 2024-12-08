﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public string CV { get; set; }
    }
}