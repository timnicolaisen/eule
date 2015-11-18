using VacationManagement;

namespace GUIManagement
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DatabaseManagement;
    using GUIManagement;

    public class PendingVacationRequestViewItem
    {
        private ulong _vacationRequestID;
        private DateTime _vacationStartDate;
        private DateTime _vacationEndDate;
        private int _netVacationDays;
        private VacationRequestProcessingState _vacationRequestProcessingState;
        private String _requesterID;
        private String _requesterFullName;
        private String _deputyFullName;
        private Boolean _vacationPeriodOverlapNote;
        private Boolean _vacationLockPeriodNote;
        private int _remainingVacationDays;

        public ulong VacationRequestID
        {
            get 
            { 
                return _vacationRequestID; 
            }
        }

        public String VacationRequestIDViewString
        {
            get
            {
                return _vacationRequestID.ToString();
            }
        }

        public String VacationStartDateViewString
        {
            get
            { return _vacationStartDate.ToShortDateString(); }
        }

        public DateTime getVacationStartDate()
        {
            return _vacationStartDate;
        }

        public String VacationEndDateViewString
        {
            get
            { return _vacationEndDate.ToShortDateString(); }
        }

        public DateTime VacationEndDate()
        {
            return _vacationEndDate;
        }
        public String NetVacationDaysViewString
        {
            get
            { return Convert.ToString(_netVacationDays); }
        }

        public VacationRequestProcessingState VacationRequestProcessingState
        {
            get
            {
                return _vacationRequestProcessingState;
            }
        }

        public String VacationRequestProcessingStateViewString
        {
            get
            { return VacationRequestProcessingStateConverter.ConvertStateToString(_vacationRequestProcessingState); }
        }

        public String RequesterID
        {
            get { return _requesterID; }
        }

        public String DeputyFullName
        {
            get  {  return _deputyFullName; }
        }

        public String RequesterFullName
        {
            get { return _requesterFullName; }
  //        set { _requesterFullName = value; }
        }

        public Boolean VacationPeriodOverlapNote
        {
            get
            {
                return _vacationPeriodOverlapNote;
            }
        }

        public String RemainingVacationDaysViewString
        {
            get
            { return _remainingVacationDays.ToString(); }
        }
 
        public Boolean VacationLockPeriodNote
        {
            get
            {
                return _vacationLockPeriodNote;
            }
        }

 


        public PendingVacationRequestViewItem(VacationRequest vacationRequest, String requesterName, String deputyName, int remainingVacationDays)
        {
            this._vacationRequestID = vacationRequest.getVacationRequestID();
            this._requesterID = vacationRequest.getEmployeeID();
            this._vacationStartDate = vacationRequest.getVacationStartDate();
            this._vacationEndDate = vacationRequest.getVacationEndDate();
            this._netVacationDays = vacationRequest.getNetVacationDays();
            this._vacationRequestProcessingState = vacationRequest.getVacationRequestProcessingState();
            this._requesterFullName = requesterName;
            this._deputyFullName = deputyName;
            this._vacationPeriodOverlapNote = vacationRequest.getVacationPeriodOverlapNote();
            this._vacationLockPeriodNote = vacationRequest.getVacationLockPeriodNote();
            this._remainingVacationDays = remainingVacationDays;
        }

        //public PendingVacationRequestViewItem(VacationRequest vacationRequest, String requesterName, String shiftPartnerFullName)
        //    : base(vacationRequest, shiftPartnerFullName)
        //{
        //    _requesterFullName = requesterName;
        //}
    }
}