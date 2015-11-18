using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace VacationManagement.Tests
{
    [TestClass()]
    public class VacationRequestTests
    {
        const String DEFAULT_VACATION_TYPE = "Erholungsurlaub";
        const String CONFIRMATION_EMAIL_SUBMISSION = "SUBMISSION";

        private ulong _vacationRequestID;
        private DateTime _vacationStartDate;
        private DateTime _vacationEndDate;
        private DateTime _submissionDate;
        private String _vacationType;
        private VacationRequestProcessingState _vacationRequestProcessingState;
        private DateTime _modificationDate;
        private Boolean _vacationPeriodOverlapNote;
        private Boolean _vacationLockPeriodNote;
        private String _employeeID;

        [TestMethod()]
        public void getVacationRequestIDTest()
        {

        }

        [TestMethod()]
        public void getVacationStartDateTest()
        {

        }

        [TestMethod()]
        public void getVacationEndDateTest()
        {

        }

        [TestMethod()]
        public void setVacationPeriodOverlapNoteTest()
        {

        }

        [TestMethod()]
        public void setVacationLockPeriodNoteTest()
        {

        }

        [TestMethod()]
        public void getEmployeeIDTest()
        {

        }

        [TestMethod()]
        public void InsertIntoDBTest()
        {

        }

        [TestMethod()]
        public void VacationRequestTest()
        {

        }

        [TestMethod()]
        public void VacationRequestTest1()
        {

        }

        [TestMethod()]
        public void DetermineWorkingDaysInVacationRequestTest()
        {

        }
    }
}
