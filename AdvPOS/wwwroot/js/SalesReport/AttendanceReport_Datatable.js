$(document).ready(function () {
  AttendanceDataTableLoad(false, 0);
});

var AttendanceDataTableLoad = function (_IsFilterData, _EmployeeId){
  document.title = "Attendance Report";
  $("#tblAttendanceReport").DataTable({
    paging: true,
    select: true,
    order: [[0, "desc"]],
    dom: "Bfrtip",

    buttons: ["pageLength"],

    processing: true,
    serverSide: true,
    filter: true, //Search Box
    orderMulti: false,
    stateSave: true,

    ajax: {     
      url: "/SalesReport/AttendanceReportDataTable?IsFilterData=" + _IsFilterData + "&EmployeeId= " + _EmployeeId,
      type: "POST",
      datatype: "json",
    },

    columns: [
      { data: "Id", name: "Id" },
      { data: "EmployeeName", name: "EmployeeName" },
      { data: "CheckInDisplay", name: "CheckInDisplay" },
      { data: "CheckOutDisplay", name: "CheckOutDisplay" },
      { data: "StayTime", name: "StayTime" },

      {
        data: "CreatedDate",
        name: "CreatedDate",
        autoWidth: true,
        render: function (data) {
          var date = new Date(data);
          var month = date.getMonth() + 1;
          return (
            (month.length > 1 ? month : month) +
            "/" +
            date.getDate() +
            "/" +
            date.getFullYear()
          );
        },
      },
    ],

    columnDefs: [
      {
        targets: [0, 1, 2, 3, 4, 5],
        orderable: false,
      },
    ],

    lengthMenu: [
      [20, 10, 15, 25, 50, 100, 200],
      [20, 10, 15, 25, 50, 100, 200],
    ],
  });
}
