using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VacationManagement.Tests
{
    [TestClass()]
    public class EmployeeTests
    {
        private String _employeeID;
        private String _loginName;
        private String _loginPassword;
        private String _lastName;
        private String _firstName;
        private String _emailAddress;
        private int _staffID;
        private String _role;
        private int _shiftGroupID;
        private int _divisionID;

        [TestMethod()]
        public void EmployeeLogin1()
        {
            _employeeID = "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43";               
            _loginName = "Login1";
            _loginPassword = "ADkWVNwBwSGUDBGcEn0UbbX1c6Cek0EZi2nTTIaIUDHi75L7OHbS7xg4UVvhg3q3/w==";
            _lastName = "Nachname1";
            _firstName = "Vorname1";
            _emailAddress = "Login1@tim-n.de";
            _role = "Mitarbeiter";
            _shiftGroupID = 1;
            _divisionID = 1;
        }

        [TestMethod()]
        public void EmployeeTest1()
        {

        }
    }
}
