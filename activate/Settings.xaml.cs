using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;

namespace activate
{
    public partial class Settings : PhoneApplicationPage
    {
        IsolatedStorageSettings settings;
        public Settings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            if (settings.Contains("WOEID"))
            {
                WOEIDField.Text = (string)settings["WOEID"];
            }

            if (settings.Contains("isCelcius"))
            {
                if (((bool)settings["isCelcius"]) == true)
                {
                    celcius.IsChecked = true;
                }
                else
                {
                    fahrenheit.IsChecked = true;
                }
            }
            else
            {
                fahrenheit.IsChecked = true;
            }
        }

        public void AddOrUpdateValue(string Key, Object value)
        {
            // If the key exists
            if (settings.Contains(Key))
            {
                // If the value has changed
                if (settings[Key] != value)
                {
                    // Store the new value
                    settings[Key] = value;
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add(Key, value);
            }
        }

        private void findWOEID_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("To find your WOEID enter your location in top right search field labeled \"Find my location\" and then copy the WOEID and paste it here.");
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri("http://isithackday.com/geoplanet-explorer/", UriKind.Absolute);

            webBrowserTask.Show();
        }

        private void weather_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void addLocation_Click(object sender, RoutedEventArgs e)
        {
            if (WOEIDField.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid WOEID. To find your WOEID, please click on the \"Find my WOEID\" button.");
                return;
            }
            // If the key exists
            if (settings.Contains("WOEID"))
            {
                // If the value has changed
                if ((string)settings["WOEID"] != WOEIDField.Text)
                {
                    // Store the new value
                    settings["WOEID"] = WOEIDField.Text;
                    MessageBox.Show("The updated WOEID Value is: " + settings["WOEID"]);
                }
            }
            // Otherwise create the key.
            else
            {
                settings.Add("WOEID", WOEIDField.Text);
                MessageBox.Show("Your WOEID " + settings["WOEID"] + "has been added successfully");
            }
        }

        private void fahrenheit_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void celcius_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}