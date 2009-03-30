using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Concept.Console.GoogleCalender
{
    public static class GoogleAuth
    {

        private const string AuthURL = "https://www.google.com/accounts/ClientLogin";

        public static string AuthenticateCalender()
        {
            string authenticateURL = AuthURL + "?Email=**EMAIL ADDRESS**&Passwd=**PASSWORD**&source=steve-ozCC-1&service=cl";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(authenticateURL);
            request.ContentLength = ASCIIEncoding.ASCII.GetBytes(authenticateURL).Length;

            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            
            Stream responseStream = response.GetResponseStream();

            int buffer = 1024;
            byte[] data = new byte[buffer];
            int count = 0;

            do
            {
                count = responseStream.Read(data, 0, buffer);
            }
            while (count != 0);

            response.Close();

            string responseText = ParseResponseEncoding(response.ContentEncoding).GetString(data);
            string[] responseParse = responseText.Split('=');
            string authToken = responseParse[responseParse.Length - 1];

            System.Console.WriteLine("Auth Response: {0}", response.StatusCode);
            System.Console.WriteLine("Auth Token: {0}", authToken);

            return SanitiseToken(authToken);
        }

        private static string SanitiseToken(string token)
        {
            string cleanString = string.Empty;

            foreach (char c in token)
            {
                if (c > 31)
                {
                    cleanString += c;
                }
            }

            return cleanString.Trim();
        }

        private static Encoding ParseResponseEncoding(string encodingType)
        {

            switch (encodingType)
            {
                case "ASCII":
                    return Encoding.ASCII;
                case "UTF-8":
                    return Encoding.UTF8;
                case "Unicode":
                    return Encoding.Unicode;
                
            }

            //default to ASCII if unknown
            return Encoding.Default;
        }
    }
}
