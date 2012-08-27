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

        private ToDoCategory _activeCategory;
        public ToDoCategory ActiveCategory
        {
            get { return _activeCategory; }
            set
            {
                _activeCategory = value;
                NotifyPropertyChanged("ActiveCategory");
            }
        }

        // All to-do items.
        private ObservableCollection<ToDoItem> _allToDoItems;
        public ObservableCollection<ToDoItem> AllToDoItems
        {
            get { return _allToDoItems; }
            set
            {
                _allToDoItems = value;
                NotifyPropertyChanged("AllToDoItems");
            }
        }

        // A list of all categories, used by the add task page.
        private List<ToDoCategory> _categoriesList;
        public List<ToDoCategory> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
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
            var toDoCategoriesInDB = from ToDoCategory category in toDoDB.Categories
                                     select category;

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

            /*
            using (ToDoDataContext ToDoDB = new ToDoDataContext("Data Source=isostore:/ToDo.sdf"))
            {
                ToDoCategory newCategory = new ToDoCategory
                {
                    Name = CategoryField.Text
                };

                //check if it already exists by iterating through all
                IList<ToDoCategory> categories = this.GetCategories();
                foreach (ToDoCategory todoCateg in categories)
                {
                    if (todoCateg.Name.Equals(newCategory.Name))
                    {
                        MessageBox.Show("The category already exists");
                        return;
                    }
                }

                ToDoDB.Categories.InsertOnSubmit(newCategory);
                ToDoDB.SubmitChanges();
                MessageBox.Show("Your new category has been added successfully!");
                this.NavigationService.Navigate(new Uri("/Todo.xaml", UriKind.Relative));
            }
             * */

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
