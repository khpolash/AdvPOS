using System;

namespace AdvPOS.Models.AttendanceViewModel
{
    public class AttendanceGridViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 EmployeeId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime StayTime { get; set; }


    }
}

