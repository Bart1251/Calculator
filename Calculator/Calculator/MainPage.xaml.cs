using System;
using Xamarin.Forms;

namespace CalculatorApp
{

    public partial class MainPage : ContentPage
    {
        private double width = 0;
        private double height = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (this.width > this.height)
                {
                    DisplayAlert("teraz", "teraz", "teraz");
                }
            }
        }
    }
}
