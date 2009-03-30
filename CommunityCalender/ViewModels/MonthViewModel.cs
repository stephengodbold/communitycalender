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
using CommunityCalender.Models;
using System.Collections.ObjectModel;



namespace CommunityCalender.ViewModels
{
    public class MonthViewModel : ViewModelBase
    {
        private DateTime internalDateStore;

        public Month Month { get { return CalculateMonth(); } }

        private Day currentDay;
        public Day CurrentDay
        {
            get
            {
                return currentDay;
            }
            set
            {
                currentDay = value;
                OnPropertyChanged("CurrentDay");
            }
        }

        public string UserGreeting { get; set; }

        public MonthViewModel()
        {
            internalDateStore = DateTime.Today;
            UserGreeting = "Hi there, you should log in!";
        }

        public void NextMonth()
        {
            internalDateStore = internalDateStore.AddMonths(1);
            CurrentDay = null;
            OnPropertyChanged("Month");
        }

        public void PreviousMonth()
        {
            internalDateStore = internalDateStore.AddMonths(-1);
            CurrentDay = null;
            OnPropertyChanged("Month");
        }

        private Month CalculateMonth()
        {
            var month = new Month();

            month.MonthIndex = internalDateStore.Month;
            month.Year = internalDateStore.Year;
            month.Days = GetDaysInMonth();
            month.Name = string.Concat(GetMonthName(), " ", internalDateStore.Year.ToString());

            return month;
        }

        private string GetMonthName()
        {
            switch (internalDateStore.Month)
            {
                case 1:
                    return "January";
                case 2:
                    return "Febuary";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "What the?";
            }
        }

        private Collection<Day> GetDaysInMonth()
        {
            var days = new Collection<Day>();
            int daysInMonth = DateTime.DaysInMonth(internalDateStore.Year, internalDateStore.Month);

            var events = new Collection<Event>();
            events.Add(new Event { Title = "Event!" });
            events.Add(new Event { Title = "Another Event!", Summary = "This event occurs at some time and will be cool" });

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(internalDateStore.Year, internalDateStore.Month, day);
                var aDay = new Day { Date = date, Name = date.DayOfWeek.ToString(), DayNumber = day };

                if ((day % 4) == 0)
                {
                    aDay.Events = events;
                }

                days.Add(aDay);
            }

            return days;
        }

    }
}
