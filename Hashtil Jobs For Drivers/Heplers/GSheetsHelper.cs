using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System.IO;

namespace Hashtil_Jobs_For_Drivers.Heplers
{
    static class GSheetsHelper
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "JobsForDrivers";
        static readonly string SpreadsheetId = "1OW516xac_aL82ecobJSq5Xmmo09UQ6jzptTIHehJuYY";
        static readonly string Sheet = "טבלת הוצאות";
        static SheetsService SheetsService;

        static  GSheetsHelper()
        {
            GoogleCredential googleCredential;
            using(var stream =  new FileStream("hashtil-job-table-wpf.json", FileMode.Open, FileAccess.Read))
            {
                googleCredential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);

            }

            SheetsService = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = googleCredential,
                ApplicationName = ApplicationName,
            });
        }

        public static void ReadEntries()
        {
            var range = $"{Sheet}!A:N";
            var request = SheetsService.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;
            if(values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    continue;
                }
            }
            else
            {
                
            }
        }

    }
}
