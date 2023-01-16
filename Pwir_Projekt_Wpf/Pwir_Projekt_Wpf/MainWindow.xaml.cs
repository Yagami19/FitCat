using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Pwir_Projekt_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Method for scaling screen
        #region ScaleValue Depdency Property
        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                return mainWindow.OnCoerceScaleValue((double)value);
            else
                return value;
        }
        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                mainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }
        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0d;

            value = Math.Max(0.1, value);
            return value;
        }
        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {
        }
        public double ScaleValue
        {
            get
            {
                return (double)GetValue(ScaleValueProperty);
            }
            set
            {
                SetValue(ScaleValueProperty, value);
            }
        }
        #endregion
        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            CalculateScale();
        }

        private void CalculateScale()
        {
            double yScale = ActualHeight / 720.0d;
            double xScale = ActualWidth / 1280.0d;
            double value = Math.Min(xScale, yScale);
            ScaleValue = (double)OnCoerceScaleValue(MainGrid, value);
        }

        public MainWindow()
        {
            InitializeComponent();
        }
       //Method deleting TextBox data
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
        //Creating server reference       
        ServiceReference1.Service1Client calculator = new ServiceReference1.Service1Client();
        public double multiplier;

        //Methods for buttons
        private void Button_CalculateEnergy(object sender, RoutedEventArgs e)
        {
            double catWeight, catEnergy;

            if (Double.TryParse(WeightBox.Text, out catWeight))
            {               
                catEnergy = calculator.CalculateEnergy(catWeight, multiplier); 
                CaloriesBox.Text = catEnergy.ToString("0.##")+"kcal";
            }
            else
            {
                WeightBox.Text = ("Error");
                CaloriesBox.Text = "Format 0,0";
            }
        }
        private void Button_CalculateDensity(object sender, RoutedEventArgs e)
        {
            double[] result = new double[2];
            double protein, fat, ash, fiber, humidity, other, carboHydrates, density;

            if (Double.TryParse(ProteinBox.Text, out protein) 
                && Double.TryParse(FatBox.Text, out fat)
                && Double.TryParse(AshBox.Text, out ash)
                && Double.TryParse(FiberBox.Text, out fiber)
                && Double.TryParse(HumidityBox.Text, out humidity)
                && Double.TryParse(OtherBox.Text, out other)
                )
            {
                
                result = calculator.CalculateDensity(protein, fat, ash, fiber, humidity, other);
                carboHydrates = result[0];
                if ((carboHydrates) >= 0)
                {
                    density = result[1];
                    CarboHydratesBox.Text = carboHydrates.ToString("0.##") + "%";
                    DensityBox.Text = density.ToString("0.##")+"kcal";
                }
                else
                {
                    DensityBox.Text=("Sum of components over 100%");
                }
            }
            else
            {
                CarboHydratesBox.Text = "Error";
                DensityBox.Text = "Format 0,0";
            }


            
        }

        private void Button_CalculateFood(object sender, RoutedEventArgs e)
        {

            double calories, density, foodAmount;
            if (Double.TryParse(CaloriesBox.Text.Trim('k', 'c', 'a', 'l'), out calories) && Double.TryParse(DensityBox.Text.Trim('k', 'c', 'a', 'l'), out density))
            {
                foodAmount = calculator.CalculateGrams(calories, density);
                BoxFoodAmount.Text = foodAmount.ToString("0.##")+"g";
            }
            else
            {
                BoxFoodAmount.Text = "Format 0,0";
            }        

        }
        //Method parsing multiplier from Combo Box
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = CatTypeBox.SelectedIndex;
            if (selectedIndex == 0)
                multiplier = 1.2;
            if (selectedIndex == 1)
                multiplier = 1.4;
            if (selectedIndex == 2)
                multiplier = 1.0;
            if (selectedIndex == 3)
                multiplier = 0.8;
        }


    }
}
