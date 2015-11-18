using System.Data.SqlClient;


namespace AppEule.Tests.Helpers.DatabaseManagement
{

    class TestDBReset
    {
        public bool ResetTestDB() {
            const string sqlConnectionString =
                "data source=141.56.139.27\\EULE;initial catalog=EULE_TEST;user id=eule_connect;password=eulehtwddseII;MultipleActiveResultSets=True;App=EntityFramework";
            bool Result = false;
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sqlStatement =                                                                                                       //insert ID like that because of DataType conflicts
                        "drop table AspNetUserRoles;" +
                        "drop table AspNetUserLogins;" +
                        "drop table AspNetUserClaims;" +
                        "drop table AspNetRoles;" +
                        "drop table VacationEntitlement;" +
                        "drop table SickNote;" +
                        "drop table VacationLockPeriod;" +
                        "drop table VacationRequest;" +
                        "drop table AspNetUsers;" +
                        "drop table Division;" +
                        "drop table ShiftGroup;" +
                        "drop table Calendar;" +
                        "CREATE TABLE [dbo].[AspNetRoles] ([Id]   NVARCHAR (128) NOT NULL,[Name] NVARCHAR (256) NOT NULL," +
                        "CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)); " +
                        "CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]ON [dbo].[AspNetRoles]([Name] ASC);" +
                        "CREATE TABLE [dbo].[Division] ([DivisionID]        INT            IDENTITY (1, 1) NOT NULL,[DivisionTitle]     NVARCHAR (30)      NOT NULL,[DivisionManagerID] NVARCHAR (128) NULL,PRIMARY KEY CLUSTERED ([DivisionID] ASC));" +
                        "CREATE TABLE [dbo].[ShiftGroup] ([ShiftGroupID] INT            IDENTITY (1, 1) NOT NULL,[EmployeeID01] NVARCHAR (128) NOT NULL,[EmployeeID02] NVARCHAR (128) NOT NULL,PRIMARY KEY CLUSTERED ([ShiftGroupID] ASC));" +
                        "CREATE TABLE [dbo].[AspNetUsers] ([Id] NVARCHAR (128) NOT NULL,[Email] NVARCHAR (256) NULL,[EmailConfirmed] BIT   NOT NULL,[PasswordHash]         NVARCHAR (MAX) NULL,[SecurityStamp]        NVARCHAR (MAX) NULL,[PhoneNumber]          NVARCHAR (MAX) NULL," +
                            "[PhoneNumberConfirmed] BIT            NOT NULL,[TwoFactorEnabled]     BIT            NOT NULL," +
                            "[LockoutEndDateUtc]    DATETIME       NULL,[LockoutEnabled]       BIT            NOT NULL," +
                            "[AccessFailedCount]    INT            NOT NULL,[UserName]             NVARCHAR (256) NOT NULL," +
                            "[ShiftGroupID]		   INT			  NULL,[DivisionID]           INT            NOT NULL,[FirstName]			   NVARCHAR(30)   NOT NULL,[LastName]			   NVARCHAR(30)   NOT NULL,CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC),FOREIGN KEY(ShiftGroupID) REFERENCES ShiftGroup(ShiftGroupID),						" +
                            "FOREIGN KEY(DivisionID) REFERENCES Division(DivisionID));" +
                        "CREATE TABLE [dbo].[AspNetUserClaims] ([Id]         INT            IDENTITY (1, 1) NOT NULL,[UserId]     NVARCHAR (128) NOT NULL,[ClaimType]  NVARCHAR (MAX) NULL,[ClaimValue] NVARCHAR (MAX) NULL,CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE);" +
                        "CREATE NONCLUSTERED INDEX [IX_UserId]ON [dbo].[AspNetUserClaims]([UserId] ASC);" +
                        "CREATE TABLE [dbo].[AspNetUserLogins] ([LoginProvider] NVARCHAR (128) NOT NULL,[ProviderKey]   NVARCHAR (128) NOT NULL,[UserId]        NVARCHAR (128) NOT NULL,CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE);" +
                        "CREATE NONCLUSTERED INDEX [IX_UserId]ON [dbo].[AspNetUserLogins]([UserId] ASC);" +
                        "CREATE TABLE [dbo].[AspNetUserRoles] ([UserId] NVARCHAR (128) NOT NULL,[RoleId] NVARCHAR (128) NOT NULL,CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE);" +
                        "CREATE NONCLUSTERED INDEX [IX_UserId]ON [dbo].[AspNetUserRoles]([UserId] ASC);" +
                        "CREATE NONCLUSTERED INDEX [IX_RoleId]ON [dbo].[AspNetUserRoles]([RoleId] ASC);" +
                        "CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]ON [dbo].[AspNetUsers]([UserName] ASC);" +
                        "CREATE TABLE [dbo].[SickNote] ([SickNoteID]         INT            IDENTITY (1, 1) NOT NULL,[SickLeaveStartDate] DATE           NOT NULL,[SickLeaveEndDate]   DATE           NOT NULL,[EmployeeID]         NVARCHAR (128) NOT NULL,PRIMARY KEY CLUSTERED ([SickNoteID] ASC),FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[AspNetUsers] ([Id]));" +
                        "CREATE TABLE [dbo].[VacationEntitlement] ([VacationEntitlementID]    INT            IDENTITY (1, 1) NOT NULL,[VacationDaysTotal]        INT            NOT NULL,[VacationDaysPreviousYear] INT            NOT NULL,[VacationDayRemaining]     INT            NOT NULL,[EmployeeID]               NVARCHAR (128) NOT NULL,PRIMARY KEY CLUSTERED ([VacationEntitlementID] ASC),FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[AspNetUsers] ([Id]));" +
                        "CREATE TABLE [dbo].[VacationRequest] ([VacationRequestID]         BIGINT         NOT NULL,[VacationStartDate]         DATE           NOT NULL,[VacationEndDate]           DATE           NOT NULL,[SubmissionDate]            DATETIME       NOT NULL,[VacationType]              NVARCHAR (30)  NOT NULL,[VacationProcessingState]   NVARCHAR (35)  NOT NULL,[ModificationDate]          DATETIME       NULL,[VacationPeriodOverlapNote] BIT			   NULL," +
                            "[VacationLockPeriodNote]    BIT			   NULL,[NetVacationDays]			INT			   NOT NULL,[EmployeeID]                NVARCHAR (128) NOT NULL,PRIMARY KEY CLUSTERED ([VacationRequestID] ASC),FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[AspNetUsers] ([Id]));" +
                        "CREATE TABLE [dbo].[VacationLockPeriod] ([LockPeriodID]        INT  IDENTITY (1, 1) NOT NULL,[LockPeriodStartDate] DATE NOT NULL,[LockPeriodEndDate]   DATE NOT NULL,[DivisionID]          INT  NOT NULL,PRIMARY KEY CLUSTERED ([LockPeriodID] ASC),FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[Division] ([DivisionID]));" +
                        "CREATE TABLE [dbo].[Calendar] ( Date 		DATE 		NOT NULL, Weekday 		NVARCHAR(30) 	NOT NULL, DayType 		NVARCHAR(30) 	NOT NULL);" +
                        "INSERT INTO Division VALUES ('Laborbereich','NULL');" +
                        "INSERT INTO AspNetUsers VALUES ('1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43','Login1@tim-n.de','False','ADkWVNwBwSGUDBGcEn0UbbX1c6Cek0EZi2nTTIaIUDHi75L7OHbS7xg4UVvhg3q3/w==', '37a0d46d-1317-484d-b64b-bdd5906834d3',NULL,'False','False',NULL,'True',0,'Login1',NULL,(SELECT DivisionID FROM Division WHERE DivisionTitle = 'Laborbereich'),'Vorname1','Nachname1');" +
                        "INSERT INTO AspNetUsers VALUES ('b03a5057-7505-4d5e-997f-efc0b40ed6e0','Login2@tim-n.de','False','AMwipYYgMsGCx+p/KtU20DwWkhb2PMRRGlc2oqzNsXpsugQS2FiIR0QHsUllTsJpqA==','b576466d-d7d7-4bb3-bde3-a5cd0bd0c8a9',NULL,'False','False',NULL,'True',0,'Login2',NULL,(SELECT DivisionID FROM Division WHERE DivisionTitle = 'Laborbereich'),'Vorname2','Nachname2');" +
                        "INSERT INTO AspNetUsers VALUES ('f7bb0ab3-728b-4053-9b83-1b159a9d83a8','Login3@tim-n.de','False','AIXF/Lh8T0g5XJA8HE8BIJdZTTbhiSjj5ni0mKc64FeaV3VNfn4VZOk5piLmVih4Ug==','f979a9f6-c725-4805-946c-ad2e20ca4cc6',NULL,'False','False',NULL,'True',0,'Login3',NULL,(SELECT DivisionID FROM Division WHERE DivisionTitle = 'Laborbereich'),'Vorname3','Nachname3');UPDATE Division SET DivisionManagerID = 'f7bb0ab3-728b-4053-9b83-1b159a9d83a8' WHERE DivisionID = 1;" +
                        "INSERT INTO VacationEntitlement VALUES (35,5,35,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationEntitlement VALUES (35,5,35,'b03a5057-7505-4d5e-997f-efc0b40ed6e0');" +
                        "INSERT INTO VacationEntitlement VALUES (35,5,35,'f7bb0ab3-728b-4053-9b83-1b159a9d83a8');" +
                        "INSERT INTO ShiftGroup VALUES ('1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43','b03a5057-7505-4d5e-997f-efc0b40ed6e0');" +
                        "UPDATE AspNetUsers SET ShiftGroupID = (SELECT ShiftGroupID FROM ShiftGroup WHERE EmployeeID01 = '1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43' OR EmployeeID02 = '1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43') WHERE Id = '1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43';" +
                        "UPDATE AspNetUsers SET ShiftGroupID = (SELECT ShiftGroupID FROM ShiftGroup WHERE EmployeeID01 = 'b03a5057-7505-4d5e-997f-efc0b40ed6e0' OR EmployeeID02 = 'b03a5057-7505-4d5e-997f-efc0b40ed6e0') WHERE Id = 'b03a5057-7505-4d5e-997f-efc0b40ed6e0';" +
                        "INSERT INTO VacationRequest VALUES (1,'20150201','20150228',GETDATE(),'Erholungsurlaub','genommen',GETDATE(),'False','False',27,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (2,'20150301','20150331',GETDATE(),'Erholungsurlaub','befürwortet',GETDATE(),'False','False',27,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (3,'20150401','20150430',GETDATE(),'Erholungsurlaub','genehmigt',GETDATE(),'False','False',27,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (4,'20150501','20150531',GETDATE(),'Erholungsurlaub','abgelehnt durch Schichtpartner',GETDATE(),'False','False',27,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (5,'20150601','20150630',GETDATE(),'Erholungsurlaub','storniert',GETDATE(),'False','False',27,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (6,'20150701','20150731',GETDATE(),'Erholungsurlaub','genommen',GETDATE(),'False','False',27,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (7,'20150801','20150831',GETDATE(),'Erholungsurlaub','abgelehnt durch Bereichsleiter',GETDATE(),'False','False',26,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (8,'20150901','20150901',GETDATE(),'Erholungsurlaub','offen',GETDATE(),'False','False',26,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO VacationRequest VALUES (9,'20150101','20150131',GETDATE(),'Erholungsurlaub','offen',GETDATE(),'False','False',28,'f7bb0ab3-728b-4053-9b83-1b159a9d83a8');" +
                        "INSERT INTO VacationRequest VALUES (10,'20150201','20150228',GETDATE(),'Erholungsurlaub','offen',GETDATE(),'False','False',29,'1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43');" +
                        "INSERT INTO Calendar VALUES ('20150101','Donnerstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20150403','Freitag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20150406','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20150501','Freitag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20150514','Donnerstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20150525','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20151003','Samstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20151031','Samstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20151118','Mittwoch','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20151225','Freitag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20151226','Samstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20160101','Freitag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20160325','Freitag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20160328','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20160501','Sonntag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20160505','Donnerstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20160516','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20161003','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20161031','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20161116','Mittwoch','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20161225','Sonntag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20161226','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20170101','Sonntag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20170414','Freitag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20170417','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20170501','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20170525','Donnerstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20170605','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20171003','Dienstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20171031','Dienstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20171122','Mittwoch','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20171225','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20171226','Dienstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20180101','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20180330','Freitag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20180402','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20180501','Dienstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20180510','Donnerstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20180521','Montag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20181003','Mittwoch','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20181031','Mittwoch','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20181121','Mittwoch','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20181225','Dienstag','Feiertag');" +
                        "INSERT INTO Calendar VALUES ('20181226','Mittwoch','Feiertag');" +
                        "insert into VacationLockPeriod values ('20150601', '20150610',1);" +
                        "insert into AspNetRoles Values (1,'Administrator');" +
                        "insert into AspNetRoles Values (2,'Bereichsleiter');" +
                        "insert into AspNetRoles Values (3,'Mitarbeiter');" +
                        "insert into AspNetUserRoles values ('f7bb0ab3-728b-4053-9b83-1b159a9d83a8',2);" +
                        "insert into AspNetUserRoles values('b03a5057-7505-4d5e-997f-efc0b40ed6e0',3);" +
                        "insert into AspNetUserRoles values('1e04bd54-4fb8-4bb8-806c-8ad7b8c90c43',3);";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, connection))
                    {
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
                return Result;
        }
    }
}
