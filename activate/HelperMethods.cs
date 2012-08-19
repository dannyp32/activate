using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace activate
{
    public static class HelperMethods
    {
        public static System.Collections.ObjectModel.ObservableCollection<T> ToObservableCollection<T>(this System.Collections.Generic.IEnumerable<T> coll)
        {
            var c = new System.Collections.ObjectModel.ObservableCollection<T>();
            foreach (var e in coll) c.Add(e);
            return c;
        }

        public static string getShortMonth(int dateMonth)
        {
            string monthNow = "";
            switch (dateMonth)
            {
                case 1:
                    monthNow = "Jan";
                    break;
                case 2:
                    monthNow = "Feb";
                    break;
                case 3:
                    monthNow = "March";
                    break;
                case 4:
                    monthNow = "April";
                    break;
                case 5:
                    monthNow = "May";
                    break;
                case 6:
                    monthNow = "June";
                    break;
                case 7:
                    monthNow = "July";
                    break;
                case 8:
                    monthNow = "Aug";
                    break;
                case 9:
                    monthNow = "Sept";
                    break;
                case 10:
                    monthNow = "Oct";
                    break;
                case 11:
                    monthNow = "Nov";
                    break;
                case 12:
                    monthNow = "Dec";
                    break;
                default:
                    monthNow = "Couldn't retrieve month :/";
                    break;
            }

            return monthNow;
        }
    }
}
