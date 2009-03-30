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
using System.Windows.Browser;
using CommunityCalender.Common;

namespace CommunityCalender
{
    public partial class ViewManager : UserControl
    {
        public ViewManager()
        {
            // Required to initialize variables
            InitializeComponent();

            this.LayoutRoot.Children.Add(MonthView);
            this.LayoutRoot.Children.Add(EventView);

            //Authenticate();
            SetActiveView(ApplicationPage.MonthView, null);
        }

        private MonthView monthView;
        private MonthView MonthView
        {
            get
            {
                if (monthView == null)
                {
                    monthView = new MonthView();
                    monthView.Visibility = Visibility.Collapsed;
                    monthView.PageChanged += new CommunityCalender.Controls.PageChangedEventHandler(SetActiveView);
                }

                return monthView;
            }
        }

        private EventView eventView;
        private EventView EventView
        {
            get
            {
                if (eventView == null)
                {
                    eventView = new EventView();
                    eventView.Visibility = Visibility.Collapsed;
                    eventView.PageChanged += new CommunityCalender.Controls.PageChangedEventHandler(SetActiveView);
                }

                return eventView;
            }
        }

        private UserControl ActiveView { get; set; }

        /// <summary>
        /// Sets the active view to the requested view with the provided data context
        /// </summary>
        /// <param name="targetPage"></param>
        /// <param name="dataContext"></param>
        private void SetActiveView(CommunityCalender.ViewModels.ApplicationPage targetPage, CommunityCalender.ViewModels.ViewModelBase dataContext)
        {
            
            if (ActiveView != null)
            {
                ActiveView.Visibility = Visibility.Collapsed;
            }

            if (targetPage == ApplicationPage.EventView)
            {
                ActiveView = EventView;
            }
            else
            {
                ActiveView = MonthView;
            }

            if (dataContext != null)
            {
                ActiveView.DataContext = dataContext;
            }
            
            ActiveView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Sets the active view to the requested view with the default data context
        /// </summary>
        /// <param name="targetPage"></param>
        private void SetActiveView(CommunityCalender.ViewModels.ApplicationPage targetPage)
        {
            if (ActiveView != null)
            {
                ActiveView.Visibility = Visibility.Collapsed;
            }

            if (targetPage == ApplicationPage.EventView)
            {
                ActiveView = EventView;
            }
            else
            {
                ActiveView = MonthView;
            }

             ActiveView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Checks the current authentication status of the user
        /// and sets up the callback for redirection once authentication 
        /// is completed
        /// </summary>
        private void Authenticate()
        {
            AuthenticationService.AuthenticationServiceClient clientProxy = new CommunityCalender.AuthenticationService.AuthenticationServiceClient();

            try
            {
                clientProxy.IsLoggedInCompleted += new EventHandler<CommunityCalender.AuthenticationService.IsLoggedInCompletedEventArgs>(clientProxy_IsLoggedInCompleted);
                clientProxy.IsLoggedInAsync();
                clientProxy.CloseAsync();
            }
            catch
            {
                clientProxy.Abort();
            }
        }

        /// <summary>
        /// Callback for the authentication call. Displays a control based on the 
        /// login status of the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void clientProxy_IsLoggedInCompleted(object sender, CommunityCalender.AuthenticationService.IsLoggedInCompletedEventArgs e)
        {
            if (e.Result == true)
            {
                MonthViewModel model = new MonthViewModel();
                model.UserGreeting = "Hi there ";
                SetActiveView(ApplicationPage.MonthView, model);
            }
            else
                SetActiveView(ApplicationPage.MonthView, null);
                        
        }
    }
}