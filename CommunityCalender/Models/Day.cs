using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace CommunityCalender.Models
{
    public class Day
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int DayNumber { get; set; }

        public Collection<Event> Events { get; set; }

    }
}
