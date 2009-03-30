using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CommunityCalender.Controls;
using CommunityCalender.ViewModels;

namespace CommunityCalender
{
	public partial class EventView : UserControl, IPageChanged
	{
        public event PageChangedEventHandler PageChanged;

		public EventView()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        protected void OnPageChanged(ApplicationPage targetPage, ViewModelBase dataContext)
        {
            if (PageChanged != null)
            {
                PageChanged(targetPage, dataContext);
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            OnPageChanged(ApplicationPage.MonthView, null);
        }


    }
}