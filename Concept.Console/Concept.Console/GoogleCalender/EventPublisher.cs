using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Net;
using System.Diagnostics;
using Concept.Console.GoogleCalender.Contracts;

namespace Concept.Console.GoogleCalender
{
    public static class EventPublisher
    {

        /// <summary>
        /// See http://code.google.com/apis/calendar/docs/2.0/developers_guide_protocol.html#CreatingSingle
        /// for info on the event publishing details.
        /// </summary>

        private const string EventPostURL = "http://www.google.com/calendar/feeds/default/private/full";
        private const string EventMessage = "<entry xmlns='http://www.w3.org/2005/Atom'" +
                                                "xmlns:gd='http://schemas.google.com/g/2005'>" +
                                                "<category scheme='http://schemas.google.com/g/2005#kind'" +
                                                "term='http://schemas.google.com/g/2005#event'></category>" +
                                                "<title type='text'>{0}</title>" +
                                                "<content type='text'>{1}</content>" +
                                                "<gd:transparency" +
                                                    "value='http://schemas.google.com/g/2005#event.opaque'>" +
                                                "</gd:transparency>" +
                                                "<gd:eventStatus" +
                                                    "value='http://schemas.google.com/g/2005#event.confirmed'>" +
                                                "</gd:eventStatus>" +
                                                          "<gd:where valueString='{2}'></gd:where> " +
                                                "<gd:when startTime='{3}'" +
                                                    "endTime='{4}'></gd:when>" +
                                            "</entry>";

        

        public static void PublishEvent(Event calenderEvent)
        {
            string token = GoogleAuth.AuthenticateCalender();

            string postEventMessage = string.Format(EventMessage, calenderEvent.Title, calenderEvent.Content,
                                                                    "360", calenderEvent.Start.ToString("yyyyMMdd"),
                                                                    calenderEvent.End.ToString("yyyyMMdd"));

            byte[] rawRequest = Encoding.ASCII.GetBytes(postEventMessage);

            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(EventPostURL);
            request.Headers.Add("Authorization", "Authorization: GoogleLogin auth=" + token);
            request.PreAuthenticate = true;
 
            //TODO: Find the auth header and put the token in...

            request.Method = "POST";
            request.ContentLength = rawRequest.Length;
            request.ContentType = "application/atom+xml";
            
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(rawRequest, 0, rawRequest.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Debug.Assert(response != null, "Response is null");

            System.Console.WriteLine("Response Code: {0}", response.StatusCode);
        }
    }
}
