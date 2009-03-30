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
    public class Month 
    {

        /// <summary>
        /// Name of the month
        /// </summary>
        public string Name { get; set; }

        public int MonthIndex { get; set; }

        public int Year { get; set; }

        /// <summary>
        /// Collection of days that belong to this month
        /// </summary>
        public Collection<Day> Days { get; set; }

    }
}
