using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Text.RegularExpressions;

namespace suitsAPI.Authentication
{
    public static class ApiKeyValidation
    {

        public static Dictionary<string, bool> RequestedRights = new()
        {
            { "GET/WeatherForecast", true },
            { "POST/api/Mammoth", true },
            { "GET/api/Mammoth", true }, // Set this as admin until the test begins
        };
        public static Dictionary<string, string> MethodKeys = new()
        {
            { "GET/WeatherForecast", "5005-" },
            { "POST/api/Mammoth", "5005-" },
            { "GET/api/Mammoth", "5005-" },
        };
        public static KeyValuePair<bool, string> IsValidKey(string apiKey)
        {
            /*
             * This is a fairly simple project, so we are not going to handle creation/registration/storage of API Keys until later
             * API Keys just need to follow a simple format: 7025-REQUEST_CODE-RIGHTS_CODE
             * REQUEST_CODE specifies the project we are looking for, 5005 for MAMMOTH, and 0550 for WIGLE
             * RIGHTS_CODE specifies whether the user has the actual rights to run a method (i.e. start a demonstration) 0000 for admin, incrementing counts for others
            */
            Regex fullMatch = new("[0-9]{4}-[0-9]{4}-[0-9]+");
            var fullMatchResults = fullMatch.Matches(apiKey);
            Regex initialCode = new("[0-9]{4}-");
            var initialCodeResults = initialCode.Matches(apiKey);
            if (fullMatchResults.Count != 1 || initialCodeResults.Count < 2)
            {
                return new KeyValuePair<bool, string>(false, "Invalid API Key format");
            }
            if (initialCodeResults[0].Value != "7025-")
            {
                return new KeyValuePair<bool, string>(false, "Invalid initial code");
            }
            if (initialCodeResults[1].Value != "5005-" && initialCodeResults[1].Value != "0550-")
            {
                return new KeyValuePair<bool, string> (false, "Invalid request code");
            }

            return new KeyValuePair<bool, string> (true, "Success");
        }
        public static KeyValuePair<bool, string> AreRightsValid(string apiKey, string methodRequested)
        {
            /*
             * This method simply checks whether the supplied API key has enough rights to access the method requested, to avoid sending admin commands from non-admin users.
            */
            Regex initialCode = new("[0-9]{4}-");
            var initialCodeResults = initialCode.Matches(apiKey);
            if (initialCodeResults[1].Value != MethodKeys[methodRequested])
            {
                return new KeyValuePair<bool, string>(false, "You do not have rights to this project");
            }

            if (RequestedRights[methodRequested])
            {
                Regex matches = new("[0-9]{4}-[0-9]{4}-");
                var matchResults = matches.Matches(apiKey);
                string user = apiKey.Replace(matchResults[0].Value, "");
                if (user != "0000")
                {
                    return new KeyValuePair<bool, string>(false, "This is an admin method, invalid request");
                }
            }

            return new KeyValuePair<bool, string>(true, "Success");
        }
    }
}
