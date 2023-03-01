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
    public partial class MainWindow : Window
    {
        #region
        private bool IsDecimal = false;
        private bool IsNumberTwo = false;

        private string NumberOne = null;
        private string NumberTwo = null;

        private string CurrentFunction = "";
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }



        private void AddNumber(string txt)
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
        private void SetNumber(string txt)
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
        private void Buttons_With_Numbers_Click(object sender, RoutedEventArgs e)
        {
            var txt = ((Button)sender).Content.ToString();
            {
                if(IsDecimal && txt == "•")
                {
                    MessageBox.Show("Число десятичное!","Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if(txt == "•")
                {
                    IsDecimal = true;
                    txt = ",";
                }
            }

            if (txt == "+/-")
            {
                if (ResultScoreboard_TextBox.Text.Length > 0)
                    if(ResultScoreboard_TextBox.Text[0] == '-')
                    {
                        ResultScoreboard_TextBox.Text = ResultScoreboard_TextBox.Text.Substring(1, ResultScoreboard_TextBox.Text.Length - 1);
                    }
                    else
                    {
                        ResultScoreboard_TextBox.Text = "-" + ResultScoreboard_TextBox.Text;
                    }
                SetNumber(ResultScoreboard_TextBox.Text);
                return;
            }
            AddNumber(txt);
        }

        private void Buttons_With_Primitive_Functions_Click(object sender, RoutedEventArgs e)
        {
            if (IsNumberTwo)
            {
                IsNumberTwo = false;
                CalculateResult(CurrentFunction);
                NumberOne = null;
                NumberTwo = null;
            }
            if(NumberOne == null)
            {
                if (ResultScoreboard_TextBox.Text.Length > 0) NumberOne = ResultScoreboard_TextBox.Text;
                else
                {
                    MessageBox.Show("Введите число!", "Ошибка..", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            IsNumberTwo = true; 
            CurrentFunction = ((Button)sender).Content.ToString();
            CalculateResult(CurrentFunction);
        }

        private void CalculateResult(string operation)
        {
            string result = null;
            switch (operation)
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

        private void OutputResult(string result, string operation)
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

        private void Buttons_With_Official_Functions_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Content)
            {
                case "C":
                    ResultScoreboard_TextBox.Text = "";
                    textHistory.Text = "";
                    IsNumberTwo = false;
                    CurrentFunction = null;
                    NumberOne = null;
                    NumberTwo = null;
                    IsDecimal = false;
                    break;
                case "=":
                    CalculateResult(CurrentFunction);
                    IsNumberTwo = false;
                    NumberOne = null;
                    NumberTwo = null;
                    break;
                case "→":
                    if(ResultScoreboard_TextBox.Text.Length <=0)
                    {
                        return;
                    }
                    ResultScoreboard_TextBox.Text = ResultScoreboard_TextBox.Text.Substring(0, ResultScoreboard_TextBox.Text.Length - 1);
                    SetNumber(ResultScoreboard_TextBox.Text);
                    break;
                case "CE":
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
