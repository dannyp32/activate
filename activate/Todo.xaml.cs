using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using System.Windows.Media;
using activate.Model;

namespace activate
{
    public partial class Todo : PhoneApplicationPage
    {
        public Todo()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void weather_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/NewCategory.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Save changes to the database.
            App.ViewModel.SaveChangesToDB();
        }

        private void actionClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/NewCategory.xaml", UriKind.Relative));
        }

    }

}