using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace FoodRecipe
{
    /// <summary>
    /// Interaction logic for SettingScreen.xaml
    /// </summary>
    public partial class SettingScreen : Window
    {
        //public delegate void ColorChangedHandler(String newColor);
        //public event ColorChangedHandler ColorChanged;

        //public String NewColor { get; set; }
        //private String _oldColor;
        //String DefaultColor;
        /*public SettingScreen(String oldColor, String defaultColor)
        {
            InitializeComponent();
            
            _oldColor = oldColor;
            DefaultColor = defaultColor;
            foreach (Button colorElement in this.ColorPicker.Children)
            {
                if (colorElement.Name==oldColor)
                {
                    ((FontAwesome.WPF.ImageAwesome)colorElement.Content).Visibility = Visibility.Visible;
                }
                else
                {
                    ((FontAwesome.WPF.ImageAwesome)colorElement.Content).Visibility = Visibility.Hidden;
                }
            }
            
        }*/
        public delegate void DeathHandler();
        public event DeathHandler Dying;
        public SettingScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            bool showSplash = bool.Parse(value);
            if (!showSplash)
            {
                isVisibleSplashScreen.IsChecked = false;
            }
            else
            {
                isVisibleSplashScreen.IsChecked = true;
            }
        }

        private void isVisibleSplashScreen_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Full,true);
        }

        private void isVisibleSplashScreen_Unchecked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Full,true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            Dying?.Invoke();
        }
        /*
private void PaleGreen_Click(object sender, RoutedEventArgs e)
{
   NewColor = "PaleGreen";
   ColorChanged?.Invoke(NewColor);
}

private void PaleVioletRed_Click(object sender, RoutedEventArgs e)
{
   NewColor = "PaleVioletRed";
   ColorChanged?.Invoke(NewColor);
}

private void PaleGoldenrod_Click(object sender, RoutedEventArgs e)
{
   NewColor = "PaleGoldenrod";
   ColorChanged?.Invoke(NewColor);
}

private void default_Click(object sender, RoutedEventArgs e)
{
   NewColor = DefaultColor;
   ColorChanged?.Invoke(NewColor);
}
*/
    }
}
