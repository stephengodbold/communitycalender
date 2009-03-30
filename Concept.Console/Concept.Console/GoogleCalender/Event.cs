using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concept.Console.GoogleCalender.Contracts
{

//<entry xmlns='http://www.w3.org/2005/Atom'
//    xmlns:gd='http://schemas.google.com/g/2005'>
//  <category scheme='http://schemas.google.com/g/2005#kind'
//    term='http://schemas.google.com/g/2005#event'></category>
//  <title type='text'>Tennis with Beth</title>
//  <content type='text'>Meet for a quick lesson.</content>
//  <gd:transparency
//    value='http://schemas.google.com/g/2005#event.opaque'>
//  </gd:transparency>
//  <gd:eventStatus
//    value='http://schemas.google.com/g/2005#event.confirmed'>
//  </gd:eventStatus>
//  <gd:where valueString='Rolling Lawn Courts'></gd:where>
//  <gd:when startTime='2006-04-17T15:00:00.000Z'
//    endTime='2006-04-17T17:00:00.000Z'></gd:when>
//</entry>


    public class Event
    {
        public string Category { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
