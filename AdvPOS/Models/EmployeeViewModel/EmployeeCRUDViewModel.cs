using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.EmployeeViewModel
{
    public class EmployeeCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Today;
        public int Designation { get; set; }
        public string DesignationDisplay { get; set; }
        public int Department { get; set; }
        public string DepartmentDisplay { get; set; }
        [Display(Name = "Sub Department")]
        public int? SubDepartment { get; set; }
        public string SubDepartmentDisplay { get; set; }
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; } = DateTime.Today;
        [Display(Name = "Leaving Date")]
        public DateTime LeavingDate { get; set; } = DateTime.Today;
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public static implicit operator EmployeeCRUDViewModel(Employee _Employee)
        {
            return new EmployeeCRUDViewModel
            {
                Id = _Employee.Id,
                EmployeeId = _Employee.EmployeeId,
                FirstName = _Employee.FirstName,
                LastName = _Employee.LastName,
                DateOfBirth = _Employee.DateOfBirth,
                Designation = _Employee.Designation,
                Department = _Employee.Department,
                SubDepartment = _Employee.SubDepartment,
                JoiningDate = _Employee.JoiningDate,
                LeavingDate = _Employee.LeavingDate,
                Phone = _Employee.Phone,
                Email = _Employee.Email,
                Address = _Employee.Address,
                CreatedDate = _Employee.CreatedDate,
                ModifiedDate = _Employee.ModifiedDate,
                CreatedBy = _Employee.CreatedBy,
                ModifiedBy = _Employee.ModifiedBy,
                Cancelled = _Employee.Cancelled,
            };
        }

        public static implicit operator Employee(EmployeeCRUDViewModel vm)
        {
            return new Employee
            {
                Id = vm.Id,
                EmployeeId = vm.EmployeeId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DateOfBirth = vm.DateOfBirth,
                Designation = vm.Designation,
                Department = vm.Department,
                SubDepartment = vm.SubDepartment,
                JoiningDate = vm.JoiningDate,
                LeavingDate = vm.LeavingDate,
                Phone = vm.Phone,
                Email = vm.Email,
                Address = vm.Address,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
