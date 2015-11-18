//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppEule
{
    using System;
    using System.Collections.Generic;
    
    public partial class VacationRequest
    {
        public VacationRequest()
        {
            this.VacationReplacementRequests = new HashSet<VacationReplacementRequest>();
        }
    
        public long VacationRequestID { get; set; }
        public System.DateTime VacationStartDate { get; set; }
        public System.DateTime VacationEndDate { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public string VacationType { get; set; }
        public string VacationProcessingState { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<bool> VacationPeriodOverlapNote { get; set; }
        public Nullable<bool> VacationLockPeriodNote { get; set; }
        public string EmployeeID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<VacationReplacementRequest> VacationReplacementRequests { get; set; }
    }
}
