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
        private string _employeeID01_FullName;
        private string _employeeID02_FullName;

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

        
        public String EmployeeID01
        {
            get { return _employeeID01; }
            set { _employeeID01 = value; }
        }

        [DisplayName("Schichtpartner 1")]
        public String EmployeeID01_FullName
        {
            get { return _employeeID01_FullName; }
            set { _employeeID01_FullName = value; }
        }

       
        public String EmployeeID02
        {
            get { return _employeeID02; }
            set { _employeeID02 = value; }
        }

        [DisplayName("Schichtpartner 2")]
        public String EmployeeID02_FullName
        {
            get { return _employeeID02_FullName; }
            set { _employeeID02_FullName = value; }
        }

        public ShiftGroup(int ShiftGroupID, String EmployeeID01, String EmployeeID02)
        { 
            _shiftGroupID = ShiftGroupID;
            _employeeID01 = EmployeeID01;
            _employeeID02 = EmployeeID02;
        }

        public ShiftGroup(String EmployeeID01, String EmployeeID02)
        { 
            _employeeID01 = EmployeeID01;
            _employeeID02 = EmployeeID02;
        }

        public ShiftGroup(int ShiftGroupID, String EmployeeID01, String EmployeeID02, String EmployeeID01_FullName, String EmployeeID02_FullName)
        {
            _shiftGroupID = ShiftGroupID;
            _employeeID01 = EmployeeID01;
            _employeeID02 = EmployeeID02;
            _employeeID01_FullName = EmployeeID01_FullName;
            _employeeID02_FullName = EmployeeID02_FullName;

        }

        public ShiftGroup()
        { 
            _employeeID01 = "Default";
            _employeeID02 = "Default";
        }
       
    }

}