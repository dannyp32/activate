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
    public partial class NewTodoItem : PhoneApplicationPage
    {
        public NewTodoItem()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.ViewModel.LoadCollectionsFromDatabase();
        }

        private void addClick(object sender, RoutedEventArgs e)
        {
            if (TaskField.Text == string.Empty)
            {
                MessageBox.Show("Please enter a new task.");
                return;
            }
            if (App.ViewModel.ActiveCategory == null)
            {
                MessageBox.Show("The current category is null");
                return;
            }
            using (ToDoDataContext ToDoDB = new ToDoDataContext())
            {

                ToDoItem newItem = new ToDoItem
                {
                    ItemName = TaskField.Text,
                    _categoryId = App.ViewModel.ActiveCategory.Id,
                    IsComplete = false
                };
                ToDoDB.Items.InsertOnSubmit(newItem);
                ToDoDB.SubmitChanges();
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    this.NavigationService.Navigate(new Uri("/TodoItems.xaml", UriKind.Relative));
                }
            }
        }
    }
}
