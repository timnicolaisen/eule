using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEule.Models
{
    using System;
    using System.Collections.Generic;

    public partial class VacationLockPeriod
    {
        public int LockPeriodID { get; set; }
        public System.DateTime LockPeriodStartDate { get; set; }
        public System.DateTime LockPeriodEndDate { get; set; }
        public int DivisionID { get; set; }

        public virtual Division Division { get; set; }
    }
}