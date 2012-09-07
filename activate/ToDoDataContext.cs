using System;
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
    }

    [Table]
    public class ToDoItem : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int toDoItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ToDoItemId
        {
            get { return toDoItemId; }
            set
            {
                if (toDoItemId != value)
                {
                    NotifyPropertyChanging("ToDoItemId");
                    toDoItemId = value;
                    NotifyPropertyChanged("ToDoItemId");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string itemName;

        [Column]
        public string ItemName
        {
            get { return itemName; }
            set
            {
                if (itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define completion value: private field, public property, and database column.
        private bool isComplete;

        [Column]
        public bool IsComplete
        {
            get { return isComplete; }
            set
            {
                if (isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary version;


        // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int categoryId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<ToDoCategory> category;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "category", ThisKey = "categoryId", OtherKey = "Id")]
        public ToDoCategory Category
        {
            get { return category.Entity; }
            set
            {
                NotifyPropertyChanging("Category");
                category.Entity = value;

                if (value != null)
                {
                    categoryId = value.Id;
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
        private int id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return id; }
            set
            {
                NotifyPropertyChanging("Id");
                id = value;
                NotifyPropertyChanged("Id");
            }
        }

        // Define category name: private field, public property, and database column.
        private string name;

        [Column]
        public string Name
        {
            get { return name; }
            set
            {
                NotifyPropertyChanging("Name");
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary version;

        // Define the entity set for the collection side of the relationship.
        private EntitySet<ToDoItem> todos;

        [Association(Storage = "todos", OtherKey = "categoryId", ThisKey = "Id")]
        public EntitySet<ToDoItem> ToDos
        {
            get { return this.todos; }
            set { this.todos.Assign(value); }
        }


        // Assign handlers for the add and remove operations, respectively.
        public ToDoCategory()
        {
            todos = new EntitySet<ToDoItem>(
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
