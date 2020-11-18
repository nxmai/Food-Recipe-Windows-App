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
using System.Windows.Shapes;

namespace FoodRecipe
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();

            
        }

        private void toFavoriteScreen_Click(object sender, RoutedEventArgs e)
        {
            var screen = new FavoriteScreen();
            this.Close();
            screen.ShowDialog();
        }

        private void addRecipe_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeScreen = new AddRecipeWindow();
            addRecipeScreen.Dying += addRecipeScreenClosing;
            this.Hide();
            addRecipeScreen.Show();
        }

        private void addRecipeScreenClosing()
        {
            this.load();
            this.Show();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            var main = this;
            var settingScreen = new SettingScreen();
            settingScreen.Show();
            settingScreen.Topmost = true;
            settingScreen.Focus();
            this.IsEnabled = false;
            settingScreen.Dying += SettingScreenClosing;
        }

        private void SettingScreenClosing()
        {
            this.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void load()
        {
            return;
        }
    }
}
