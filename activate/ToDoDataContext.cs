﻿using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace activate.Model
{
    public class ToDoDataContext : DataContext
    {

        // Pass the connection string to the base class.
        public ToDoDataContext()
            : base("Data Source=isostore:/AppDB.sdf")
        { }

        public ToDoCategory activeCategory;

        // Specify a table for the to-do items.
        public Table<ToDoItem> Items;

        // Specify a table for the categories.
        public Table<ToDoCategory> Categories;

        //Weather stuff
        public WeatherItem currentWeather;

        // Specify a table for the to-do items.
        public Table<WeatherItem> Forecasts;
    }
 
      [Table]
    public class WeatherItem : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _weatherItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int WeatherItemId
        {
            get { return _weatherItemId; }
            set
            {
                if (_weatherItemId != value)
                {
                    NotifyPropertyChanging("WeatherItemId");
                    _weatherItemId = value;
                    NotifyPropertyChanged("WeatherItemId");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _itemDay;

        [Column]
        public string ItemDay
        {
            get { return _itemDay; }
            set
            {
                if (_itemDay != value)
                {
                    NotifyPropertyChanging("ItemDay");
                    _itemDay = value;
                    NotifyPropertyChanged("ItemDay");
                }
            }
        }

        private string _dayOfWeek;

        [Column]
        public string DayOfWeek
        {
            get { return _dayOfWeek; }
            set
            {
                if (_dayOfWeek != value)
                {
                    NotifyPropertyChanging("DayOfWeek");
                    _dayOfWeek = value;
                    NotifyPropertyChanged("DayOfWeek");
                }
            }
        }

        // Define completion value: private field, public property, and database column.

        private string _high;

        [Column]
        public string High
        {
            get { return _high; }
            set
            {
                if (_high != value)
                {
                    NotifyPropertyChanging("High");
                    _high = value;
                    NotifyPropertyChanged("High");
                }
            }
        }

        private string _low;

        [Column]
        public string Low
        {
            get { return _low; }
            set
            {
                if (_low != value)
                {
                    NotifyPropertyChanging("Low");
                    _low = value;
                    NotifyPropertyChanged("Low");
                }
            }
        }

        private string _condition;

        [Column]
        public string Condition
        {
            get { return _condition; }
            set
            {
                if (_condition != value)
                {
                    NotifyPropertyChanging("Condition");
                    _condition = value;
                    NotifyPropertyChanged("Condition");
                }
            }
        }

        private string _imageSource;
        [Column]
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (_imageSource != value)
                {
                    NotifyPropertyChanging("ImageSource");
                    _imageSource = value;
                    NotifyPropertyChanged("ImageSource");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

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
  

    [Table]
    public class ToDoItem : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _toDoItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ToDoItemId
        {
            get { return _toDoItemId; }
            set
            {
                if (_toDoItemId != value)
                {
                    NotifyPropertyChanging("ToDoItemId");
                    _toDoItemId = value;
                    NotifyPropertyChanged("ToDoItemId");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _itemName;

        [Column]
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define completion value: private field, public property, and database column.
        private bool _isComplete;

        [Column]
        public bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                if (_isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;


        // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int _categoryId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<ToDoCategory> _category;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_category", ThisKey = "_categoryId", OtherKey = "Id")]
        public ToDoCategory Category
        {
            get { return _category.Entity; }
            set
            {
                NotifyPropertyChanging("Category");
                _category.Entity = value;

                if (value != null)
                {
                    _categoryId = value.Id;
                }

                NotifyPropertyChanging("Category");
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


    [Table]
    public class ToDoCategory : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return _id; }
            set
            {
                NotifyPropertyChanging("Id");
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        // Define category name: private field, public property, and database column.
        private string _name;

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        // Define the entity set for the collection side of the relationship.
        private EntitySet<ToDoItem> _todos;

        [Association(Storage = "_todos", OtherKey = "_categoryId", ThisKey = "Id")]
        public EntitySet<ToDoItem> ToDos
        {
            get { return this._todos; }
            set { this._todos.Assign(value); }
        }


        // Assign handlers for the add and remove operations, respectively.
        public ToDoCategory()
        {
            _todos = new EntitySet<ToDoItem>(
                new Action<ToDoItem>(this.attach_ToDo),
                new Action<ToDoItem>(this.detach_ToDo)
                );
        }

        // Called during an add operation
        private void attach_ToDo(ToDoItem toDo)
        {
            NotifyPropertyChanging("ToDoItem");
            toDo.Category = this;
        }

        // Called during a remove operation
        private void detach_ToDo(ToDoItem toDo)
        {
            NotifyPropertyChanging("ToDoItem");
            toDo.Category = null;
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
