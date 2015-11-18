using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIManagement
{
    public class VacationEntitlementViewItem
    {
        private String _employeeID;
        private int _remainingVacationDays;
        private int _vacationDaysTotal;
        private int _vacationDaysPreviousYear;

        public String EmployeeID
        {
            get { return _employeeID; }
        }

        public int RemainingVacationDays
        {
            get { return _remainingVacationDays; }
            set { _remainingVacationDays = value; }
        }

        public int VacationDaysTotal
        {
            get { return _vacationDaysTotal; }
            set { _vacationDaysTotal = value; }
        }

        public int VacationDaysPreviousYear
        {
            get { return _vacationDaysPreviousYear; }
            set { _vacationDaysPreviousYear = value; }
        }

        public VacationEntitlementViewItem()
        {

        }

        public VacationEntitlementViewItem(String EmployeeID, int RemainingVacationDays, int VacationDaysTotal, int VacationDaysPreviousYear)
        {
            this._employeeID = EmployeeID;
            this._remainingVacationDays = RemainingVacationDays;
            this._vacationDaysTotal = VacationDaysTotal;
            this._vacationDaysPreviousYear = VacationDaysPreviousYear;
        }
    }
}