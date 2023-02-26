using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Engineering_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Свойства
        private bool IsDecimal = false; // Переменная определяющая - является ли число десятичным
        private bool IsNumberTwo = false; // Переменная определяющая - какое число вводит пользователь (Второе или первое) true - второе, false - первое

        private string NumberOne = null; // Число номер 1
        private string NumberTwo = null; // Число номер 2

        private string CurrentFunction = ""; //Содержит функцию, которую нажал пользователь
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }



        private void AddNumber(string txt) // Метод добавляет в нужное число и строку результата символы из атрибута 'txt'
        {
            if (IsNumberTwo)
            {
                NumberTwo += txt;
                ResultScoreboard_TextBox.Text = NumberTwo;
            }
            else
            {
                NumberOne += txt;
                ResultScoreboard_TextBox.Text = NumberOne;
            }
        }
        private void SetNumber(string txt) // Метод ставит в нужное число и строку результата символы из атрибута 'txt'
        {
            if (IsNumberTwo)
            {
                NumberTwo = txt;
                ResultScoreboard_TextBox.Text = NumberTwo;
            }
            else
            {
                NumberOne = txt;
                ResultScoreboard_TextBox.Text = NumberOne;
            }
        }
        private void Buttons_With_Numbers_Click(object sender, RoutedEventArgs e) //Обработчик нажатия на все кнопки, работающие с данным числом в результирующей строке, в том числе и цифры
        {
            var txt = ((Button)sender).Content.ToString(); //в переменную txt добавляется не сама кнопка, а текст с нажатой кнопки
            {
                if(IsDecimal && txt == "•") // Предотвращает вторую запятую в числе
                {
                    MessageBox.Show("Число десятичное!","Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if(txt == "•") // Если пользователь нажал на точку и число не было десятичным, то число переводится в разряд десятичных и содержимое txt заменяется на ","
                {
                    IsDecimal = true;
                    txt = ",";
                }
            }

            if (txt == "+/-") // Если пользователь нажал на кнопку "+/-", то выполняется следующая часть кода
            {
                if (ResultScoreboard_TextBox.Text.Length > 0) //Проверка, если результирующее поле не пустое - то..
                    if(ResultScoreboard_TextBox.Text[0] == '-') //Условие позволяет переводить число из отрицательного в положительный и обратно
                    {
                        ResultScoreboard_TextBox.Text = ResultScoreboard_TextBox.Text.Substring(1, ResultScoreboard_TextBox.Text.Length - 1);
                    }
                    else
                    {
                        ResultScoreboard_TextBox.Text = "-" + ResultScoreboard_TextBox.Text;
                    }
                    //###########################################################################################################//
                    //###                             (Почему нельзя использовать пример ниже)                                ###//
                    //###########################################################################################################//
                    //###                                                                                                     ###//
                    //###    ResultScoreboard_TextBox.Text = (double.Parse(ResultScoreboard_TextBox.Text) * -1).ToString();   ###//
                    //###                                                                                                     ###//
                    //###########################################################################################################//
                    //###                                                                                                     ###//
                    //###   Данное условие позволяет избегать некоторых ошибок, например - (Данное действие можно выполнить   ###//
                    //###   только если поле не пустое, но пользователь может написать запятую, а потом нажать "-", что       ###//
                    //###   приведёт к крашу программы, а что бы не делать ещё больше разных условий, лучше применить         ###//
                    //###   этот метод с условием "...if(ResultScoreboard_TextBox.Text[0] == '-')..." во избежание лишней     ###//
                    //###   рутины с реализацией..                                                                            ###//
                    //###                                                                                                     ###//
                    //###########################################################################################################//


                SetNumber(ResultScoreboard_TextBox.Text);
                return;
            }
            AddNumber(txt);
        }

        private void Buttons_With_Primitive_Functions_Click(object sender, RoutedEventArgs e) // Обработчик нажатия на математические функции
        {
            if (IsNumberTwo) //Условие позволяет произвести расчёт, если пользователь после ввода второго числа нажмёт не "=", а какую-либо функцию
            {
                IsNumberTwo = false;
                CalculateResult(CurrentFunction);
                NumberOne = null;
                NumberTwo = null;
            }
            if(NumberOne == null) //Позволяет определить, ввёл-ли бользователь число перед нажатием функции
            {
                if (ResultScoreboard_TextBox.Text.Length > 0) NumberOne = ResultScoreboard_TextBox.Text;
                else
                {
                    MessageBox.Show("Введите число!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            IsNumberTwo = true; //После нажатия на любую функцию, программа меняет своё взаимодействия на другое число
            CurrentFunction = ((Button)sender).Content.ToString(); //Функция с кнопки
            CalculateResult(CurrentFunction);
        }

        private void CalculateResult(string operation) // Метод вычисляющий результат согласно выбранной функции
        {
            string result = null;
            switch (operation) //Обращение к классу Functions в соответствии с выбранной функцией
            {
                case "+": 
                    result = Functions.Plus(NumberOne, NumberTwo);
                    break;

                case "-":
                    result = Functions.Minus(NumberOne, NumberTwo);
                    break;

                case "×":
                    result = Functions.Multiply(NumberOne, NumberTwo);
                    break;

                case "÷":
                    result = Functions.Divide(NumberOne, NumberTwo);
                    break;

                case "%":
                    result = Functions.Percent(NumberOne, NumberTwo);
                    break;

                case "√":
                    result = Functions.Sqrt(NumberOne);
                    IsNumberTwo = false;
                    break;

                case "x²":
                    result = Functions.Pow(NumberOne);
                    IsNumberTwo = false;
                    break;
                case "1/x":
                    result = Functions.OneDev(NumberOne);
                    IsNumberTwo = false;
                    break;

                default: break;
            }
            OutputResult(result, operation);
            if(IsNumberTwo)
            {
                if (result != null) NumberOne = result; 
            }
            else
            {
                NumberOne = null;
            }
            IsDecimal = false;
        }

        private void OutputResult(string result, string operation) // Метод выводит в соответствующем формате историю действия в историческую строку
        {
            switch (operation)
            {
                case "√":
                    if (NumberOne != null) textHistory.Text = "√" + NumberOne + " = ";
                    break;

                case "x²":
                    if (NumberOne != null) textHistory.Text = NumberOne + "² = ";
                    break;

                case "1/x":
                    if (NumberOne != null) textHistory.Text = "1/" + NumberOne + " = ";
                    break;

                default:
                    {
                        if (NumberTwo != null)
                        {
                            textHistory.Text = NumberOne + " " + operation + " " + NumberTwo + " = ";
                        }
                        else
                        {
                            if (NumberOne != null)
                            {
                                textHistory.Text = NumberOne + " " + operation + " ";
                                break;
                            }
                        }
                    }
                    break;
            }

            NumberTwo = null;
            if(result != null)
            {
                ResultScoreboard_TextBox.Text = result;
            }
        }

        private void Buttons_With_Official_Functions_Click(object sender, RoutedEventArgs e) // Обработчик нажатия на служебные функции
        {
            Button button = (Button)sender;
            switch (button.Content)
            {
                case "C": //Полное очищение
                    ResultScoreboard_TextBox.Text = "";
                    textHistory.Text = "";
                    IsNumberTwo = false;
                    CurrentFunction = null;
                    NumberOne = null;
                    NumberTwo = null;
                    IsDecimal = false;
                    break;
                case "=": //Расчёт
                    CalculateResult(CurrentFunction);
                    IsNumberTwo = false;
                    NumberOne = null;
                    NumberTwo = null;
                    break;
                case "→": //BackSpace
                    if(ResultScoreboard_TextBox.Text.Length <=0) //Предотвращает стирание пустой строки
                    {
                        return;
                    }
                    ResultScoreboard_TextBox.Text = ResultScoreboard_TextBox.Text.Substring(0, ResultScoreboard_TextBox.Text.Length - 1);
                    SetNumber(ResultScoreboard_TextBox.Text);
                    break;
                case "CE": // Очищение числа взаимодействия
                    if (ResultScoreboard_TextBox.Text.Length <= 0)
                    {
                        return;
                    }
                    ResultScoreboard_TextBox.Text = "";
                    SetNumber(ResultScoreboard_TextBox.Text);
                    break;
            }
        }

    }
}
