using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Length_converter
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class StartPage : Length_converter.Common.LayoutAwarePage
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void pageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ApplicationView.Value == ApplicationViewState.Snapped)
            {
                NormalGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                SnappedGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                FirstComboSnapped.SelectedIndex = FirstCombo.SelectedIndex;
                SecondComboSnapped.SelectedIndex = SecondCombo.SelectedIndex;
            }
            else
            {
                NormalGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                SnappedGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                FirstCombo.SelectedIndex = FirstComboSnapped.SelectedIndex;
                SecondCombo.SelectedIndex = SecondComboSnapped.SelectedIndex;
            }
        }

        private async void Convert(object sender, RoutedEventArgs e)
        {
            await conversion();
        }

        double convert(int s1, int s2, double value)
        {
            if (s1 == s2)
            { }
            else if (s1 == 0)
            {
                if (s2 == 1)
                {
                    value = value * 39.37;
                }
                else if (s2 == 2)
                {
                    value = value * 3.281;
                }
                else if (s2 == 3)
                {
                    value = value * 1.094;
                }
                else if (s2 == 4)
                {
                    value = value * 0.000621;
                }
            }
            else if (s1 == 1)
            {
                if (s2 == 0)
                {
                    value = value * 0.0254;
                }
                else if (s2 == 2)
                {
                    value = value * 0.0833;
                }
                else if (s2 == 3)
                {
                    value = value * 0.0277;
                }
                else if (s2 == 4)
                {
                    value = value * 0.0000158;
                }
            }
            else if (s1 == 2)
            {
                if (s2 == 0)
                {
                    value = value * 0.3048;
                }
                else if (s2 == 1)
                {
                    value = value * 12;
                }
                else if (s2 == 3)
                {
                    value = value * 0.333;
                }
                else if (s2 == 4)
                {
                    value = value * 0.000189;
                }
            }
            else if (s1 == 3)
            {
                if (s2 == 0)
                {
                    value = value * 0.9144;
                }
                else if (s2 == 1)
                {
                    value = value * 36;
                }
                else if (s2 == 2)
                {
                    value = value * 3;
                }
                else if (s2 == 4)
                {
                    value = value * 0.000568;
                }
            }
            else if (s1 == 4)
            {
                if (s2 == 0)
                {
                    value = value * 1609.344;
                }
                else if (s2 == 1)
                {
                    value = value * 63360;
                }
                else if (s2 == 2)
                {
                    value = value * 5280;
                }
                else if (s2 == 3)
                {
                    value = value * 1760;
                }
            }
            return value;
        }

        public async Task conversion()
        {
            double value;
            double snappedValue;
            bool IsNumber;
            int s1;
            int s2;

            if (Input.Text.Contains(","))
            {
                Input.Text = Input.Text.Replace(',', '.');
            }

            if (ApplicationView.Value == ApplicationViewState.Snapped)
            {
                IsNumber = double.TryParse(InputSnapped.Text, out snappedValue);
                value = snappedValue;
                Input.Text = InputSnapped.Text;
                s1 = FirstComboSnapped.SelectedIndex;
                s2 = SecondComboSnapped.SelectedIndex;
            }
            else
            {
                IsNumber = double.TryParse(Input.Text, out value);
                snappedValue = value;
                InputSnapped.Text = Input.Text;
                s1 = FirstCombo.SelectedIndex;
                s2 = SecondCombo.SelectedIndex;
            }

            if (value < 0)
            {
                IsNumber = false;
            }

            if (IsNumber)
            {
                Output.Text = convert(s1, s2, value).ToString();
                OutputSnapped.Text = Output.Text;
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Convert only positive numbers", "Invalid conversion");

                dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                dialog.Commands.Add(new UICommand("Ok", (args) =>
                {
                    Input.Text = "";
                    Output.Text = "0";
                    InputSnapped.Text = "";
                    OutputSnapped.Text = "0";
                }));

                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 0;

                await dialog.ShowAsync();
            }
        }
    }
}
