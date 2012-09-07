using System;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace activate
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.WeatherViewModel.reloadWeather();
            this.DataContext = App.WeatherViewModel;
            base.OnNavigatedTo(e);
        }
        private void todopage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Todo.xaml", UriKind.Relative));
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void settings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }
    }
}