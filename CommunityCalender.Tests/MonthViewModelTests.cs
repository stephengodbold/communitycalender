using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunityCalender.ViewModels;
using CommunityCalender.Models;

namespace CommunityCalender.Tests
{
    /// <summary>
    /// Summary description for MonthViewModelTests
    /// </summary>
    [TestClass]
    public class MonthViewModelTests
    {
        public MonthViewModelTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private MonthViewModel monthViewModel;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize]
        public void TestSetup()
        {
            monthViewModel = new MonthViewModel();
        }

        [TestMethod]
        public void Set_Month_Returns_Correct_Month()
        {
            monthViewModel.ChangeMonth(1, 2009);

            Assert.IsNotNull(monthViewModel.Month);
            Assert.IsTrue(monthViewModel.Month.Name.ToUpper() == "JANUARY");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Set_Month_Throws_Exception_For_Month_GT_12()
        {
            monthViewModel.ChangeMonth(25,2008);
        }

        [TestMethod]
        public void Month_Has_Correct_Number_Of_Days()
        {
            monthViewModel.ChangeMonth(1, 2009);

            Assert.IsNotNull(monthViewModel.Month);
            Assert.IsNotNull(monthViewModel.Month.Days);
            Assert.IsTrue(monthViewModel.Month.Days.Count == DateTime.DaysInMonth(2009,1));        }

    }
}
