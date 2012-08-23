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
using activate.Model;
using activate.ViewModel;
using System.Windows.Navigation;

namespace activate
{
    public partial class NewCategory : PhoneApplicationPage
    {
        public NewCategory()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.ViewModel.LoadCollectionsFromDatabase();
        }

        private void addCategoryClick(object sender, RoutedEventArgs e)
        {
            if (CategoryField.Text == string.Empty)
            {
                MessageBox.Show("Please enter a name for your new category.");
                return;
            }
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
        }

        public IList<ToDoCategory> GetCategories()
        {
            IList<ToDoCategory> categoryList = null;
            using (ToDoDataContext context = new ToDoDataContext("Data Source=isostore:/ToDo.sdf"))
            {
                IQueryable<ToDoCategory> query = from c in context.Categories select c;
                categoryList = query.ToList();
            }
            return categoryList;
        }

    }
}