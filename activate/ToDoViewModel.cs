using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using activate.Model;
using System.Windows;
using System;


namespace activate.ViewModel
{
    public class ToDoViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private ToDoDataContext toDoDB;

        // Class constructor, create the data context object.
        public ToDoViewModel()
        {
            toDoDB = new ToDoDataContext();
        }

        private ToDoCategory activeCategory;
        public ToDoCategory ActiveCategory
        {
            get { return activeCategory; }
            set
            {
                activeCategory = value;
                NotifyPropertyChanged("ActiveCategory");
            }
        }

        // All to-do items.
        private ObservableCollection<ToDoItem> allToDoItems;
        public ObservableCollection<ToDoItem> AllToDoItems
        {
            get { return allToDoItems; }
            set
            {
                allToDoItems = value;
                NotifyPropertyChanged("AllToDoItems");
            }
        }

        // A list of all categories, used by the add task page.
        private List<ToDoCategory> categoriesList;
        public List<ToDoCategory> CategoriesList
        {
            get { return categoriesList; }
            set
            {
                categoriesList = value;
                NotifyPropertyChanged("CategoriesList");
            }
        }

        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            toDoDB.SubmitChanges();
        }

        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {

            // Specify the query for all to-do items in the database.
            var toDoItemsInDB = from ToDoItem todo in toDoDB.Items
                                select todo;

            // Query the database and load all to-do items.
            AllToDoItems = new ObservableCollection<ToDoItem>(toDoItemsInDB);

            // Specify the query for all categories in the database.
            var toDoCategoriesInDB = from ToDoCategory myCategory in toDoDB.Categories
                                     select myCategory;

            // Load a list of all categories.
            CategoriesList = toDoDB.Categories.ToList();

            //CategoriesList = new ObservableCollection<ToDoCategory>(toDoCategoriesInDB);

        }

        // Add a to-do item to the database and collections.
        public void AddToDoItem(ToDoItem newToDoItem)
        {
            // Add a to-do item to the data context.
            toDoDB.Items.InsertOnSubmit(newToDoItem);

            // Save changes to the database.
            toDoDB.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            AllToDoItems.Add(newToDoItem);
        }

        // Remove a to-do task item from the database and collections.
        public void DeleteToDoItem(ToDoItem toDoForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllToDoItems.Remove(toDoForDelete);

            // Remove the to-do item from the data context.
            toDoDB.Items.DeleteOnSubmit(toDoForDelete);

            // Save changes to the database.
            toDoDB.SubmitChanges();
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
