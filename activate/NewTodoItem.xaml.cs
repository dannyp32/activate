using System;
using System.Windows;
using Microsoft.Phone.Controls;
using activate.Model;
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
                    categoryId = App.ViewModel.ActiveCategory.Id,
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
