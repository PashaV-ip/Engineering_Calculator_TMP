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
        private bool Decimal = false; //Десятичный - фиксирует является ли число десятичным.
        private string NumberOne = ""; //Число первое
        private string NumberTwo = ""; //Число второе
        private string Function = ""; //Хранит функцию (Действие) над числами
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Buttons_With_Numbers_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender; //Преобразует тип object переменной sender в тип Button переменной button
            if(NumberOne == "") //Определение к какому числу относитя данный ввод числа.
            {
                if (TextBox_Scoreboard.Text == "0" || TextBox_Scoreboard.Text == "") //Условие позволяет заменить первоначальный ноль на число
                {
                    TextBox_Scoreboard.Text = button.Content.ToString(); //Присваиваем содержимому Результирующей строки содержимое кнопки.
                }
                else
                {
                    TextBox_Scoreboard.Text += button.Content.ToString();
                }
            }
            else
            {
                if(NumberTwo == "" || TextBox_Scoreboard.Text == "0") //Условие позволяет заменить первоначальный ноль на число
                {
                    TextBox_Scoreboard.Text = button.Content.ToString();
                    NumberTwo = TextBox_Scoreboard.Text;
                }
                else
                {
                    TextBox_Scoreboard.Text += button.Content.ToString();
                    NumberTwo = TextBox_Scoreboard.Text;
                }
            }
        }
        private void Buttons_With_Primitive_Functions_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Content)
            {
                case "+":
                    Function = "+";
                    NumberOne = TextBox_Scoreboard.Text; //Сохраняет первое число в NumberOne
                    NumberTwo = "";
                    break;
                case "-":
                    Function = "-";
                    NumberOne = TextBox_Scoreboard.Text;
                    NumberTwo = "";
                    break;
                case "÷":
                    Function = "/";
                    NumberOne = TextBox_Scoreboard.Text;
                    NumberTwo = "";
                    break;
                case "×":
                    Function = "*";
                    NumberOne = TextBox_Scoreboard.Text;
                    NumberTwo = "";
                    break;
                case "=":
                    Respond();
                    break;
                case "→":
                    if(TextBox_Scoreboard.Text.ToString()[TextBox_Scoreboard.Text.Length-1] == ',') //Позволяет определить, когда пользователь стирает запятую и перевести число в разряд целых
                    {
                        Decimal = false;
                    }
                    TextBox_Scoreboard.Text = TextBox_Scoreboard.Text.Remove(TextBox_Scoreboard.Text.Length - 1);
                    if (TextBox_Scoreboard.Text == "") //Если строка закончилась - пишет ноль.
                        TextBox_Scoreboard.Text = "0";
                    break;
                case "•":
                    if(!Decimal) //Позволяет избежать установки 2-N запятых в числе
                    {
                        Decimal = true;
                        TextBox_Scoreboard.Text += ",";
                    }
                    break;
                case "CE": //Удаляет число находящееся в строке и переводит состояние Decemal в false.
                    TextBox_Scoreboard.Text = "0";
                    Decimal = false;
                    break;
                case "C":
                    Decimal = false;
                    NumberOne = "";
                    NumberTwo = "";
                    Function = "";
                    TextBox_Scoreboard.Text = "0";
                    break;
                case "+/-":
                    TextBox_Scoreboard.Text = (double.Parse(TextBox_Scoreboard.Text)*-1).ToString();
                    break;

            }
        }
        private void Respond()
        {
            //FastMemory = Number
            if(NumberOne == "")
            {
                NumberOne = TextBox_Scoreboard.Text;
            }
            if (NumberTwo == "") //Позволяет произвести быстро действие с 1 числом (Самим-сабой), что бы не вводить такое же второе число
            {                    //(Уберает ошибку с отсуцтвием второго числа)
                NumberTwo = NumberOne;
            }
            switch (Function)
            {
                case "+":
                    NumberOne = (double.Parse(NumberOne) + double.Parse(NumberTwo)).ToString();
 
                    break;
                case "-":
                    NumberOne = (double.Parse(NumberOne) - double.Parse(NumberTwo)).ToString();
 
                    break;
                case "/":
                    NumberOne = (double.Parse(NumberOne) / double.Parse(NumberTwo)).ToString();
  
                    break;
                case "*":
                    NumberOne = (double.Parse(NumberOne) * double.Parse(NumberTwo)).ToString();
      
                    break;
            }
            if (double.Parse(NumberOne) % 1 == 0) //Условие позволяет проверить, является ли результирующее число десятичным или нет
            {
                Decimal = false;
            }
            else
            {
                if (!Decimal) Decimal = true;
            }
            TextBox_Scoreboard.Text = NumberOne;
        }


        /*
        private void Buttons_With_Primitive_Functions_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Content)
            {
                case "+/-":
                    double Number = double.Parse(TextBox_Scoreboard.Text);
                    TextBox_Scoreboard.Text = (Number * -1).ToString();
                    break;
                case "•":
                    if (!Decimal)
                    {
                        Button buttonTMP = new Button();
                        buttonTMP.Content = ".";
                        Print_New_Simbol_In_Scoreboard(buttonTMP, true);
                        Decimal = true;
                    }
                    break;
                case "÷":
                    Function = "/";
                    Update_Fast_Memory();
                    break;
                case "×":
                    Function = "*";
                    Update_Fast_Memory();
                    break;
                case "-":
                    Function = "-";
                    Update_Fast_Memory();
                    break;
                case "+":
                    Function = "+";
                    Update_Fast_Memory();
                    break;
                case "=":
                    Check_Result();
                    break;
                case "→":
                    TextBox_Scoreboard.Text = TextBox_Scoreboard.Text.Substring(TextBox_Scoreboard.Text.Length-1);
                    break;
                case "CE":
                    TextBox_Scoreboard.Text = "0";
                    break;
                case "C":
                    TextBox_Scoreboard.Text = "0";
                    FastMemory = "";
                    Decimal = false;
                    Function = "";
                    break;

            }
        }

        private void Check_Result()
        {
            if (Function == "/" && TextBox_Scoreboard.Text == "0")
            {
                MessageBox.Show("Вы пытаетесь поделить на ноль!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (FastMemory != "")
                {
                    //string value = new DataTable().Compute($"{FastMemory}{Function}{TextBox_Scoreboard.Text}", null).ToString();
                    double ResultValue = 0;
                    if(Function == "+")
                    {
                        ResultValue = double.Parse(FastMemory) + double.Parse(TextBox_Scoreboard.Text);
                    }
                    else if(Function == "-")
                    {
                        ResultValue = double.Parse(FastMemory) - double.Parse(TextBox_Scoreboard.Text);
                    }
                    else if(Function == "*")
                    {
                        ResultValue = double.Parse(FastMemory) * double.Parse(TextBox_Scoreboard.Text);
                    }
                    else if(Function == "/")
                    {
                        ResultValue = double.Parse(FastMemory) / double.Parse(TextBox_Scoreboard.Text);
                    }


                    if (ResultValue % 1 == 0)
                    {
                        if (Decimal) Decimal = false;
                        TextBox_Scoreboard.Text = ResultValue.ToString();
                    }
                    else
                    {
                        if (!Decimal) Decimal = true;
                        string ResultValueString = ResultValue.ToString().Replace(",", ".");
                        TextBox_Scoreboard.Text = ResultValueString;
                    }
                    TestTextBoxDecimal.Text = Decimal.ToString();
                }
            }
        }


        private void Buttons_With_Numbers_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Print_New_Simbol_In_Scoreboard(button);
        }
        private void Print_New_Simbol_In_Scoreboard(Button button, bool DecimalSeparator = false)
        {
            if (Function == "")
            {
                if (TextBox_Scoreboard.Text == "0" && !DecimalSeparator)
                {
                    if (button.Content == "0")
                    {

                    }
                    else
                    {
                        TextBox_Scoreboard.Text = button.Content.ToString();
                    }
                }
                else
                {
                    TextBox_Scoreboard.Text += button.Content.ToString();

                }
            }
            
        }
        private void Update_Fast_Memory()
        {
            if (FastMemory == "")
            {
                FastMemory = TextBox_Scoreboard.Text.ToString();
                Decimal = false;
                TextBox_Scoreboard.Text = "0";
            }
            else if(FastMemory != "")
            {
                Check_Result();
            }
        }
        */
    }
}
