using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        List<String> tags = new List<string>();
        int curStep = 0;
        int curStepVisibility = 0;
        public delegate void DeathHandler();
        public event DeathHandler Dying;
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
            if (String.IsNullOrWhiteSpace(StepDescription.Text))
            {
                StepDescription.Text = "Step Description";
                StepDescription.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            bool continueAddStep = false;
            if (curStep != curStepVisibility)
            {
                curStepVisibility = curStep;
                StepDescription.Text = "Step Description";
                StepDescription.Foreground = new SolidColorBrush(Colors.Gray);
                AddImages.Visibility = Visibility.Visible;
                StepDescription.IsEnabled = true;
                Images.Children.Clear();
                ImagesScrollView.Visibility = Visibility.Collapsed;
                StepCount.Text = $"{curStepVisibility + 1}?/{Steps.Count}";
            }
            else
            {
                if (StepDescription.Text == "Step Description")
                {
                    MessageBox.Show("Bạn phải có hướng dẫn cho bước nấu ăn này chớ");
                    StepDescription.Focus();
                }
                else if (curStep == Steps.Count)
                {
                    var result = MessageBox.Show("Wait Wait! Bước nấu ăn này không có hình ảnh minh họa sao", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        List<String> tmp = new List<string>();
                        tmp.Add("");
                        Steps.Add(tmp);
                        continueAddStep = true;
                    }
                    else
                    {
                        /*Do Nothing*/
                    }
                }
                else
                {
                    continueAddStep = true;
                }
                if (continueAddStep)
                {
                    Steps[curStep][0] = StepDescription.Text;
                    StepDescription.Text = "Step Description";
                    StepDescription.Foreground = new SolidColorBrush(Colors.Gray);
                    StepDescription.IsEnabled = true;
                    AddImages.Visibility = Visibility.Visible;
                    Images.Children.Clear();
                    ImagesScrollView.Visibility = Visibility.Collapsed;
                    curStep++;
                    curStepVisibility = curStep;
                    StepCount.Text = $"{curStepVisibility + 1}?/{Steps.Count}";
                }
            }
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
                for (int i = 1; i < Steps[curStep].Count; i++)
                {
                    Image image1 = new Image();
                    image1.Height = 100;
                    image1.Margin = new Thickness(10, 10, 10, 10);
                    var imageSteps = new BitmapImage(new Uri(Steps[curStep][i], UriKind.Absolute));
                    image1.Source = imageSteps;
                    Images.Children.Add(image1);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (FoodName.Text == "Food Name")
            {
                MessageBox.Show("Ey yo! Món ăn phải có tên nè");
                FoodName.Focus();
            }
            else if (FoodIngredient.Text == "Ingredient")
            {
                MessageBox.Show("Hey! Món ăn thì phải có thành phần, nguyên liệu chứ nhờ");
                FoodIngredient.Focus();
            }
            else if (String.IsNullOrEmpty(thumbnailPath))
            {
                MessageBox.Show("Hmmmm! Món ăn phải có hình mới hấp dẫn chớ");
            }
            else if (Steps.Count == 0)
            {
                MessageBox.Show("Oh no! Phải có ít nhất một bước nấu ăn chớ");
            }
            else
            {
                Recipe recipe = new Recipe(FoodName.Text, FoodIngredient.Text, thumbnailPath, YoutubeLink.Text, false, Steps, "HeartOutline", "White");
                int err = recipe.SaveToFiles($"{pathRoot}");
                if (err == 0)
                {
                    MessageBox.Show("Đã thêm công thức");

                }
                else if (err == 1)
                {
                    MessageBox.Show("Tên công thức món ăn này đã tồn tại. Hãy đổi thành tên khác");
                }
                else
                {
                    MessageBox.Show("Chưa thêm được công thức\nLiên hệ nhà phát triển.");
                }
            }
        }

        private void AddThumbnail_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
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

            if (curStepVisibility == 0)
            {
                MessageBox.Show("Bạn đang ở bước đầu tiên rồi á");
            }
            else
            {
                curStepVisibility--;
                StepDescription.Text = Steps[curStepVisibility][0];
                StepDescription.Foreground = new SolidColorBrush(Colors.Black);
                StepDescription.IsEnabled = false;
                Images.Children.Clear();
                AddImages.Visibility = Visibility.Collapsed;
                ImagesScrollView.Visibility = Visibility.Visible;
                for (int i = 1; i < Steps[curStepVisibility].Count; i++)
                {
                    Image image1 = new Image();
                    image1.Height = 100;
                    image1.Margin = new Thickness(10, 10, 10, 10);
                    var imageSteps = new BitmapImage(new Uri(Steps[curStepVisibility][i], UriKind.Absolute));
                    image1.Source = imageSteps;
                    Images.Children.Add(image1);
                }
                StepCount.Text = $"{curStepVisibility + 1}/{Steps.Count}";
            }
        }

        private void NextStep_Click(object sender, RoutedEventArgs e)
        {

            if (curStepVisibility == curStep)
            {
                MessageBox.Show("Bạn vừa đi qua bước cuối cùng rồi á. Giờ thêm bước mới đi nè");
                StepCount.Text = $"{curStepVisibility + 1}?/{Steps.Count}";
            }
            else if (curStepVisibility == curStep - 1)
            {
                curStepVisibility++;
                StepDescription.Text = "Step Description";
                StepDescription.Foreground = new SolidColorBrush(Colors.Gray);
                StepDescription.IsEnabled = true;
                AddImages.Visibility = Visibility.Visible;
                Images.Children.Clear();
                ImagesScrollView.Visibility = Visibility.Collapsed;
                StepCount.Text = $"{curStepVisibility + 1}?/{Steps.Count}";

            }
            else
            {
                curStepVisibility++;
                StepDescription.Text = Steps[curStepVisibility][0];
                StepDescription.Foreground = new SolidColorBrush(Colors.Black);
                StepDescription.IsEnabled = false;
                Images.Children.Clear();
                AddImages.Visibility = Visibility.Collapsed;
                ImagesScrollView.Visibility = Visibility.Visible;
                for (int i = 1; i < Steps[curStepVisibility].Count; i++)
                {
                    Image image1 = new Image();
                    image1.Height = 100;
                    image1.Margin = new Thickness(10, 10, 10, 10);
                    var imageSteps = new BitmapImage(new Uri(Steps[curStepVisibility][i], UriKind.Absolute));
                    image1.Source = imageSteps;
                    Images.Children.Add(image1);
                }
                StepCount.Text = $"{curStepVisibility + 1}/{Steps.Count}";
            }
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
            ImagesScrollView.Visibility = Visibility.Collapsed;

            curStep = 0;
            curStepVisibility = 0;
            Steps.Clear();
            StepCount.Text = $"{curStepVisibility + 1}?/{Steps.Count}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dying?.Invoke();
        }

        /*
        private void AddTag_Click(object sender, RoutedEventArgs e)
        {
            if (TagInput.Text == "Add Tag...")
            {
                MessageBox.Show("");
            }
            else
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Background = new SolidColorBrush(Colors.White);
                textBlock.Text = TagInput.Text;
                textBlock.Margin = new Thickness(1,0,1,0);
                textBlock.TextAlignment = TextAlignment.Center;
                TagBoxes.Children.Add(textBlock);
                TagInput.Text = "Add Tag...";
                
            }
        }

        private void TagInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TagInput.Text == "Add Tag...")
            {
                TagInput.Text = "";
                TagInput.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TagInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TagInput.Text))
            {
                TagInput.Text = "Add Tag...";
                TagInput.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
        */
    }
}
