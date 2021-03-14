using System;
using System.ComponentModel.DataAnnotations;

namespace APIApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateOfJoin { get; set; }

    }
}