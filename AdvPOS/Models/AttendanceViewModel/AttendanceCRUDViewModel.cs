using System;
using System.ComponentModel.DataAnnotations;

namespace AdvPOS.Models.AttendanceViewModel
{
    public class AttendanceCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; } 
        [Required]
        [Display(Name = "Employee")]
        public Int64 EmployeeId { get; set; }
        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }
        [Display(Name = "Check In")]
        [Required]
        public DateTime CheckIn { get; set; }
        public string CheckInDisplay { get; set; }
        [Display(Name = "Check Out")]
        [Required]
        public DateTime CheckOut { get; set; }
        public string CheckOutDisplay { get; set; }
        [Display(Name = "Stay Time")]
        public TimeSpan StayTime { get; set; }


        public static implicit operator AttendanceCRUDViewModel(Attendance _Attendance)
        {
            return new AttendanceCRUDViewModel
            {
                Id = _Attendance.Id,
                EmployeeId = _Attendance.EmployeeId,
                CheckIn = _Attendance.CheckIn,
                CheckOut = _Attendance.CheckOut,
                StayTime = _Attendance.StayTime,
                CreatedDate = _Attendance.CreatedDate,
                ModifiedDate = _Attendance.ModifiedDate,
                CreatedBy = _Attendance.CreatedBy,
                ModifiedBy = _Attendance.ModifiedBy,
                Cancelled = _Attendance.Cancelled,

            };
        }

        public static implicit operator Attendance(AttendanceCRUDViewModel vm)
        {
            return new Attendance
            {
                Id = vm.Id,
                EmployeeId = vm.EmployeeId,
                CheckIn = vm.CheckIn,
                CheckOut = vm.CheckOut,
                StayTime = vm.StayTime,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}
