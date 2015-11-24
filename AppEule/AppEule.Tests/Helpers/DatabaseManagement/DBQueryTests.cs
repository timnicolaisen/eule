using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using AppEule.Tests.Helpers.DatabaseManagement;
using DatabaseManagement;
using GUIManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using VacationManagement;

namespace DatabaseManagement.Tests
{
    using VacationManagement;

    [TestClass()]
    public class DBQueryTests
    {
        public const string sqlConnectionString =
            "Data Source=v22015112828829481.yourvserver.net;Initial Catalog=EULE_ASP;Persist Security Info=True;User ID=sa;Password=Ti#2#7#m";

        public const string SUBMITTED = "offen";
        public const string AGREED = "zugestimmt";
        public const string PERMITTED = "befürwortet";
        public const string APPROVED = "genehmigt";
        public const string REJECTED_BY_DEPUTY = "abgelehnt durch Schichtpartner";
        public const string REJECTED_BY_DIVISION_MANAGER = "abgelehnt durch Bereichsleiter";
        public const string CANCELED = "storniert";
        public const string TAKEN = "genommen";

        [TestMethod()]
        public void SelectHolidaysTest_F5015_K_1()
        {
            int intdays = 0; // return value


            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT Count(*) AS 'DayCount' FROM dbo.Calendar WHERE (Date BETWEEN @VacationStartDate AND @VacationEndDate) AND DayType IN ('Feiertag', 'Betriebsruhetag') AND Weekday NOT IN ('Samstag','Sonntag')";

                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("VacationStartDate", "2015-02-01");
                    cmd.Parameters.AddWithValue("VacationEndDate", "2015-05-01");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {

                                // To avoid unexpected bugs access columns by name.
                                intdays = reader.GetInt32(reader.GetOrdinal("DayCount"));
                            }
                        }

                    }

                }
                Assert.AreEqual(3, intdays);
            }

        }

        [TestMethod()]
        public void SelectHolidaysTest_F5015_K_2()
        {
            int intdays = 0; // return value


            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT Count(*) AS 'DayCount' FROM dbo.Calendar WHERE (Date BETWEEN @VacationStartDate AND @VacationEndDate) AND DayType IN ('Feiertag', 'Betriebsruhetag') AND Weekday NOT IN ('Samstag','Sonntag')";

                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("VacationStartDate", "2015-02-01");
                    cmd.Parameters.AddWithValue("VacationEndDate", "2015-02-01");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {

                                // To avoid unexpected bugs access columns by name.
                                intdays = reader.GetInt32(reader.GetOrdinal("DayCount"));
                            }
                        }

                    }

                }
                Assert.AreEqual(0, intdays);
            }

        }


        [TestMethod()]
        public void SelectHolidaysTest_F5015_K_3()
        {
            int intdays = 0; // return value


            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT Count(*) AS 'DayCount' FROM dbo.Calendar WHERE (Date BETWEEN @VacationStartDate AND @VacationEndDate) AND DayType IN ('Feiertag', 'Betriebsruhetag') AND Weekday NOT IN ('Samstag','Sonntag')";

                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("VacationStartDate", "2015-02-01");
                    cmd.Parameters.AddWithValue("VacationEndDate", "2020-02-01");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {

                                // To avoid unexpected bugs access columns by name.
                                intdays = reader.GetInt32(reader.GetOrdinal("DayCount"));
                            }
                        }

                    }

                }

                Assert.AreEqual(37, intdays);
              
            }

        }

        [TestMethod()]
        public void SelectEmployeeVacationRequestInTimePeriodTest_F5201_K_1()
        {
            var timelist = new List<Tuple<DateTime, DateTime>>();
            DateTime StartTmp = new DateTime();
            DateTime EndTmp = new DateTime();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT VacationStartDate, VacationEndDate FROM VacationRequest WHERE EmployeeID = @EmployeeID AND ((@VacationStartDate BETWEEN VacationStartDate AND VacationEndDate) OR (@VacationEndDate BETWEEN VacationStartDate AND VacationEndDate))";

                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                    cmd.Parameters.AddWithValue("VacationStartDate", "20150710");
                    cmd.Parameters.AddWithValue("VacationEndDate", "20150724");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Returns into temp variables and insert them into the list
                                StartTmp = reader.GetDateTime(reader.GetOrdinal("VacationStartDate"));
                                EndTmp = reader.GetDateTime(reader.GetOrdinal("VacationEndDate"));
                                timelist.Add(Tuple.Create(StartTmp, EndTmp));
                            }
                        }

                    }

                }
            }
            var testlist = new List<Tuple<DateTime, DateTime>>();
            DateTime Start = new DateTime(2015, 07, 01);
            DateTime End = new DateTime(2015, 07, 31);
            Tuple<DateTime, DateTime> Tupel = new Tuple<DateTime, DateTime>(Start, End);
            testlist.Add(Tupel);

            CollectionAssert.AreEqual(testlist, timelist);

        }

        [TestMethod()]
        public void SelectLockPeriodsTest_F5018_K_1()
        {
            var locklist = new List<Tuple<DateTime, DateTime>>();
            DateTime StartlockTmp = new DateTime();
            DateTime EndlockTmp = new DateTime();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT LockPeriodStartDate, LockPeriodEndDate FROM dbo.VacationLockPeriod WHERE (@VacationStartDate BETWEEN LockPeriodStartDate AND LockPeriodEndDate) OR (@VacationEndDate BETWEEN LockPeriodStartDate AND LockPeriodEndDate)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("VacationStartDate", "20150605");
                    cmd.Parameters.AddWithValue("VacationEndDate", "20150608");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Returns into temp variables and insert them into the list
                                StartlockTmp = reader.GetDateTime(reader.GetOrdinal("LockPeriodStartDate"));
                                EndlockTmp = reader.GetDateTime(reader.GetOrdinal("LockPeriodEndDate"));
                                locklist.Add(Tuple.Create(StartlockTmp, EndlockTmp));
                            }
                        }

                    }

                }
            }
            var testlist = new List<Tuple<DateTime, DateTime>>();
            DateTime Start = new DateTime(2015, 06, 01);
            DateTime End = new DateTime(2015, 06, 10);
            Tuple<DateTime, DateTime> Tupel = new Tuple<DateTime, DateTime>(Start, End);
            testlist.Add(Tupel);

            CollectionAssert.AreEqual(testlist, locklist);

        }

        [TestMethod()]
        public void SelectRemainingVacationDaysTest_F5014_K_1()
        {
            int RemainingDays = 0;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT VacationDayRemaining FROM dbo.VacationEntitlement WHERE EmployeeID = @EmployeeID";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Return into variable
                                RemainingDays = reader.GetInt32(reader.GetOrdinal("VacationDayRemaining"));
                            }
                        }

                    }

                }
            }
            int actual = 35;
            Assert.AreEqual(actual, RemainingDays);
        }


        [TestMethod()]
        public void InsertNewVacationRequestTest_F5203_K_1()
        {
            bool Result = false;
            TestDBReset Reset = new TestDBReset();
            ulong VacationRequestID = 100;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "INSERT INTO [dbo].[VacationRequest] VALUES (" + @VacationRequestID +
                    ", @VacationStartDate, @VacationEndDate, @SubmissionDate, @VacationType, @VacationProcessingState, @ModificationDate, @VacationPeriodOverlapNote, @VacationLockPeriodNote, @NetVacationDays, @EmployeeID)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("VacationStartDate", "20150301");
                    cmd.Parameters.AddWithValue("VacationEndDate", "20151202");
                    cmd.Parameters.AddWithValue("SubmissionDate", "2015-06-03 00:00:00.000");
                    cmd.Parameters.AddWithValue("VacationType", "Erholungsurlaub");
                    cmd.Parameters.AddWithValue("VacationProcessingState", "genehmigt");
                    cmd.Parameters.AddWithValue("ModificationDate", "2015-06-03 12:25:47.933");
                    cmd.Parameters.AddWithValue("VacationPeriodOverlapNote", 0);
                    cmd.Parameters.AddWithValue("VacationLockPeriodNote", 0);
                    cmd.Parameters.AddWithValue("NetVacationDays", 20);
                    cmd.Parameters.AddWithValue("EmployeeID", "f7bb0ab3-728b-4053-9b83-1b159a9d83a8");

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Result = true;

                    }
                    catch (SqlException e)
                    {
                        {
                            Result = false;
                        }
                    }
                }
            }
            if (Result == true)
            {

                if (Reset.ResetTestDB())
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            bool actual = true;
            Assert.AreEqual(actual, Result);
        }

       




        [TestMethod()]
        public void SelectDivisionManagerTest_F5206_K_1()
        {
            string Idtmp = "c";
            string Usernametmp = "c";
            string PasswordHashtmp = "c";
            string FirstNametmp = "c";
            string LastNametmp = "c";
            string Emailtmp = "c";
            int ShiftGroupIDtmp = 0;
            int DivisionIDtmp = 0;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT Id, Username, PasswordHash, LastName, FirstName, Email, ShiftGroupID, DivisionID FROM dbo.AspNetUsers WHERE Id =" +
                    "(SELECT DivisionManagerID FROM dbo.Division WHERE DivisionID = (SELECT DivisionID FROM dbo.AspNetUsers WHERE Id = @EmployeeID))";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Return into variable
                                // wihout Role and StaffID
                                Idtmp = reader.GetString(reader.GetOrdinal("Id"));
                                Usernametmp = reader.GetString(reader.GetOrdinal("Username"));
                                PasswordHashtmp = reader.GetString(reader.GetOrdinal("PasswordHash"));
                                FirstNametmp = reader.GetString(reader.GetOrdinal("FirstName"));
                                LastNametmp = reader.GetString(reader.GetOrdinal("LastName"));
                                Emailtmp = reader.GetString(reader.GetOrdinal("Email"));
                                if (reader.IsDBNull(reader.GetOrdinal("ShiftGroupID")))
                                    //Test, ob ID NULL, wenn ja ID = 0
                                {
                                    ShiftGroupIDtmp = 0;
                                }
                                else
                                {
                                    ShiftGroupIDtmp = reader.GetInt32(reader.GetOrdinal("ShiftGroupID"));
                                }
                                DivisionIDtmp = reader.GetInt32(reader.GetOrdinal("DivisionID"));
                            }
                        }

                    }

                }
            }
            //Create list instead of the object 'DivisionManager' to make it comparable
            List<object> actual = new List<object>();
            actual.Add(Idtmp);
            actual.Add(Usernametmp);
            actual.Add(PasswordHashtmp);
            actual.Add(LastNametmp);
            actual.Add(FirstNametmp);
            actual.Add(Emailtmp);
            actual.Add(ShiftGroupIDtmp);
            actual.Add(DivisionIDtmp);


            //Create list of expected data
            List<object> expected = new List<object>();
            expected.Add("f7bb0ab3-728b-4053-9b83-1b159a9d83a8");
            expected.Add("Login3");
            expected.Add("AIXF/Lh8T0g5XJA8HE8BIJdZTTbhiSjj5ni0mKc64FeaV3VNfn4VZOk5piLmVih4Ug==");
            expected.Add("Nachname3");
            expected.Add("Vorname3");
            expected.Add("Login3@tim-n.de");
            expected.Add(0);
            expected.Add(1);
       


            //Compare the created lists
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SelectDeputyTest_F5019_K_1()
        {
            {
                string Idtmp = "c";
                string Usernametmp = "c";
                string PasswordHashtmp = "c";
                string FirstNametmp = "c";
                string LastNametmp = "c";
                string Emailtmp = "c";
                int ShiftGroupIDtmp = 0;
                int DivisionIDtmp = 0;
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sqlStatement =
                        "SELECT Id, Username, PasswordHash, LastName, FirstName, Email, ShiftGroupID, DivisionID FROM dbo.AspNetUsers WHERE ShiftGroupID = (SELECT ShiftGroupID FROM dbo.AspNetUsers WHERE Id = @EmployeeID) AND Id != @EmployeeID";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if the reader has any rows at all before starting to read.
                            if (reader.HasRows)
                            {
                                // Read advances to the next row.
                                while (reader.Read())
                                {
                                    // Save DB-Return into variables
                                    // wihout Role and StaffID
                                    Idtmp = reader.GetString(reader.GetOrdinal("Id"));
                                    Usernametmp = reader.GetString(reader.GetOrdinal("Username"));
                                    PasswordHashtmp = reader.GetString(reader.GetOrdinal("PasswordHash"));
                                    FirstNametmp = reader.GetString(reader.GetOrdinal("FirstName"));
                                    LastNametmp = reader.GetString(reader.GetOrdinal("LastName"));
                                    Emailtmp = reader.GetString(reader.GetOrdinal("Email"));
                                    if (reader.IsDBNull(reader.GetOrdinal("ShiftGroupID")))
                                        //if ID NULL, then ID = 0, no Shiftgroup 0 in database
                                    {
                                        ShiftGroupIDtmp = 0; // 0 means, no Shiftgroup for that Employee
                                    }
                                    else
                                    {
                                        ShiftGroupIDtmp = reader.GetInt32(reader.GetOrdinal("ShiftGroupID"));
                                    }
                                    DivisionIDtmp = reader.GetInt32(reader.GetOrdinal("DivisionID"));
                                }
                            }

                        }

                    }
                } //Create list instead of the object 'Deputy' to make it comparable
                List<object> actual = new List<object>();
                actual.Add(Idtmp);
                actual.Add(Usernametmp);
                actual.Add(PasswordHashtmp);
                actual.Add(LastNametmp);
                actual.Add(FirstNametmp);
                actual.Add(Emailtmp);
                actual.Add(ShiftGroupIDtmp);
                actual.Add(DivisionIDtmp);


                // Create list of expected data
                List<object> expected = new List<object>();
                expected.Add("b03a5057-7505-4d5e-997f-efc0b40ed6e0"); // tested with "Login2"
                expected.Add("Login2");
                expected.Add("AMwipYYgMsGCx+p/KtU20DwWkhb2PMRRGlc2oqzNsXpsugQS2FiIR0QHsUllTsJpqA==");
                expected.Add("Nachname2");
                expected.Add("Vorname2");
                expected.Add("Login2@tim-n.de");
                expected.Add(1);
                expected.Add(1);




                CollectionAssert.AreEqual(expected, actual);

            }
        }

        [TestMethod()]
        public void SelectAllVacationRequestsOfEmployeeTest_F5031_K_1()
        {
            long convertbigint = 0;
            DateTime VacationStartDatetmp = new DateTime(2000, 1, 1);
            DateTime VacationEndDatetmp = new DateTime(2000, 1, 1);
            DateTime SubmissionDatetmp = new DateTime(2000, 1, 1);
            //string instead of VacationRequestProcessingState to make this function independent from ConvertStringToVacationRequestProcessingState()
            string VacationProcessingStatetmp = "offen";
            string VacationTypetmp = "c";
            DateTime ModificationDatetmp = new DateTime(2000, 1, 1);
            Boolean VacationOverlapNotetmp = false;
            Boolean VacationLockPeriodNotetmp = false;
            string EmployeeIDtmp = "c";
            int NetVacationDays = 0;

            var VacList = new List<VacationRequest>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT * FROM dbo.VacationRequest WHERE EmployeeID = @EmployeeID order by VacationStartDate";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeID", "f7bb0ab3-728b-4053-9b83-1b159a9d83a8");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Returns into temp variables and insert them into the list

                                convertbigint = reader.GetInt64(reader.GetOrdinal("VacationRequestID"));
                                ulong VacationRequestIDtmp = Convert.ToUInt64(convertbigint);
                                //VacationRequestID needs to be converted because of DataType confliction
                                VacationStartDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationStartDate"));
                                VacationEndDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationEndDate"));
                                SubmissionDatetmp = reader.GetDateTime(reader.GetOrdinal("SubmissionDate"));
                                VacationTypetmp = reader.GetString(reader.GetOrdinal("VacationType"));
                                VacationProcessingStatetmp =
                                    (reader.GetString(reader.GetOrdinal("VacationProcessingState")));
                                ModificationDatetmp = reader.GetDateTime(reader.GetOrdinal("ModificationDate"));
                                VacationOverlapNotetmp =
                                    reader.GetBoolean(reader.GetOrdinal("VacationPeriodOverlapNote"));
                                VacationLockPeriodNotetmp =
                                    reader.GetBoolean(reader.GetOrdinal("VacationLockPeriodNote"));
                                EmployeeIDtmp = reader.GetString(reader.GetOrdinal("EmployeeID"));
                                NetVacationDays = reader.GetInt32(reader.GetOrdinal("NetVacationDays"));


                            }
                        }
                    }
                }
            }
            //Create list instead of the object 'VacList' to make it comparable
            List<object> actual = new List<object>();
            actual.Add(convertbigint);
            actual.Add(VacationStartDatetmp);
            actual.Add(VacationEndDatetmp);
            actual.Add(SubmissionDatetmp);
            actual.Add(VacationTypetmp);
            actual.Add(VacationProcessingStatetmp);
            actual.Add(ModificationDatetmp);
            actual.Add(VacationOverlapNotetmp);
            actual.Add(VacationLockPeriodNotetmp);
            actual.Add(EmployeeIDtmp);
            actual.Add(NetVacationDays);


            //Create list of expected data
            List<object> expected = new List<object>();
            expected.Add((long) 9);
            expected.Add(new DateTime(2015, 01, 01));
            expected.Add(new DateTime(2015, 01, 31));
            expected.Add(SubmissionDatetmp);
            expected.Add("Erholungsurlaub");
            expected.Add("offen");
            expected.Add(ModificationDatetmp);
            expected.Add(false);
            expected.Add(false);
            expected.Add("f7bb0ab3-728b-4053-9b83-1b159a9d83a8");
            expected.Add(28);



            //Compare the created lists
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DBQueryTest()
        {

        }

        [TestMethod()]
        public void UpdateVacationRequestStatusTest_F5081_K_1()
        {
            TestDBReset Reset = new TestDBReset();
            bool Result = false;
            int UpdateVacationRequestStatusboth = 10;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement = //insert ID like that because of DataType conflicts
                    "UPDATE dbo.VacationRequest SET VacationProcessingState = @VacationProcessingState, ModificationDate = @modificationDate WHERE VacationRequestID = " +
                    UpdateVacationRequestStatusboth + "";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("VacationProcessingState", "ACHTUNG");
                    cmd.Parameters.AddWithValue("modificationDate", "2050-01-01 01:14:34.020");

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery(); //insert,delete,update is NonQuery
                        Result = true;
                    }
                    catch (SqlException e) //if failure in Database result = false
                    {
                        {
                            Result = false;
                        }
                    }
                }
            }
            if (Result == true)
            {

                if (Reset.ResetTestDB())
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }

            bool actual = true;
            Assert.AreEqual(actual, Result);
        }

       


        [TestMethod()]
        public void SelectShiftPartnerTest_F5222_K_1()
        {
            string ShiftPartnerID = "null";
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT Id FROM dbo.AspNetUsers WHERE ShiftGroupID = (SELECT ShiftGroupID FROM dbo.AspNetUsers WHERE Id = @EmployeeID) AND Id != @EmployeeID";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Return into variable
                                ShiftPartnerID = reader.GetString(reader.GetOrdinal("Id"));
                            }
                        }
                    }
                }
            }
            Assert.AreEqual("b03a5057-7505-4d5e-997f-efc0b40ed6e0", ShiftPartnerID);
        }

        [TestMethod()]
        public void SelectRequesterTest_F5210_K_1()
        {
            string Idtmp = "c";
            string Usernametmp = "c";
            string PasswordHashtmp = "c";
            string FirstNametmp = "c";
            string LastNametmp = "c";
            string Emailtmp = "c";
            int ShiftGroupIDtmp = 0;
            int DivisionIDtmp = 0;
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT Id, Username, PasswordHash, LastName, FirstName, Email, ShiftGroupID, DivisionID FROM dbo.AspNetUsers WHERE Id = (SELECT EmployeeID FROM dbo.VacationRequest WHERE VacationRequestID = " +
                    2 + ")";

                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {


                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Return into variables
                                // wihout Role and StaffID
                                Idtmp = reader.GetString(reader.GetOrdinal("Id"));
                                Usernametmp = reader.GetString(reader.GetOrdinal("Username"));
                                PasswordHashtmp = reader.GetString(reader.GetOrdinal("PasswordHash"));
                                FirstNametmp = reader.GetString(reader.GetOrdinal("FirstName"));
                                LastNametmp = reader.GetString(reader.GetOrdinal("LastName"));
                                Emailtmp = reader.GetString(reader.GetOrdinal("Email"));
                                if (reader.IsDBNull(reader.GetOrdinal("ShiftGroupID")))
                                    //if ID NULL, then ID = 0, no Shiftgroup 0 in database
                                {
                                    ShiftGroupIDtmp = 0; // 0 means, no Shiftgroup for that Employee
                                }
                                else
                                {
                                    ShiftGroupIDtmp = reader.GetInt32(reader.GetOrdinal("ShiftGroupID"));
                                }
                                DivisionIDtmp = reader.GetInt32(reader.GetOrdinal("DivisionID"));
                            }
                        }

                    }

                }
            } //Create list instead of the object 'Employee' to make it comparable
            List<object> actual = new List<object>();
            actual.Add(Idtmp);
            actual.Add(Usernametmp);
            actual.Add(PasswordHashtmp);
            actual.Add(LastNametmp);
            actual.Add(FirstNametmp);
            actual.Add(Emailtmp);
            actual.Add(ShiftGroupIDtmp);
            actual.Add(DivisionIDtmp);


            // Create list of expected data
            List<object> expected = new List<object>();
            expected.Add("1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43"); // tested with "Login1"
            expected.Add("Login1");
            expected.Add("ADkWVNwBwSGUDBGcEn0UbbX1c6Cek0EZi2nTTIaIUDHi75L7OHbS7xg4UVvhg3q3/w==");
            expected.Add("Nachname1");
            expected.Add("Vorname1");
            expected.Add("Login1@tim-n.de");
            expected.Add(1);
            expected.Add(1);




            CollectionAssert.AreEqual(expected, actual);

        }



        [TestMethod()]
        public void SelectSubmittedVacationRequestsOfShiftPartnerTest_F5207_K_1() //wird bearbeitet von Daniel, muss noch dokumentiert werden
        {
            ulong VacationRequestIDtmp = 0UL;
            long convertbigint = 0;
            DateTime VacationStartDatetmp = new DateTime(2000, 1, 1);
            DateTime VacationEndDatetmp = new DateTime(2000, 1, 1);
            DateTime SubmissionDatetmp = new DateTime(2000, 1, 1);
            string VacationProcessingStatetmp = "";
            string VacationTypetmp = "c";
            DateTime ModificationDatetmp = new DateTime(2000, 1, 1);
            DateTime Mod = new DateTime(2000, 1, 1);
            DateTime Sub = new DateTime(2000, 1, 1);
            Boolean VacationOverlapNotetmp = false;
            Boolean VacationLockPeriodNotetmp = false;
            string EmployeeIDtmp = "c";
            int NetVacationDays = 0;
            int i = 0;

            var VacList = new List<VacationRequest>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT * FROM dbo.VacationRequest WHERE EmployeeID = @EmployeeID AND VacationProcessingState = 'offen' order by VacationStartDate";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Returns into temp variables and insert them into the list
                                VacationRequestIDtmp = (ulong)reader.GetInt64(reader.GetOrdinal("VacationRequestID"));

                                VacationStartDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationStartDate"));
                                VacationEndDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationEndDate"));
                                SubmissionDatetmp = reader.GetDateTime(reader.GetOrdinal("SubmissionDate"));
                                VacationTypetmp = reader.GetString(reader.GetOrdinal("VacationType"));
                                VacationProcessingStatetmp = reader.GetString(reader.GetOrdinal("VacationProcessingState"));
                                ModificationDatetmp = reader.GetDateTime(reader.GetOrdinal("ModificationDate"));
                                VacationOverlapNotetmp = reader.GetBoolean(reader.GetOrdinal("VacationPeriodOverlapNote"));
                                VacationLockPeriodNotetmp = reader.GetBoolean(reader.GetOrdinal("VacationLockPeriodNote"));
                                EmployeeIDtmp = reader.GetString(reader.GetOrdinal("EmployeeID"));
                                NetVacationDays = reader.GetInt32(reader.GetOrdinal("NetVacationDays"));

                                if (i == 1)
                                {
                                  VacationRequest Vac = new VacationRequest(VacationRequestIDtmp, EmployeeIDtmp, VacationStartDatetmp,
                                    VacationEndDatetmp, SubmissionDatetmp, VacationTypetmp,VacationRequestProcessingState.submitted,
                                    ModificationDatetmp, VacationOverlapNotetmp, VacationLockPeriodNotetmp, NetVacationDays);
                                    Mod = ModificationDatetmp;
                                    Sub = SubmissionDatetmp;
                                    VacList.Add(Vac);
                                }
                                i++;
                            }
                        }
                    }
                }
            }
            VacationRequest Vac1 = new VacationRequest(8, "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43", new DateTime(2015, 09, 01), new DateTime(2015, 09, 01),
                Sub, "Erholungsurlaub", VacationRequestProcessingState.submitted, Mod, false, false, 26);
            
            var VacListExp = new List<VacationRequest>();
            VacListExp.Add(Vac1);
            bool result = false;
            if ((VacList[0].getVacationRequestID() == VacListExp[0].getVacationRequestID())
                && (VacList[0].getEmployeeID() == VacListExp[0].getEmployeeID())
                && (VacList[0].getVacationStartDate() == VacListExp[0].getVacationStartDate())
                && (VacList[0].getVacationEndDate() == VacListExp[0].getVacationEndDate())
                && (VacList[0].getSubmissionDate() == VacListExp[0].getSubmissionDate())
                && (VacList[0].getVacationType() == VacListExp[0].getVacationType())
                && (VacList[0].getVacationRequestProcessingState() == VacListExp[0].getVacationRequestProcessingState())
                && (VacList[0].getModificationDate() == VacListExp[0].getModificationDate())
                && (VacList[0].getVacationPeriodOverlapNote() == VacListExp[0].getVacationPeriodOverlapNote())
                && (VacList[0].getVacationLockPeriodNote() == VacListExp[0].getVacationLockPeriodNote())
                && (VacList[0].getNetVacationDays() == VacListExp[0].getNetVacationDays()))
            {
                result = true;
            }

            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void SelectEmployeesOfDivisionTest_F5208_K_1() 
        {
            string Idtmp = "c";
            string Usernametmp = "c";
            string PasswordHashtmp = "c";
            string FirstNametmp = "c";
            string LastNametmp = "c";
            string Emailtmp = "c";
            int ShiftGroupIDtmp = 0;
            int DivisionIDtmp = 0;
            var EmpList = new List<Employee>();
            List<List<object>> MegaListeDB = new List<List<object>>();
            List<object> MegaListeDB1 = new List<object>();
            List<object> MegaListeDB2 = new List<object>();
            List<object> MegaListeDB3 = new List<object>();
            //ulong VacationRequestIDtmp = Convert.ToUInt64(bla);
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT * FROM dbo.AspNetUsers WHERE DivisionID = (SELECT DivisionID FROM dbo.AspNetUsers WHERE Id = @EmployeeID)";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeID", "b03a5057-7505-4d5e-997f-efc0b40ed6e0");
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Return into variables
                                // wihout Role and StaffID
                                Idtmp = reader.GetString(reader.GetOrdinal("Id"));
                                Usernametmp = reader.GetString(reader.GetOrdinal("Username"));
                                PasswordHashtmp = reader.GetString(reader.GetOrdinal("PasswordHash"));
                                FirstNametmp = reader.GetString(reader.GetOrdinal("FirstName"));
                                LastNametmp = reader.GetString(reader.GetOrdinal("LastName"));
                                Emailtmp = reader.GetString(reader.GetOrdinal("Email"));
                                if (reader.IsDBNull(reader.GetOrdinal("ShiftGroupID")))
                                    //Test, ob ID NULL, wenn ja ID = 0
                                {
                                    ShiftGroupIDtmp = 0;
                                }
                                else
                                {
                                    ShiftGroupIDtmp = reader.GetInt32(reader.GetOrdinal("ShiftGroupID"));
                                }
                                DivisionIDtmp = reader.GetInt32(reader.GetOrdinal("DivisionID"));
                                Employee DivEmployee = new Employee(Idtmp, Usernametmp, PasswordHashtmp, FirstNametmp,
                                    LastNametmp, Emailtmp, ShiftGroupIDtmp, DivisionIDtmp);
                                EmpList.Add(DivEmployee);

                                if (i == 1)    //here you can choice which position you will test
                                {
                                    MegaListeDB1.Add(Idtmp);
                                    MegaListeDB1.Add(Usernametmp);
                                    MegaListeDB1.Add(PasswordHashtmp);
                                    MegaListeDB1.Add(FirstNametmp);
                                    MegaListeDB1.Add(LastNametmp);
                                    MegaListeDB1.Add(Emailtmp);
                                    MegaListeDB1.Add(ShiftGroupIDtmp);
                                    MegaListeDB1.Add(DivisionIDtmp);
                                }



                                i++;
                            }
                    }

                }

            }
        } //Create list instead of the object 'Employee' to make it comparable

    

