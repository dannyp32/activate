using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using activate.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace activate.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private ToDoDataContext weatherDB;

        // Class constructor, create the data context object.
        public WeatherViewModel()
        {
            weatherDB = new ToDoDataContext();
        }

        // All to-do items.
        private ObservableCollection<WeatherItem> _weatherItems;
        public ObservableCollection<WeatherItem> WeatherItems
        {
            get { return _weatherItems; }
            set
            {
                _weatherItems = value;
                NotifyPropertyChanged("WeatherItems");
            }
        }



        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            weatherDB.SubmitChanges();
        }

        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {
            // Specify the query for all to-do items in the database.
            var weatherItemsInDB = from WeatherItem weather in weatherDB.Items
                                   select weather;

            // Query the database and load all to-do items.
            WeatherItems = new ObservableCollection<WeatherItem>(weatherItemsInDB);
        }



        // Add a to-do item to the database and collections.
        public void AddToDoItem(WeatherItem newWeatherItem)
        {
            // Add a to-do item to the data context.
            weatherDB.Forecasts.InsertOnSubmit(newWeatherItem);

            // Save changes to the database.
            weatherDB.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            WeatherItems.Add(newWeatherItem);

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