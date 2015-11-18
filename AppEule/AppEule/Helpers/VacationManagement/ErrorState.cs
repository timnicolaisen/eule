

namespace VacationManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ErrorState
    {
        private static ErrorState _errorStateInstance;
        private int _error;

        public const int OK = 0;
        public const int DATABASE_ERROR = 1;
        public const int END_DATE_BEFORE_START_DATE = 2;
        public const int VACATION_REQUEST_NOT_IN_CURRENT_YEAR = 3;
        public const int ZERO_NET_VACATION_DAYS_IN_VACATION_REQUEST = 4;
        public const int INSUFFICIENT_REMAINING_VACATION_DAYS = 5;
        public const int OVERLAP_WITH_OWN_VACATION_REQUEST = 6;




        public static ErrorState ErrorStateInstance
        {
           get 
            {
                if (_errorStateInstance == null)
                {
                    _errorStateInstance = new ErrorState();
                }
                return _errorStateInstance;
            }
        }

        private ErrorState() 
        {
            _error = 0;
        }

        public void setError(int value) 
        {
            _error = value;
        }

        public int getLastError()
        {
            int value = _error;
            _error = 0;
            return value;
        }

        public String getErrorMessage(int errorNumber)
        {
            String result = "";
            switch (errorNumber)
            {
                case DATABASE_ERROR: result = "Datenbankfehler. Wenden Sie sich bitte an Ihren Systemadministrator."; break;
                case END_DATE_BEFORE_START_DATE: result = "Das Urlaubsenddatum liegt vor dem Urlaubsstartdatum."; break;
                case VACATION_REQUEST_NOT_IN_CURRENT_YEAR: result = "Der gewünschte Urlaubszeitraum liegt nicht vollständig im aktuellen Jahr."; break;
                case ZERO_NET_VACATION_DAYS_IN_VACATION_REQUEST: result = "Der gewünschte Urlaubszeitraum enthält 0 Werktage."; break;
                case INSUFFICIENT_REMAINING_VACATION_DAYS: result = "Sie haben nicht genügend Urlaubstage übrig."; break;
                case OVERLAP_WITH_OWN_VACATION_REQUEST: result = "Im gewünschten Zeitraum liegt bereits ein Urlaubsantrag im System vor."; break;
                default: result = "Unbekannter Fehler"; break;

            }
                    return result;
        }


    }
}