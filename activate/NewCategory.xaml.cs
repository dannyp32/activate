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

namespace activate
{
    public partial class NewCategory : PhoneApplicationPage
    {
        public NewCategory()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ToDoDataContext ToDoDB = new ToDoDataContext("Data Source=isostore:/ToDo.sdf"))
            {
                ToDoCategory newCategory = new ToDoCategory
                {
                    Name = CategoryField.Text,
                    DayCreated = DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString(),
                    MonthCreated = HelperMethods.getShortMonth(DateTime.Now.Month)
                };
                ToDoDB.Categories.InsertOnSubmit(newCategory);
                ToDoDB.SubmitChanges();
                MessageBox.Show("Your new category was added successfully!");
                System.Console.WriteLine(newCategory.Name);
                System.Console.WriteLine(newCategory.DayCreated);
                System.Console.WriteLine(newCategory.MonthCreated);
                this.NavigationService.Navigate(new Uri("/Todo.xaml", UriKind.Relative));
            }
        }
    }
}