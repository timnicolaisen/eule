using System.ComponentModel;

namespace VacationManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class ShiftGroup
    {
        private int _shiftGroupID;
        private string _employeeID01;
        private string _employeeID02;

        public int ShiftGroupID { get; set; }
        public string EmployeeID01 { get; set; }
        public string EmployeeID02 { get; set; }

        public ShiftGroup(int ShiftGroupID, String EmployeeID01, String EmployeeID02)
        { //without Role and StaffID!
            _shiftGroupID = ShiftGroupID;
            _employeeID01 = EmployeeID01;
            _employeeID02 = EmployeeID02;
        }

    }

}