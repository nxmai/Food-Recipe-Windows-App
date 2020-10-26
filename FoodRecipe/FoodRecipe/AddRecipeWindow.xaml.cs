using Microsoft.Win32;
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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        String thumbnailPath = "";
        String pathRoot = AppDomain.CurrentDomain.BaseDirectory;
        List<List<String>> Steps = new List<List<string>>();
        int curStep = 0;
        public AddRecipeWindow()
        {
            InitializeComponent();
        }

        private void FoodName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FoodName.Text == "Food Name")
            {
                FoodName.Text = "";
                FoodName.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void FoodName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FoodName.Text))
            {
                FoodName.Text = "Food Name";
                FoodName.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void FoodDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FoodDescription.Text == "Description (optional)")
            {
                FoodDescription.Text = "";
                FoodDescription.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void FoodDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FoodDescription.Text))
            {
                FoodDescription.Text = "Description (optional)";
                FoodDescription.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void StepDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (StepDescription.Text == "Step Description")
            {
                StepDescription.Text = "";
                StepDescription.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void StepDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(StepDescription.Text))
            {
                StepDescription.Text = "Step Description";
                StepDescription.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            Steps[curStep][0] = StepDescription.Text;
            StepDescription.Text = " ";
            AddImages.Visibility = Visibility.Visible;
        }

        private void AddImages_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                List<String> tmp = new List<string>();
                tmp.Add(" ");
                for (int i = 0; i < screen.FileNames.Length; i++)
                {
                    tmp.Add(screen.FileNames[i]); 
                }
                Steps.Add(tmp);
                AddImages.Visibility = Visibility.Collapsed;
                
                for (int i=1; i<Steps[curStep].Count; i++)
                {
                    Image image1 = new Image();
                    image1.Height = 150;
                    var imageSteps = new BitmapImage( new Uri(Steps[curStep][i],UriKind.Absolute));
                    image1.Source = imageSteps;
                    Images.Children.Add(image1);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe(FoodName.Text,FoodDescription.Text,thumbnailPath,false,Steps);
            recipe.SaveToFiles($@"{pathRoot}/Recipe");
        }

        private void AddThumbnail_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog()==true)
            {
                thumbnailPath = screen.FileName;
            }
        }
    }
}
