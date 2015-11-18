using System;
using DatabaseManagement;

namespace GUIManagement
{

    using VacationManagement;
    public class AllVacationRequestViewItem
    {

        protected ulong _vacationRequestID;
        protected String _requesterID;
        protected DateTime _vacationStartDate;
        protected DateTime _vacationEndDate;
        protected int _netVacationDays;
        protected String _requesterFullName;
        protected VacationRequestProcessingState _vacationRequestProcessingState;
        protected DateTime _modificationDate;


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
            { return _vacationEndDate.ToShortDateString(); }
        }

        public String VacationRequestProcessingStateViewString
        {
            get { return VacationRequestProcessingStateConverter.ConvertStateToString(_vacationRequestProcessingState); }
        }

        public VacationRequestProcessingState VacationRequestProcessingState
        {
            get { return _vacationRequestProcessingState; }
        }

        public String RequesterFullName
        {
            get { return _requesterFullName; }
        }


        public String NetVacationDaysViewString
        {
            get { return Convert.ToString(_netVacationDays); }
        }

        public String EmployeeID
        {
            get { return _requesterID; }

        }

        public String ModificationDateViewString
        {
            get
            { return _modificationDate.ToShortDateString(); }
        }


        public AllVacationRequestViewItem(VacationRequest vacationRequest, String requesterFullName)
        {
            _vacationRequestID = vacationRequest.getVacationRequestID();
            _vacationStartDate = vacationRequest.getVacationStartDate();
            _vacationEndDate = vacationRequest.getVacationEndDate();
            _requesterID = vacationRequest.getEmployeeID();
            _netVacationDays = vacationRequest.getNetVacationDays();
            _vacationRequestProcessingState = vacationRequest.getVacationRequestProcessingState();
            _requesterFullName = requesterFullName;
            _modificationDate = vacationRequest.getModificationDate();
        }

        public AllVacationRequestViewItem()
        {

        }
    }
}
