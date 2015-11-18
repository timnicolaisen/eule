using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIManagement
{
    using VacationManagement;

    public class VacationRequestProcessingStateConverter
    {

        private const string SUBMITTED = "offen";
        private const string AGREED = "zugestimmt";
        private const string PERMITTED = "befürwortet";
        private const string APPROVED = "genehmigt";
        private const string REJECTED_BY_DEPUTY = "abgelehnt durch Schichtpartner";
        private const string REJECTED_BY_DIVISION_MANAGER = "abgelehnt durch Bereichsleiter";
        private const string CANCELED = "storniert";
        private const string TAKEN = "genommen";
        private const string UNKNOWN = "unbekannt";

        public static String ConvertStateToString(VacationRequestProcessingState ProcessingState)
        {
            String result = "unbekannt";

            switch (ProcessingState)
            {
                case VacationRequestProcessingState.agreed: result = AGREED; break;
                case VacationRequestProcessingState.approved: result = APPROVED; break;
                case VacationRequestProcessingState.canceled: result = CANCELED; break;
                case VacationRequestProcessingState.permitted: result = PERMITTED; break;
                case VacationRequestProcessingState.rejectedByDeputy: result = REJECTED_BY_DEPUTY; break;
                case VacationRequestProcessingState.rejectedByDivisionManager: result = REJECTED_BY_DIVISION_MANAGER; break;
                case VacationRequestProcessingState.submitted: result = SUBMITTED; break;
                case VacationRequestProcessingState.taken: result = TAKEN; break;
                default: result = UNKNOWN; break;
            }
            return result;
        }
    }
}