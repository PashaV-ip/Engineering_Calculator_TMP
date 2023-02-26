using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Engineering_Calculator
{
    public static class Functions
    {
        //Тут думаю без коментариев
        public static string Plus(string num1, string num2)
        {
            double a;
            double b;

            if (!Double.TryParse(num1, out a) || !Double.TryParse(num2, out b))
            {
                return null;
            }
            return (a + b).ToString();
        }
        public static string Minus(string num1, string num2)
        {
            double a;
            double b;

            if (!Double.TryParse(num1, out a) || !Double.TryParse(num2, out b))
            {
                return null;
            }
            return (a - b).ToString();
        }
        public static string Multiply(string num1, string num2)
        {
            double a;
            double b;

            if (!Double.TryParse(num1, out a) || !Double.TryParse(num2, out b))
            {
                return null;
            }
            return (a * b).ToString();
        }
        public static string Divide(string num1, string num2)
        {
            double a;
            double b;

            if (!Double.TryParse(num1, out a) || !Double.TryParse(num2, out b))
            {
                return null;
            }
            if (b == 0)
            {
                MessageBox.Show("На ноль делить нельзя!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            return (a / b).ToString();
        }
        public static string Percent(string num1, string num2)
        {
            double a;
            double b;

            if (!Double.TryParse(num1, out a) || !Double.TryParse(num2, out b))
            {
                return null;
            }
            return (a * b / 100).ToString();
        }
        public static string Sqrt(string num1)
        {
            double a;

            if (!Double.TryParse(num1, out a))
            {
                return null;
            }
            return Math.Sqrt(a).ToString();
        }
        public static string Pow(string num1)
        {
            double a;

            if (!Double.TryParse(num1, out a)) 
            { 
                return null; 
            }
            return Math.Pow(a, 2).ToString();
        }
        public static string OneDev(string num1)
        {
            double a;

            if (!Double.TryParse(num1, out a)) 
            {
                return null;
            }
            if (a == 0)
            {
                MessageBox.Show("На ноль делить нельзя!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
            return (1 / a).ToString();
        }
    }
}
