using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace activate.Model
{
    public class WeatherData
    {
        private string dayOfWeek;
        public string DayOfWeek
        {
            get
            {
                return dayOfWeek;
            }
            set
            {
                if (dayOfWeek != value)
                {
                    NotifyPropertyChanging("DayOfWeek");
                    dayOfWeek = value;
                    NotifyPropertyChanged("DayOfWeek");

                }
            }
        }

        private string monthAndDay;
        public string MonthAndDay
        {
            get
            {
                return monthAndDay;
            }
            set
            {
                if (monthAndDay != value)
                {
                    NotifyPropertyChanging("MonthAndDay");
                    monthAndDay = value;
                    NotifyPropertyChanged("MonthAndDay");

                }
            }
        }

        private string high;
        public string High
        {
            get
            {
                return high;
            }
            set
            {
                if (high != value)
                {
                    NotifyPropertyChanging("High");
                    high = value;
                    NotifyPropertyChanged("High");

                }
            }
        }

        private string low;
        public string Low
        {
            get
            {
                return low;
            }
            set
            {
                if (low != value)
                {
                    NotifyPropertyChanging("Low");
                    low = value;
                    NotifyPropertyChanged("Low");

                }
            }
        }

        private string condition;
        public string Condition
        {
            get
            {
                return condition;
            }
            set
            {
                if (condition != value)
                {
                    NotifyPropertyChanging("Condition");
                    condition = value;
                    NotifyPropertyChanged("Condition");

                }
            }
        }

        private string imageSource;
        public string ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                if (imageSource != value)
                {
                    NotifyPropertyChanging("ImageSource");
                    imageSource = value;
                    NotifyPropertyChanged("ImageSource");

                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
