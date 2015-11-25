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
            set { _shiftGroupID = value; }

        }

        public int getShiftGroupID()
        {
            return _shiftGroupID;
        }

        public String getEmployeeID01()
        {
            return _employeeID01;
        }

        public String getEmployeeID02()
        {
            return _employeeID02;
        }

        [DisplayName("Schichtpartner 1")]
        public String EmployeeID01
        {
            get { return _employeeID01; }
            set { _employeeID01 = value; }
        }

        [DisplayName("Schichtpartner 2")]
        public String EmployeeID02
        {
            get { return _employeeID02; }
            set { _employeeID02 = value; }
        }

        public ShiftGroup(int ShiftGroupID, String EmployeeID01, String EmployeeID02)
        { //without Role and StaffID!
            _shiftGroupID = ShiftGroupID;
            _employeeID01 = EmployeeID01;
            _employeeID02 = EmployeeID02;
        }

        public ShiftGroup(String EmployeeID01, String EmployeeID02)
        { //without Role and StaffID!
            _employeeID01 = EmployeeID01;
            _employeeID02 = EmployeeID02;
        }

        public ShiftGroup()
        { //without Role and StaffID!
            _employeeID01 = "Default";
            _employeeID02 = "Default";
        }
       
    }

}