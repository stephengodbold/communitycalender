using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CommunityCalender.ViewModels;
using CommunityCalender.Models;
using System.Collections.ObjectModel;
using CommunityCalender.Controls;
using System.Windows.Browser;

namespace CommunityCalender
{
	public partial class MonthView : UserControl, IPageChanged
	{
        public event PageChangedEventHandler PageChanged;

        private ViewModels.MonthViewModel viewModel;

		public MonthView()
		{
			// Required to initialize variables
			InitializeComponent();

            //start on the current month
            viewModel = new CommunityCalender.ViewModels.MonthViewModel();
            this.DataContext = viewModel;
		}

        /// <summary>
        /// Raises the page changed event if there is a 
        /// subscriber
        /// </summary>
        /// <param name="targetPage"></param>
        protected void OnPageChanged(ApplicationPage targetPage, ViewModelBase dataContext)
        {
            if (PageChanged != null)
            {
                PageChanged(targetPage, dataContext);
            }
        }

        private void DayList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = this.DataContext as MonthViewModel;

            if (context != null)
            {
                if ((e.AddedItems != null) && (e.AddedItems.Count > 0))
                {
                    context.CurrentDay = e.AddedItems[0] as Day;
                }
            }
        }

        private void EventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = this.DataContext as MonthViewModel;

            if (context != null)
            {
                if ((e.AddedItems != null) && (e.AddedItems.Count > 0))
                {
                    OnPageChanged(ApplicationPage.EventView, new EventViewModel { Event = e.AddedItems[0] as Event });
                }
            }
        }

        private void PreviousMonthButton_Click(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MonthViewModel;

            if (context != null)
            {
                context.PreviousMonth();
            }
        }

        private void NextMonthButton_Click(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MonthViewModel;

            if (context != null)
            {
                context.NextMonth();
            }
        }

    }
}