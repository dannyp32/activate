using System;
using System.Net;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using activate.Model;
using System.IO.IsolatedStorage;
using System.ComponentModel;

namespace activate.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        IsolatedStorageSettings settings;
        // Class constructor, create the data context object.
        public WeatherViewModel()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
            loadYahooWeather();
        }

        // All to-do items.
        private ObservableCollection<WeatherData> weatherList;
        public ObservableCollection<WeatherData> WeatherList
        {
            get 
            {
                if (weatherList == null)
                {
                    weatherList = new ObservableCollection<WeatherData>();
                }
                return weatherList; 
            }
        }

        private string mainWeather;
        public string MainWeather
        {
            get { return mainWeather; }
            set
            {
                mainWeather = value;
                NotifyPropertyChanged("MainWeather");
            }
        }

        private string mainCity;
        public string MainCity
        {
            get { return mainCity; }
            set
            {
                mainCity = value;
                NotifyPropertyChanged("MainCity");
            }
        }

        private string mainDay;
        public string MainDay
        {
            get { return mainDay; }
            set
            {
                mainDay = value;
                NotifyPropertyChanged("MainDay");
            }
        }

        private bool woeidUpdated;
        public bool WOEIDUpdated
        {
            get { return woeidUpdated; }
            set
            {
                woeidUpdated = value;
                NotifyPropertyChanged("WOEIDUpdated");
            }
        }

        private string getLinkParameters()
        {
            string linkParameters;
            if (settings.Contains("WOEID"))
            {
                linkParameters = (string)settings["WOEID"];
            }
            else
            {
                linkParameters = "2442047";
            }
            if (settings.Contains("isCelcius"))
            {
                if (((bool)settings["isCelcius"]) == true)
                {
                    linkParameters += "&u=c";
                }
            }
            return linkParameters;
        }

        public void reloadWeather()
        {
            loadYahooWeather();
        }

        private void loadYahooWeather()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    WebClient webclient = new WebClient();
                    webclient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webclientDownloadStringCompleted);
                    webclient.DownloadStringAsync(new Uri("http://weather.yahooapis.com/forecastrss?w=" + getLinkParameters()));
                }
                catch (WebException webException)
                {
                    MessageBox.Show("Activate had a problem retrieving your weather forecast from yahoo. Please check if your WOEID is correct. If the problem persists, feel free to contact the developer through the settings page.");
                }
                catch (Exception e)
                {
                    MessageBox.Show("An unexpected error occured. Please try to reload activate. If the problem persists, feel free to contact the developer through the settings page.");
                }
            }
            else
            {
                MessageBox.Show("Activate was unable to load your weather. Please check your internet connection.");
            }
        }

        private void webclientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            WeatherList.Clear();

            XNamespace yweather = "http://xml.weather.yahoo.com/ns/rss/1.0";
            XDocument weatherXML = XDocument.Parse(e.Result);
            MainDay = getMonthAndDay(0);

            try
            {
                foreach (XElement _row in weatherXML.Root.Elements("channel"))
                {
                    MainCity = (string)_row.Element(yweather + "location").Attribute("city").Value + ", " +
                                       (string)_row.Element(yweather + "location").Attribute("region").Value;
                }

                foreach (XElement _row in weatherXML.Root.Elements("channel"))
                {
                    MainWeather = (string)_row.Element("item").Element(yweather + "condition").Attribute("temp").Value;
                }

                foreach (XElement _row in weatherXML.Root.Elements("channel"))
                {
                    foreach (XElement _child in _row.Elements("item"))
                    {
                        int i = 0;
                        foreach (XElement _thirdChild in _child.Elements(yweather + "forecast"))
                        {
                            WeatherData weatherData = new WeatherData();
                            weatherData.DayOfWeek = ((string)_thirdChild.Attribute("day").Value).ToUpper();
                            weatherData.MonthAndDay = getMonthAndDayShort(i);
                            weatherData.Low = (string)_thirdChild.Attribute("low").Value + "°";
                            weatherData.High = (string)_thirdChild.Attribute("high").Value + "°";
                            weatherData.Condition = (string)_thirdChild.Attribute("text").Value;
                            weatherData.ImageSource = "images\\Weather\\" + (string)_thirdChild.Attribute("code").Value + ".png";
                            WeatherList.Add(weatherData);
                            i++;
                        }
                    }

                }
            }
            catch(NullReferenceException nullException)
            {
                MessageBox.Show("The WOEID you entered is not valid. Please double check that you've entered it correctly. If the problem continues, feel free to email the developer. We will be glad to assist you");
            }
        }
        
        //Format of the returned string will be "Monday, Sept 02"
        private string getMonthAndDay(int offset)
        {
            if (offset == 0)
            {
                DateTime day = DateTime.Today;
                string shortMonth = getShortMonth(day.Month);
                string dayNumber = day.Day.ToString();
                string dayString = day.DayOfWeek.ToString();
                return dayString + ", " + shortMonth + " " + dayNumber;
            }
            else
            {
                DateTime day = DateTime.Today.AddDays(offset);
                string shortMonth = getShortMonth(day.Month);
                string dayNumber = day.Day.ToString();
                string dayString = day.DayOfWeek.ToString();
                return dayString + ", " + shortMonth + " " + dayNumber;

            }
        }

        //Format of the returned string will be "Sept 02"
        private string getMonthAndDayShort(int offset)
        {
            if (offset == 0)//means it's going to return today's date
            {
                DateTime day = DateTime.Today;
                string shortMonth = getShortMonth(day.Month);
                string dayNumber = day.Day.ToString();
                return shortMonth + " " + dayNumber;
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
                    monthNow = "Mar";
                    break;
                case 4:
                    monthNow = "Apr";
                    break;
                case 5:
                    monthNow = "May";
                    break;
                case 6:
                    monthNow = "Jun";
                    break;
                case 7:
                    monthNow = "Jul";
                    break;
                case 8:
                    monthNow = "Aug";
                    break;
                case 9:
                    monthNow = "Sep";
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}