using System;
using Xamarin.Forms;

namespace CalculatorApp
{
    public enum Operation
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
        Square,
        SquareRoot,
        Inverse
    }

    public partial class MainPage : ContentPage
    {
        private double a = 0;
        private double b = 0;
        private double result = 0;
        private Operation operation = Operation.None;
        private bool comma = false;
        private bool equalsClicked = false;

        private double width = 0;
        private double height = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NumberClick(object sender, EventArgs e)
        {
            try
            {
                if (operation == Operation.None)
                {
                    if (comma && !a.ToString().Contains(","))
                        a = double.Parse(a.ToString() + "," + ((Button)sender).Text);
                    else
                        a = double.Parse(a.ToString() + ((Button)sender).Text);
                    comma = false;
                }
                else
                {
                    if (comma && !b.ToString().Contains(","))
                        b = double.Parse(b.ToString() + "," + ((Button)sender).Text);
                    else
                        b = double.Parse(b.ToString() + ((Button)sender).Text);
                    comma = false;
                }
                Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OperationClick(object sender, EventArgs e)
        {
            comma = false;
            if (operation != Operation.None)
                return;
            switch (((Button)sender).Text)
            {
                case "+":
                    operation = Operation.Add;
                    break;
                case "-":
                    operation = Operation.Subtract;
                    break;
                case "x":
                    operation = Operation.Multiply;
                    break;
                case "/":
                    operation = Operation.Divide;
                    break;
                case "x^2":
                    operation = Operation.Square;
                    result = a * a;
                    break;
                case "sqrt":
                    operation = Operation.SquareRoot;
                    result = Math.Sqrt(a);
                    break;
                case "1/x":
                    operation = Operation.Inverse;
                    result = 1 / a;
                    break;
            }
            Print();
        }

        private void EqualsClick(object sender, EventArgs e)
        {
            if (operation == Operation.None)
                return;
            switch (operation)
            {
                case Operation.Add:
                    result = a + b;
                    break;
                case Operation.Subtract:
                    result = a - b;
                    break;
                case Operation.Multiply:
                    result = a * b;
                    break;
                case Operation.Divide:
                    result = a / b;
                    break;
            }
            equalsClicked = true;
            Print();
            a = result;
            b = 0;
            result = 0;
            comma = false;
            equalsClicked = false;
            operation = Operation.None;
        }

        private void ClearClick(object sender, EventArgs e)
        {
            a = 0;
            b = 0;
            result = 0;
            comma = false;
            equalsClicked = false;
            operation = Operation.None;
            Print();
        }

        private void CommaClick(object sender, EventArgs e)
        {
            comma = true;
        }

        private void Print()
        {
            if (operation == Operation.None)
            {
                Output_Label.Text = a.ToString();
                OutputEquation_Label.Text = "";
                return;
            }
            switch (operation)
            {
                case Operation.Add:
                case Operation.Subtract:
                case Operation.Multiply:
                case Operation.Divide:
                    if (equalsClicked)
                    {
                        Output_Label.Text = result.ToString();
                        OutputEquation_Label.Text = a.ToString() + " " + OperationToSign(operation) + " " + b.ToString() + " =";
                    }
                    else
                    {
                        Output_Label.Text = b.ToString();
                        OutputEquation_Label.Text = a.ToString() + " " + OperationToSign(operation);
                    }
                    return;
                case Operation.Square:
                    OutputEquation_Label.Text = "sqr(" + a.ToString() + ")";
                    break;
                case Operation.SquareRoot:
                    OutputEquation_Label.Text = "sqrt(" + a.ToString() + ")";
                    break;
                case Operation.Inverse:
                    OutputEquation_Label.Text = "1 / " + a.ToString();
                    break;
            }
            Output_Label.Text = result.ToString();
            a = result;
            b = 0;
            comma = false;
            operation = Operation.None;
            result = 0;
        }

        private string OperationToSign(Operation operation)
        {
            string sign = "";
            switch (operation)
            {
                case Operation.Add:
                    sign = "+";
                    break;
                case Operation.Subtract:
                    sign = "-";
                    break;
                case Operation.Multiply:
                    sign = "x";
                    break;
                case Operation.Divide:
                    sign = "/";
                    break;
            }
            return sign;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if (this.width > this.height)
                {
                    GridLayout.Children.Remove(VerticalGridControll);
                    GridLayout.Children.Add(HorizontalGridControll);
                    DisplayAlert("teraz", "teraz", "teraz");
                }
                else
                {
                    GridLayout.Children.Remove(HorizontalGridControll);
                    GridLayout.Children.Add(VerticalGridControll);
                }
            }
        }
    }
}
