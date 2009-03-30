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

namespace CommunityCalender.ViewModels
{
    public class EventViewModel : ViewModelBase
    {
        private Event _event;
        public Event Event
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged("Event");
            }
        }
    }
}
