using System;
using DatabaseManagement;
using System.ComponentModel;
using System.Web.Mvc;

namespace GUIManagement
{
    using System.Collections.Generic;
    using VacationManagement;

    public class EmployeeDetailsViewItem
    {
        protected string _id = "";
        protected string _userName = "";
        protected string _firstName = "";
        protected string _lastName = "";
        protected string _email = "";
        protected string _roleName = "";
        protected string _shiftGroupPartnerName = "";
        protected string _divisonName = "";
        protected List<SelectListItem> items = null;




        [DisplayName("Mitarbeiter ID")]
        public string UserId
        {
            get { return _id; }
        }

        [DisplayName("Benutzername")]
        public string UserName
        {
            get { return _userName; }
        }

        [DisplayName("Vorname")]
        public string FirstName
        {
            get { return _firstName; }
        }

        [DisplayName("Nachname")]
        public string LastName
        {
            get { return _lastName; }
        }

        [DisplayName("E-Mail")]
        public string Email
        {
            get { return _email; }
        }

        [DisplayName("Benutzerrolle")]
        public string RoleName
        {
            get { return _roleName; }
        }

        [DisplayName("Schichtgruppenpartner")]
        public string ShiftGroupPartnerName
        {
            get { return _shiftGroupPartnerName; }
        }

        [DisplayName("Abteilung")]
        public string DivisonName
        {
            get { return _divisonName; }
        }



        public EmployeeDetailsViewItem(String UserId , String UserName, String FirstName, String LastName, String Email, String RoleName, String ShiftGroupPartnerName, String DivisionName)
        {
            _id = UserId;
            _userName = UserName;
            _firstName = FirstName;
            _lastName = LastName;
            _email = Email;
            _roleName = RoleName;
            _shiftGroupPartnerName = ShiftGroupPartnerName;
            _divisonName = DivisionName;

        }

    }
}