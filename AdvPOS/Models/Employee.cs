using System;

namespace AdvPOS.Models
{
    public class Employee: EntityBase
    {
        public Int64 Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Designation { get; set; }
        public int Department { get; set; }
        public int? SubDepartment { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LeavingDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
