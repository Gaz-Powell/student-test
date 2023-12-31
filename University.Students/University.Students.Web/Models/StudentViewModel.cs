﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace University.Students.Web.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Year of study")]
        public string YearOfStudy { get; set; }

        public IEnumerable<string> Subjects { get; set; }

        public SelectList YearOfStudyOptions { get; set; }
    }
}
