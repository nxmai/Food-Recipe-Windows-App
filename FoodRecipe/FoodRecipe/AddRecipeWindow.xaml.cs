using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        private void FoodIngredient_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FoodIngredient.Text == "Ingredient")
            {
                FoodIngredient.Text = "";
                FoodIngredient.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void FoodIngredient_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FoodIngredient.Text))
            {
                FoodIngredient.Text = "Ingredient";
                FoodIngredient.Foreground = new SolidColorBrush(Colors.Gray);
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
            if (String.IsNullOrWhiteSpace( StepDescription.Text))
            {
                StepDescription.Text = "Step Description";
                StepDescription.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            Steps[curStep][0] = StepDescription.Text;
            StepDescription.Text = "Step Description";
            StepDescription.Foreground = new SolidColorBrush(Colors.Gray);
            AddImages.Visibility = Visibility.Visible;
            Images.Children.Clear();
            ImagesScrollView.Visibility = Visibility.Collapsed;
            curStep++;
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
                ImagesScrollView.Visibility = Visibility.Visible;
                for (int i=1; i<Steps[curStep].Count; i++)
                {
                    Image image1 = new Image();
                    image1.Height = 100;
                    image1.Margin = new Thickness(10,10,10,10);
                    var imageSteps = new BitmapImage( new Uri(Steps[curStep][i],UriKind.Absolute));
                    image1.Source = imageSteps;
                    Images.Children.Add(image1);
                }
                
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe(FoodName.Text,FoodIngredient.Text,thumbnailPath,YoutubeLink.Text,false,Steps);
            if (recipe.SaveToFiles($"{pathRoot}"))
            {
                MessageBox.Show("Đã thêm công thức");
               
            }
            else
            {
                MessageBox.Show("Chưa thêm được công thức\nLiên hệ nhà phát triển.");
            }
            
        }

        private void AddThumbnail_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog()==true)
            {
                thumbnailPath = screen.FileName;
                AddThumbnail.Visibility = Visibility.Collapsed;
                Image image1 = new Image();
                image1.Height = 130;
                var thumbnail = new BitmapImage(new Uri(thumbnailPath, UriKind.Absolute));
                image1.Source = thumbnail;
                Thumbnail.Children.Add(image1);
                Thumbnail.Visibility = Visibility.Visible;
            }

        }

        private void PrevStep_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextStep_Click(object sender, RoutedEventArgs e)
        {

        }

        private void YoutubeLink_GotFocus(object sender, RoutedEventArgs e)
        {
            if (YoutubeLink.Text == "Link Youtube")
            {
                YoutubeLink.Text = "";
                YoutubeLink.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void YoutubeLink_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(YoutubeLink.Text))
            {
                YoutubeLink.Text = "Link Youtube";
                YoutubeLink.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            FoodName.Text = "Food Name";
            FoodName.Foreground = new SolidColorBrush(Colors.Gray);

            FoodIngredient.Text = "Ingredient";
            FoodIngredient.Foreground = new SolidColorBrush(Colors.Gray);

            YoutubeLink.Text = "Link Youtube";
            YoutubeLink.Foreground = new SolidColorBrush(Colors.Gray);

            StepDescription.Text = "Step Description";
            StepDescription.Foreground = new SolidColorBrush(Colors.Gray);

            AddThumbnail.Visibility = Visibility.Visible;
            Thumbnail.Children.Clear();
            Thumbnail.Visibility = Visibility.Collapsed;

            AddImages.Visibility = Visibility.Visible;
            Images.Children.Clear();
            Images.Children.Clear();
            ImagesScrollView.Visibility = Visibility.Collapsed;

            curStep = 0;
            Steps.Clear();
        }
    }
}