MegaListeDB.Add(MegaListeDB1);
            List<List<object>> MegaListe = new List<List<object>>();
            List<object> MegaListe1 = new List<object>();
            MegaListe1.Add("b03a5057-7505-4d5e-997f-efc0b40ed6e0");
            MegaListe1.Add("Login2");
            MegaListe1.Add("AMwipYYgMsGCx+p/KtU20DwWkhb2PMRRGlc2oqzNsXpsugQS2FiIR0QHsUllTsJpqA==");
            MegaListe1.Add("Vorname2");
            MegaListe1.Add("Nachname2");
            MegaListe1.Add("Login2@tim-n.de");
            MegaListe1.Add(1);
            MegaListe1.Add(1);
            MegaListe.Add(MegaListe1);
           
            CollectionAssert.AreEqual(MegaListe1,MegaListeDB1);       

        }

        [TestMethod()]
        public void SelectVacationRequestsInStateTest_F5209_K_1()  //muss noch bearbeitet werden
        {
            ulong VacationRequestIDtmp = 0UL;
            DateTime VacationStartDatetmp = new DateTime(2000, 1, 1);
            DateTime VacationEndDatetmp = new DateTime(2000, 1, 1);
            DateTime SubmissionDatetmp = new DateTime(2000, 1, 1);
            string VacationProcessingStatetmp = "";
            string VacationTypetmp = "c";
            DateTime ModificationDatetmp = new DateTime(2000, 1, 1);
            Boolean VacationOverlapNotetmp = false;
            Boolean VacationLockPeriodNotetmp = false;
            string EmployeeIDtmp = "c";
            int NetVacationDays = 0;
            List<String> States = new List<string>();
            States.Add("offen");
            var VacList = new List<VacationRequest>();
            foreach (String item in States)
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sqlStatement =
                        "SELECT * FROM dbo.VacationRequest WHERE EmployeeID = @EmployeeID AND VacationProcessingState = @States order by VacationStartDate";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
                        cmd.Parameters.AddWithValue("States", item);
                        cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if the reader has any rows at all before starting to read.
                            if (reader.HasRows)
                            {
                                // Read advances to the next row.
                                while (reader.Read())
                                {
                                    // Save DB-Returns into temp variables and insert them into the list

                                    VacationRequestIDtmp = (ulong)reader.GetInt64(reader.GetOrdinal("VacationRequestID"));
                                    VacationStartDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationStartDate"));
                                    VacationEndDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationEndDate"));
                                    SubmissionDatetmp = reader.GetDateTime(reader.GetOrdinal("SubmissionDate"));
                                    VacationTypetmp = reader.GetString(reader.GetOrdinal("VacationType"));
                                    VacationProcessingStatetmp =
                                        (
                                            reader.GetString(reader.GetOrdinal("VacationProcessingState")));
                                    ModificationDatetmp = reader.GetDateTime(reader.GetOrdinal("ModificationDate"));
                                    VacationOverlapNotetmp =
                                        reader.GetBoolean(reader.GetOrdinal("VacationPeriodOverlapNote"));
                                    VacationLockPeriodNotetmp =
                                        reader.GetBoolean(reader.GetOrdinal("VacationLockPeriodNote"));
                                    EmployeeIDtmp = reader.GetString(reader.GetOrdinal("EmployeeID"));
                                    NetVacationDays = reader.GetInt32(reader.GetOrdinal("NetVacationDays"));

                                    VacationRequest Vac = new VacationRequest(VacationRequestIDtmp, EmployeeIDtmp,
                                        VacationStartDatetmp,
                                        VacationEndDatetmp, SubmissionDatetmp, VacationTypetmp,
                                        VacationRequestProcessingState.submitted,
                                        ModificationDatetmp, VacationOverlapNotetmp, VacationLockPeriodNotetmp,
                                        NetVacationDays);
                                    VacList.Add(Vac);
                                }
                            }
                        }
                    }
                }
            }
            VacationRequest Vacexpect = new VacationRequest(10, "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43", new DateTime(2015, 02, 01), new DateTime(2015, 02, 28),
                                    SubmissionDatetmp, "Erholungsurlaub", VacationRequestProcessingState.submitted, ModificationDatetmp,
                                    false, false, 29);
            bool Result = false;
            if ((VacList[0].getVacationRequestID() == Vacexpect.getVacationRequestID())
                && (VacList[0].getEmployeeID() == Vacexpect.getEmployeeID())
                && (VacList[0].getVacationStartDate() == Vacexpect.getVacationStartDate())
                && (VacList[0].getVacationEndDate() == Vacexpect.getVacationEndDate())
                && (VacList[0].getSubmissionDate() == Vacexpect.getSubmissionDate())
                && (VacList[0].getVacationType() == Vacexpect.getVacationType())
                && (VacList[0].getVacationRequestProcessingState() == Vacexpect.getVacationRequestProcessingState())
                && (VacList[0].getModificationDate() == Vacexpect.getModificationDate())
                && (VacList[0].getVacationPeriodOverlapNote() == Vacexpect.getVacationPeriodOverlapNote())
                && (VacList[0].getVacationLockPeriodNote() == Vacexpect.getVacationLockPeriodNote())
                && (VacList[0].getNetVacationDays() == Vacexpect.getNetVacationDays()))
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
            Assert.AreEqual(true, Result);
        }

        /// <summary>
        /// Selects one VacationRequest
        /// </summary>
        /// <param name="VacationRequestID">VacationRequestID of that one Request</param>
        /// <returns>that one VacationRequest</returns>
        [TestMethod()]
        public void SelectVacationRequestTest_F5032_K_1()  //muss noch dokumentiert werden
        {
       
            ulong VacationRequestIDtmp = 0UL;
            DateTime VacationStartDatetmp = new DateTime(2000, 1, 1);
            DateTime VacationEndDatetmp = new DateTime(2000, 1, 1);
            DateTime SubmissionDatetmp = new DateTime(2000, 1, 1);
            string VacationProcessingStatetmp = "";
            string VacationTypetmp = "c";
            DateTime ModificationDatetmp = new DateTime(2000, 1, 1);
            Boolean VacationOverlapNotetmp = false;
            Boolean VacationLockPeriodNotetmp = false;
            string EmployeeIDtmp = "null";
            int NetVacationDays = 0;

            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT * FROM dbo.VacationRequest WHERE VacationRequestID = " + 10 + "";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Returns into temp variables and insert them into the list

                                VacationRequestIDtmp = (ulong)reader.GetInt64(reader.GetOrdinal("VacationRequestID"));
                                VacationStartDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationStartDate"));
                                VacationEndDatetmp = reader.GetDateTime(reader.GetOrdinal("VacationEndDate"));
                                SubmissionDatetmp = reader.GetDateTime(reader.GetOrdinal("SubmissionDate"));
                                VacationTypetmp = reader.GetString(reader.GetOrdinal("VacationType"));
                                VacationProcessingStatetmp = reader.GetString(reader.GetOrdinal("VacationProcessingState"));
                                ModificationDatetmp = reader.GetDateTime(reader.GetOrdinal("ModificationDate"));
                                VacationOverlapNotetmp = reader.GetBoolean(reader.GetOrdinal("VacationPeriodOverlapNote"));
                                VacationLockPeriodNotetmp = reader.GetBoolean(reader.GetOrdinal("VacationLockPeriodNote"));
                                EmployeeIDtmp = reader.GetString(reader.GetOrdinal("EmployeeID"));
                                NetVacationDays = reader.GetInt32(reader.GetOrdinal("NetVacationDays"));

                            }
                        }
                    }
                }
            }
            VacationRequest Vac = new VacationRequest(VacationRequestIDtmp, EmployeeIDtmp, VacationStartDatetmp,
                                    VacationEndDatetmp, SubmissionDatetmp, VacationTypetmp, VacationRequestProcessingState.submitted,
                                    ModificationDatetmp, VacationOverlapNotetmp, VacationLockPeriodNotetmp, NetVacationDays);
            VacationRequest Vacexpect = new VacationRequest(10,"1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43", new DateTime(2015,02,01),new DateTime(2015,02,28),
                                    SubmissionDatetmp, "Erholungsurlaub", VacationRequestProcessingState.submitted, ModificationDatetmp,
                                    false,false,29);
            bool Result = false;
            if ((Vac.getVacationRequestID() == Vacexpect.getVacationRequestID())
                && (Vac.getEmployeeID() == Vacexpect.getEmployeeID())
                && (Vac.getVacationStartDate() == Vacexpect.getVacationStartDate())
                && (Vac.getVacationEndDate() == Vacexpect.getVacationEndDate())
                && (Vac.getSubmissionDate() == Vacexpect.getSubmissionDate())
                && (Vac.getVacationType() == Vacexpect.getVacationType())
                && (Vac.getVacationRequestProcessingState() == Vacexpect.getVacationRequestProcessingState())
                && (Vac.getModificationDate() == Vacexpect.getModificationDate())
                && (Vac.getVacationPeriodOverlapNote() == Vacexpect.getVacationPeriodOverlapNote())
                && (Vac.getVacationLockPeriodNote() == Vacexpect.getVacationLockPeriodNote())
                && (Vac.getNetVacationDays() == Vacexpect.getNetVacationDays()))
            {
                Result = true;
            }
            Assert.AreEqual(true, Result);
        }

        [TestMethod()]
        public void UpdateRemainingVacationDaysTest_F5211_K_1()   //muss noch dokumentiert werden und F-Nummer fehlt noch, gar nicht vorhanden!
        {
            bool Result = false;
            TestDBReset Reset = new TestDBReset();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "UPDATE VacationEntitlement SET VacationDayRemaining = @RemainingVacationDays WHERE EmployeeID = @EmployeeID";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {
                    cmd.Parameters.AddWithValue("RemainingVacationDays", 40);
                    cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Result = true;
                    }
                    catch (SqlException e)
                    {
                        {
                            Result = false;
                        }
                    }
                }
            }
            if (Result == true)
            {

                if (Reset.ResetTestDB())
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }

            Assert.AreEqual(true, Result);
        }

        /// <summary>
        /// selects the full name of one employee
        /// </summary>
        /// <param name="EmployeeID">EmployeeID of the wanted Employee</param>
        /// <returns>string with full name of the Employee</returns>
        [TestMethod()]
         public void SelectEmployeeFullName_F5212_K_1()  // muss noch dokumentiert werden, F-Nummer fehlt
         {
            
             string FullName = "null";
             string FirstNametmp = "null";
             string LastNametmp = "null";
             bool Result = false;
             using (SqlConnection connection = new SqlConnection(sqlConnectionString))
             {
                 string sqlStatement =
                         "SELECT FirstName, LastName FROM dbo.AspNetUsers WHERE Id = @EmployeeID";
                 using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                 {
                     cmd.Parameters.AddWithValue("EmployeeID", "1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43");
                     connection.Open();
                     using (SqlDataReader reader = cmd.ExecuteReader())
                     {
                         // Check if the reader has any rows at all before starting to read.
                         if (reader.HasRows)
                         {
                             // Read advances to the next row.
                             while (reader.Read())
                             {
                                 // Save DB-Return into variable
                                 FirstNametmp = reader.GetString(reader.GetOrdinal("FirstName"));
                                 LastNametmp = reader.GetString(reader.GetOrdinal("LastName"));
                             }
                         }
                     }
                 }
             }
            if (FirstNametmp == "Vorname1" && LastNametmp == "Nachname1")
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
            Assert.AreEqual(true, Result);
         }


        [TestMethod()]
        public void SelectShiftGroupOfDivision()
        {
            bool Result = false;
            string EmployeeID01tmp = "c";
            string EmployeeID02tmp = "c";
            int ShiftGroupIDtmp = 0;


            var ShiftList = new List<ShiftGroup>();
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                string sqlStatement =
                    "SELECT * FROM dbo.ShiftGroup";
                using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                {

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            // Read advances to the next row.
                            while (reader.Read())
                            {
                                // Save DB-Return into variables
                                // wihout Role and StaffID
                                ShiftGroupIDtmp = reader.GetInt32(reader.GetOrdinal("ShiftGroupID"));
                                EmployeeID01tmp = reader.GetString(reader.GetOrdinal("EmployeeID01"));
                                EmployeeID02tmp = reader.GetString(reader.GetOrdinal("EmployeeID02"));


                                ShiftGroup DivShiftgroup = new ShiftGroup(ShiftGroupIDtmp, EmployeeID01tmp, EmployeeID02tmp);

                                ShiftList.Add(DivShiftgroup);
                            }
                        }

                    }

                }
            }
            Assert.AreEqual(true, Result);
        }

    }

    
}

