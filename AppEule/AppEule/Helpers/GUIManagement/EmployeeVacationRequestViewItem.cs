using System;
using DatabaseManagement;

namespace GUIManagement
{

    using VacationManagement;
    public class EmployeeVacationRequestViewItem
    {

        protected ulong _vacationRequestID;
        protected String _requesterID;
        protected DateTime _vacationStartDate;
        protected DateTime _vacationEndDate;
        protected int _netVacationDays;
        protected String _shiftPartnerFullName;
        protected VacationRequestProcessingState _vacationRequestProcessingState;

        public ulong VacationRequestID
        {
            get { return _vacationRequestID; }
        }

        public String VacationStartDateViewString
        {
            get
            { return _vacationStartDate.ToShortDateString(); }
        }

        public String VacationEndDateViewString
        {
            get
            {  return _vacationEndDate.ToShortDateString(); }
        }

        public String VacationRequestProcessingStateViewString
        {
            get {  return VacationRequestProcessingStateConverter.ConvertStateToString(_vacationRequestProcessingState); }
        }

        public VacationRequestProcessingState VacationRequestProcessingState
        {
            get { return _vacationRequestProcessingState; }
        }

        public String ShiftPartnerFullName
        {
            get {  return _shiftPartnerFullName; }
        }


        public String NetVacationDaysViewString
        {
            get {  return Convert.ToString(_netVacationDays);  }
        }

        public String EmployeeID 
        {
            get { return _requesterID; }
        
        }

        public EmployeeVacationRequestViewItem(VacationRequest vacationRequest, String shiftPartnerFullName)
        {
            _vacationRequestID = vacationRequest.getVacationRequestID();
            _vacationStartDate = vacationRequest.getVacationStartDate();
            _vacationEndDate = vacationRequest.getVacationEndDate();
            _requesterID = vacationRequest.getEmployeeID();
            _netVacationDays = vacationRequest.getNetVacationDays();
            _vacationRequestProcessingState = vacationRequest.getVacationRequestProcessingState();
            _shiftPartnerFullName = shiftPartnerFullName;
        }

        public EmployeeVacationRequestViewItem()
        {

        }
    }
}
