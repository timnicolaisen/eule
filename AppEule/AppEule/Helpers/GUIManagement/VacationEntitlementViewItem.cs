using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GUIManagement
{
    public class VacationEntitlementViewItem
    {
        private String _employeeID;
        private String _employeeFullName;
        private int _remainingVacationDays;
        private int _vacationDaysTotal;
        private int _vacationDaysPreviousYear;
        private int _vacationDaysCurYear;
        const String DisplayNameCurYear = "Urlaubsanspruch 2016";
        private IEnumerable<VacationEntitlementViewItem> _vacationEntitlementList;


        public String EmployeeID
        {
            get { return _employeeID; }
        }

        [DisplayName("Mitarbeiter")]
        public String EmployeeFullName
        {
            get { return _employeeFullName; }
        }

        [DisplayName("Verbleibende Urlaubstage")]
        public int RemainingVacationDays
        {
            get { return _remainingVacationDays; }
            set { _remainingVacationDays = value; }
        }

        [DisplayName(VacationEntitlementViewItem.DisplayNameCurYear)]
        public int VacationDaysCurYear
        {
            get { return _vacationDaysCurYear; }
            set { _vacationDaysCurYear = value; }
        }

        [DisplayName(VacationEntitlementViewItem.DisplayNameCurYear)]
        public int VacationDaysTotal
        {
            get { return _vacationDaysTotal; }
            set { _vacationDaysTotal = value; }
        }

        [DisplayName("Urlaubstage Vorjahr")]
        public int VacationDaysPreviousYear
        {
            get { return _vacationDaysPreviousYear; }
            set { _vacationDaysPreviousYear = value; }
        }

      
        public IEnumerable<VacationEntitlementViewItem> VacationEntitlementList
        {
            get { return _vacationEntitlementList; }
            set { _vacationEntitlementList = value; }
           
        }

        public VacationEntitlementViewItem()
        {
            this.RemainingVacationDays = 0;
            this.VacationDaysCurYear = 0;
            this.VacationDaysTotal = 0;
            this.VacationDaysPreviousYear = 0;
        }

        public VacationEntitlementViewItem(int VacationDaysPreviousYear, int VacationDaysCurYear)
        {
            this._vacationDaysCurYear = VacationDaysCurYear;
            this._vacationDaysPreviousYear = VacationDaysPreviousYear;
        }


        public VacationEntitlementViewItem(String EmployeeID, int RemainingVacationDays, int VacationDaysTotal, int VacationDaysPreviousYear)
        {
            this._employeeID = EmployeeID;
            this._remainingVacationDays = RemainingVacationDays;
            this._vacationDaysTotal = VacationDaysTotal;
            this._vacationDaysPreviousYear = VacationDaysPreviousYear;
        }

        public VacationEntitlementViewItem(String EmployeeID, int RemainingVacationDays, int VacationDaysTotal, int VacationDaysPreviousYear, String EmployeeFullName)
        {
            this._employeeID = EmployeeID;
            this._remainingVacationDays = RemainingVacationDays;
            this._vacationDaysTotal = VacationDaysTotal;
            this._vacationDaysPreviousYear = VacationDaysPreviousYear;
            this._employeeFullName = EmployeeFullName;
        }
    }
}