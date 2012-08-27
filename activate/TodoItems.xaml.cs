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
using System.Text;
using activate.Model;
using activate.ViewModel;

namespace activate
{
    public partial class TodoItems : PhoneApplicationPage
    {
        public TodoItems()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.DataContext = App.ViewModel;
            loaditems();
            base.OnNavigatedTo(e);
        }

        private void actionClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewTodoItem.xaml", UriKind.Relative));
        }

        private void weatherClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Save changes to the database.
            App.ViewModel.SaveChangesToDB();
        }

        private void loaditems()
        {
            App.ViewModel.AllToDoItems.Clear();
            App.ViewModel.AllToDoItems = new System.Collections.ObjectModel.ObservableCollection<ToDoItem>(getItems());
        }

        public IList<ToDoItem> getItems()
        {
            IList<ToDoItem> itemsList;
            using (ToDoDataContext TodoDB = new ToDoDataContext())
            {
                IQueryable<ToDoItem> query = from item in TodoDB.Items where item._categoryId == App.ViewModel.ActiveCategory.Id select item;
                itemsList = query.ToList();
            }
            return itemsList;
        }
        
    }
}