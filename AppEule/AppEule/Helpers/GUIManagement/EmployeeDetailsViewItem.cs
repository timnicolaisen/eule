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
        public IEnumerable<List<SelectListItem>> _listOfAllEmployee { get; set; }
        public IEnumerable<SelectListItem> FullName { get; set; }




        [DisplayName("Mitarbeiter ID")]
        public string UserId
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Benutzername")]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        [DisplayName("Vorname")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [DisplayName("Nachname")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [DisplayName("E-Mail")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DisplayName("Benutzerrolle")]
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }

        [DisplayName("Schichtgruppenpartner")]
        public string ShiftGroupPartnerName
        {
            get { return _shiftGroupPartnerName; }
            set { _shiftGroupPartnerName = value; }
        }

        [DisplayName("Abteilung")]
        public string DivisonName
        {
            get { return _divisonName; }
            set { _divisonName = value; }
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

        public EmployeeDetailsViewItem()
        {

        }

        }
}