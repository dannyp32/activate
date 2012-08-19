using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Windows.Navigation;

namespace activate
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            loadWeather();
        }

        private void loadWeather()
        {

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    WebClient webclient = new WebClient();
                    webclient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webclientDownloadStringCompleted);
                    webclient.DownloadStringAsync(new Uri("http://www.google.com/ig/api?weather=" + "Austin"));
                }
                catch (WebException e)
                {
                    MessageBox.Show("Activate was unable to load your weather forecast. Please tap on the cloud button to reload the weather or check your internet connection.1");
                }

            }
            else
            {
                MessageBox.Show("Activate was unable to load your weather forecast. Please tap on the cloud button to reload the weather or check your internet connection.2");
            }
        }

        private void webclientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            upcomingForecast.Items.Clear();

            XElement weatherXML = XElement.Parse(e.Result);

            foreach (var item in weatherXML.Descendants("current_conditions"))
            {
                weather.Text = (string)item.Element("temp_f").Attribute("data").Value;
            }

            currentDay.Text = getMonthAndDay(0);

            foreach (var item in weatherXML.Descendants("forecast_information"))
            {
                currentCity.Text = (string)item.Element("city").Attribute("data").Value;
            }

            foreach (var item in weatherXML.Descendants("forecast_conditions"))
            {
                WeatherInfo weatherData = new WeatherInfo();
                weatherData.day_of_week = (string)item.Element("day_of_week").Attribute("data").Value;
                weatherData.condition = "images\\weather\\cloud.png";
                //weatherData.condition = "images\\weather\\" + ((string)item.Element("condition").Attribute("data").Value) + ".png";
                weatherData.high = ((string)item.Element("high").Attribute("data").Value) + "°";
                weatherData.low = ((string)item.Element("low").Attribute("data").Value) + "°";
                upcomingForecast.Items.Add(weatherData);
            }

        }

        private string getMonthAndDay(int offset)
        {
            if (offset == 0)
            {
                DateTime day = DateTime.Today;
                string shortMonth = HelperMethods.getShortMonth(day.Month);
                string dayNumber = day.Day.ToString();
                string dayString = day.DayOfWeek.ToString();
                return dayString + ", " + shortMonth + " " + dayNumber;
            }
            else
            {
                DateTime day = DateTime.Today.AddDays(offset);
                string shortMonth = getShortMonth(day.Month);
                string dayNumber = day.Day.ToString();
                return shortMonth + " " + dayNumber;
            }


        }

        private string getShortMonth(int dateMonth)
        {
            string monthNow = "";
            switch (dateMonth)
            {
                case 1:
                    monthNow = "Jan";
                    break;
                case 2:
                    monthNow = "Feb";
                    break;
                case 3:
                    monthNow = "March";
                    break;
                case 4:
                    monthNow = "April";
                    break;
                case 5:
                    monthNow = "May";
                    break;
                case 6:
                    monthNow = "June";
                    break;
                case 7:
                    monthNow = "July";
                    break;
                case 8:
                    monthNow = "Aug";
                    break;
                case 9:
                    monthNow = "Sept";
                    break;
                case 10:
                    monthNow = "Oct";
                    break;
                case 11:
                    monthNow = "Nov";
                    break;
                case 12:
                    monthNow = "Dec";
                    break;
                default:
                    monthNow = "Couldn't retrieve month :/";
                    break;
            }

            return monthNow;
        }


        private void todopage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            this.NavigationService.Navigate(new Uri("/Todo.xaml", UriKind.Relative));

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            loadWeather();
        }

    }

}