﻿using Google.Apis.Sheets.v4;

namespace Hashtil_Jobs_For_Drivers.Heplers
{
    public class GSheetsHelper
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "JobsForDrivers";
        static readonly string SpreadsheetId = "1OW516xac_aL82ecobJSq5Xmmo09UQ6jzptTIHehJuYY";
    }
}