using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoodRecipe
{
    /// <summary>
    /// Interaction logic for DetailScreen.xaml
    /// </summary>
    /// 

    public class Step
    {
        public List<string> instructions { get; set; }
        public List<BitmapImage> images { get; set; }
    }

    public class Entire_Process
    {
        public string name { get; set; }
        public List<Step> steps { get; set; }
        public string link_youtube { get; set; }
        public string ingredients { get; set; }
        public BitmapImage avatar { get; set; }
    }

    public partial class DetailScreen : Window
    {
        public delegate void DeathHandler();
        public event DeathHandler Dying;

        Entire_Process process;
        public DetailScreen(string foodName)
         {
                InitializeComponent();
                process = ProcessDAO.GetAll(foodName);
                Create_For_Right_Column(process.steps);
                Create_For_Left_Column(process);
         }

            public void Create_StackPanel_For_One_Step(Step step, int number)
            {
                var stackpanel = new StackPanel();


                var stackpanelTop = new StackPanel();

                var stackpanelTopLeft = new StackPanel();

                var stackpanelTopRight = new StackPanel();

                stackpanelTop.Children.Add(stackpanelTopLeft);
                stackpanelTop.Children.Add(stackpanelTopRight);

                stackpanelTop.Margin = new Thickness(10, 10, 5, 10);

                stackpanelTopLeft.Width = 100;

                stackpanelTopRight.Width = 570;


                stackpanelTop.Orientation = Orientation.Horizontal;

                Label step_label = new Label();
                step_label.Content = $"Bước {number}:";
                stackpanelTopLeft.Children.Add(step_label);
                step_label.FontSize = 25;
                step_label.Foreground = Brushes.Goldenrod;
                step_label.FontWeight = FontWeights.UltraBold;

                string instruction = "";
                int count = 0;
                foreach (var line in step.instructions)
                {
                    instruction += line;
                    if (instruction[instruction.Length - 1] != ' ')
                    {
                        if (count == 0)
                        {
                            if (instruction[instruction.Length - 1] == ':')
                            {
                                instruction += " ";
                            }
                            else
                            {
                                instruction += ": ";
                            }
                        }
                        else
                        {
                            if (instruction[instruction.Length - 1] == '.')
                            {
                                instruction += " ";
                            }
                            else
                            {
                                instruction += ". ";
                            }
                        }
                    }
                    count++;

                }

                var textBlock = new TextBlock();
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Text = instruction;
                textBlock.FontSize = 16;
                textBlock.TextAlignment = TextAlignment.Justify;
                textBlock.Margin = new Thickness(10,10,15,10);
                stackpanelTopRight.Children.Add(textBlock);

                var stackpanelBottom = new StackPanel();

                stackpanel.Children.Add(stackpanelTop);
                stackpanel.Children.Add(stackpanelBottom);



            var uniGrid = new UniformGrid();
                uniGrid.Margin = new Thickness(80, 0, 0, 0);

                uniGrid.Columns = 2;


                for (int i = 0; i < step.images.Count; i++)
                {
                    var imageStep1 = new Image();
                    imageStep1.Source = step.images[i];
                    imageStep1.Width = 200;
                    imageStep1.Height = 200;


                    if (i % 2 == 1)
                    {
                        imageStep1.HorizontalAlignment = HorizontalAlignment.Left;

                    }
                    else
                    {
                        imageStep1.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    var grid_temp = new Grid();
                    var boder = new Border();
                    boder.CornerRadius = new CornerRadius(15);
                    var temp = new ImageBrush();
                    temp.ImageSource = step.images[i];
                    boder.Background = temp;
                    grid_temp.Children.Add(boder);
                    boder.Width = 200;
                    boder.Height = 200;

                    if (i % 2 == 1)
                    {
                        grid_temp.HorizontalAlignment = HorizontalAlignment.Left;
                        grid_temp.Margin = new Thickness(15, 5, 10, 10);
                    }
                    else
                    {
                        grid_temp.HorizontalAlignment = HorizontalAlignment.Right;
                        grid_temp.Margin = new Thickness(10, 5, 0, 10);
                    }


                    uniGrid.Children.Add(grid_temp);
                    //uniGrid.Children.Add(imageStep1);
                }

                stack_Panel_Detail_Right.Children.Add(stackpanel);
                stack_Panel_Detail_Right.Children.Add(uniGrid);
            }


            public void Create_For_Right_Column(List<Step> listOfStep)
            {
                int number = 1;
                foreach (var step in listOfStep)
                {
                    Create_StackPanel_For_One_Step(step, number);
                    number++;
                }
            }

            public void Create_For_Left_Column(Entire_Process process)
            {
                Create_Video();
                nameFood.Content = process.name;
                process.ingredients = process.ingredients.Replace(", ", "\n");

                var label = new Label();
                label.Content = process.ingredients;
                label.FontSize = 13;
                label.Margin = new Thickness(30, 0, 0, 0);
                ingredient.Children.Add(label);


                for (int i = 0; i < process.steps.Count; i++)
                {
                    for (int j = 0; j < process.steps[i].images.Count; j++)
                    {

                        var grid_temp = new Grid();
                        var boder = new Border();
                        boder.CornerRadius = new CornerRadius(15);
                        var temp = new ImageBrush();
                        temp.ImageSource = process.steps[i].images[j];
                        boder.Background = temp;
                        grid_temp.Children.Add(boder);
                        boder.Width = 80;
                        boder.Height = 80;
                    grid_temp.Margin = new Thickness(2);
                        carousel.Children.Add(grid_temp);
                    }

                }

            }

            private void Window_Loaded(object sender, RoutedEventArgs e)
            {

            }

            private void Create_Video()
            {

                string html = "<html><head>";
                html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
                html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}' frameborder='0' height='200' width='290' allowfullscreen></iframe>";
                html += "</body></html>";
                //this.dimg.Visibility = Visibility.Collapsed;
                this.video.Visibility = Visibility.Visible;
                this.video.NavigateToString(string.Format(html, process.link_youtube.Split('=')[1].Replace("&feature", "")));
            }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dying?.Invoke();
        }
    }
}
