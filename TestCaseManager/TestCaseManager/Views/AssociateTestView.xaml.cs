﻿// <copyright file="AssociateTestView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System.Windows;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.Helpers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the associate test case to test page
    /// </summary>
    public partial class AssociateTestView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociateTestView"/> class.
        /// </summary>
        public AssociateTestView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the associate test view model.
        /// </summary>
        /// <value>
        /// The associate test view model.
        /// </value>
        public AssociateTestViewModel AssociateTestViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
            string testCaseIdStr = fm.Get("id");
            if (!string.IsNullOrEmpty(testCaseIdStr))
            {
                int testCaseId = int.Parse(testCaseIdStr);
                this.AssociateTestViewModel = new AssociateTestViewModel(testCaseId);
            }
           
            string suiteId = fm.Get("suiteId");
            if (!string.IsNullOrEmpty(suiteId))
            {
                this.AssociateTestViewModel.TestSuiteId = int.Parse(suiteId);
            }
            string createNew = fm.Get("createNew");
            if (!string.IsNullOrEmpty(createNew))
            {
                this.AssociateTestViewModel.CreateNew = bool.Parse(createNew);
            }
            string duplicate = fm.Get("duplicate");
            if (!string.IsNullOrEmpty(duplicate))
            {
                this.AssociateTestViewModel.Duplicate = bool.Parse(duplicate);
            }

            this.DataContext = this.AssociateTestViewModel;
            this.cbTestType.SelectedIndex = 0;
        }

        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        /// <remarks>
        /// The method is also invoked when parent frames are about to navigate.
        /// </remarks>
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        /// <summary>
        /// Filters the tests.
        /// </summary>
        private void FilterTestsInternal()
        {
            if (this.AssociateTestViewModel == null)
            {
                return;
            }

            string fullNameFilter = tbFullName.Text.Equals(AssociateTestViewFilters.FullNameDefaultText) ? string.Empty : tbFullName.Text;
            string classNameFilter = tbClassName.Text.Equals(AssociateTestViewFilters.ClassNameDefaultText) ? string.Empty : tbClassName.Text;

            this.AssociateTestViewModel.FilterTests(fullNameFilter, classNameFilter);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbFullName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbFullName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbFullName.ClearDefaultContent(ref AssociateTestViewModel.AssociateTestViewFilters.IsFullNameFilterSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbFullName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbFullName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbFullName.RestoreDefaultText(AssociateTestViewFilters.FullNameDefaultText, ref AssociateTestViewModel.AssociateTestViewFilters.IsFullNameFilterSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbFullName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbFullName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.FilterTestsInternal();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbClassName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbClassName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbClassName.ClearDefaultContent(ref AssociateTestViewModel.AssociateTestViewFilters.IsClassNameFilterSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbClassName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbClassName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbClassName.RestoreDefaultText(AssociateTestViewFilters.ClassNameDefaultText, ref AssociateTestViewModel.AssociateTestViewFilters.IsClassNameFilterSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbClassName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbClassName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.FilterTestsInternal();
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbTestType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbTestType_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cbTestType.IsDropDownOpen = true;
            cbTestType.Focus();
        }

        /// <summary>
        /// Handles the MouseMove event of the cbTestType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbTestType_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnAssociate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAssociate_Click(object sender, RoutedEventArgs e)
        {
            this.AssociateTestInternal();
        }

        /// <summary>
        /// Associates the test internal.
        /// </summary>
        private void AssociateTestInternal()
        {
            Test currentSelectedTest = dgTests.SelectedItem as Test;
            string testType = cbTestType.Text;
            this.AssociateTestViewModel.AssociateTestCaseToTest(currentSelectedTest, testType);

            Log.InfoFormat("Navigate to Edit Test Case with id= {0}, test suite id= {1}, CreateNew= {2}, Duplicate= {3}", AssociateTestViewModel.TestCaseId, AssociateTestViewModel.TestSuiteId, AssociateTestViewModel.CreateNew, AssociateTestViewModel.Duplicate);
            this.NavigateToTestCasesEditView(AssociateTestViewModel.TestCaseId, AssociateTestViewModel.TestSuiteId, AssociateTestViewModel.CreateNew, AssociateTestViewModel.Duplicate);
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTests control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTests_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociateTestInternal();
        }        
    }
}
