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

        public int ShiftGroupID
        {
            get { return _shiftGroupID; }

        }

        public int getShiftGroupID()
        {
            return _shiftGroupID;
        }

        [DisplayName("Schichtpartner 1")]
        public String EmployeeID01
        {
            get { return _employeeID01; }
        }

        [DisplayName("Schichtpartner 2")]
        public String EmployeeID02
        {
            get { return _employeeID02; }
        }

        public ShiftGroup(int ShiftGroupID, String EmployeeID01, String EmployeeID02)
        { //without Role and StaffID!
            _shiftGroupID = ShiftGroupID;
            _employeeID01 = EmployeeID01;
            _employeeID02 = EmployeeID02;
        }

    }

}